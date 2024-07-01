using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;

namespace Zenject
{
    public abstract class KeyedFactoryBase<TBase, TKey> : IValidatable
    {
        [Inject]
        readonly DiContainer _container = null;

        [InjectOptional]
        readonly List<ValuePair<TKey, Type>> _typePairs = null;

        Dictionary<TKey, Type> _typeMap = null;

        [InjectOptional]
        readonly Type _fallbackType = null;

        protected DiContainer Container
        {
            get { return _container; }
        }

        protected abstract IEnumerable<Type> ProvidedTypes
        {
            get;
        }

        public ICollection<TKey> Keys
        {
            get { return _typeMap.Keys; }
        }

        protected Dictionary<TKey, Type> TypeMap
        {
            get { return _typeMap; }
        }

        [Inject]
        public void Initialize()
        {
            Assert.That(_fallbackType == null || _fallbackType.DerivesFromOrEqual<TBase>(),
                "Expected fallback type '{0}' to derive from '{1}'", _fallbackType, typeof(TBase));

#if UNITY_EDITOR
            var duplicates = _typePairs.Select(x => x.First).GetDuplicates();

            if (!duplicates.IsEmpty())
            {
                throw Assert.CreateException(
                    "Found duplicate values in KeyedFactory: {0}", duplicates.Select(x => x.ToString()).Join(", "));
            }
#endif

            _typeMap = _typePairs.ToDictionary(x => x.First, x => x.Second);
            _typePairs.Clear();
        }

        public bool HasKey(TKey key)
        {
            return _typeMap.ContainsKey(key);
        }

        protected Type GetTypeForKey(TKey key)
        {
            Type keyedType;

            if (!_typeMap.TryGetValue(key, out keyedType))
            {
                Assert.IsNotNull(_fallbackType, "Could not find instance for key '{0}'", key);
                return _fallbackType;
            }

            return keyedType;
        }

        public virtual void Validate()
        {
            foreach (var constructType in _typeMap.Values)
            {
                Container.InstantiateExplicit(
                    constructType, ValidationUtil.CreateDefaultArgs(ProvidedTypes.ToArray()));
            }
        }

        protected static ConditionCopyNonLazyBinder AddBindingInternal<TDerived>(DiContainer container, TKey key)
            where TDerived : TBase
        {
            return container.Bind<ValuePair<TKey, Type>>()
                .FromInstance(ValuePair.New(key, typeof(TDerived)));
        }
    }
}