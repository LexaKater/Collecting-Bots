using System;
using System.Collections.Generic;

namespace Zenject
{
    // Zero parameters
    public class KeyedFactory<TBase, TKey> : KeyedFactoryBase<TBase, TKey>
    {
        protected override IEnumerable<Type> ProvidedTypes
        {
            get { return new Type[0]; }
        }

        public virtual TBase Create(TKey key)
        {
            var type = GetTypeForKey(key);
            return (TBase)Container.Instantiate(type);
        }
    }

    // One parameter
    public class KeyedFactory<TBase, TKey, TParam1> : KeyedFactoryBase<TBase, TKey>
    {
        protected override IEnumerable<Type> ProvidedTypes
        {
            get { return new[] { typeof(TParam1) }; }
        }

        public virtual TBase Create(TKey key, TParam1 param1)
        {
            return (TBase)Container.InstantiateExplicit(
                GetTypeForKey(key),
                new List<TypeValuePair>
                {
                    InjectUtil.CreateTypePair(param1)
                });
        }
    }

    // Two parameters
    public class KeyedFactory<TBase, TKey, TParam1, TParam2> : KeyedFactoryBase<TBase, TKey>
    {
        protected override IEnumerable<Type> ProvidedTypes
        {
            get { return new[] { typeof(TParam1), typeof(TParam2) }; }
        }

        public virtual TBase Create(TKey key, TParam1 param1, TParam2 param2)
        {
            return (TBase)Container.InstantiateExplicit(
                GetTypeForKey(key),
                new List<TypeValuePair>
                {
                    InjectUtil.CreateTypePair(param1),
                    InjectUtil.CreateTypePair(param2)
                });
        }
    }

    // Three parameters
    public class KeyedFactory<TBase, TKey, TParam1, TParam2, TParam3> : KeyedFactoryBase<TBase, TKey>
    {
        protected override IEnumerable<Type> ProvidedTypes
        {
            get { return new[] { typeof(TParam1), typeof(TParam2), typeof(TParam3) }; }
        }

        public virtual TBase Create(TKey key, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            return (TBase)Container.InstantiateExplicit(
                GetTypeForKey(key),
                new List<TypeValuePair>
                {
                    InjectUtil.CreateTypePair(param1),
                    InjectUtil.CreateTypePair(param2),
                    InjectUtil.CreateTypePair(param3)
                });
        }
    }

    // Four parameters
    public class KeyedFactory<TBase, TKey, TParam1, TParam2, TParam3, TParam4> : KeyedFactoryBase<TBase, TKey>
    {
        protected override IEnumerable<Type> ProvidedTypes
        {
            get { return new[] { typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4) }; }
        }

        public virtual TBase Create(TKey key, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            return (TBase)Container.InstantiateExplicit(
                GetTypeForKey(key),
                new List<TypeValuePair>
                {
                    InjectUtil.CreateTypePair(param1),
                    InjectUtil.CreateTypePair(param2),
                    InjectUtil.CreateTypePair(param3),
                    InjectUtil.CreateTypePair(param4)
                });
        }
    }
}
