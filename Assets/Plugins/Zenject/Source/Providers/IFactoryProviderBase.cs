using System;
using System.Collections.Generic;

namespace Zenject
{
    public abstract class IFactoryProviderBase<TContract> : IProvider
    {
        public IFactoryProviderBase(
            DiContainer container, Guid factoryId)
        {
            Container = container;
            FactoryId = factoryId;
        }

        public bool IsCached
        {
            get { return false; }
        }

        protected Guid FactoryId
        {
            get;
            private set;
        }

        protected DiContainer Container
        {
            get;
            private set;
        }

        public bool TypeVariesBasedOnMemberType
        {
            get { return false; }
        }

        public Type GetInstanceType(InjectContext context)
        {
            return typeof(TContract);
        }

        public abstract void GetAllInstancesWithInjectSplit(
            InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer);
    }
}