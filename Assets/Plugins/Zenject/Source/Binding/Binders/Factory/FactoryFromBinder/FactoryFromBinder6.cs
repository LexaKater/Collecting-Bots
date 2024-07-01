using System.Collections.Generic;

namespace Zenject
{
    [NoReflectionBaking]
    public class FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>
        : FactoryFromBinderBase
    {
        public FactoryFromBinder(
            DiContainer container, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
            : base(container, typeof(TContract), bindInfo, factoryBindInfo)
        {
        }

        public ConditionCopyNonLazyBinder FromMethod(
#if !NET_4_6
            ModestTree.Util.
#endif
            Func<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> method)
        {
            ProviderFunc =
                (container) => new MethodProviderWithContainer<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>(method);

            return this;
        }

        // Shortcut for FromIFactory and also for backwards compatibility
        public ConditionCopyNonLazyBinder FromFactory<TSubFactory>()
            where TSubFactory : IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>
        {
            return this.FromIFactory(x => x.To<TSubFactory>().AsCached());
        }

        public FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> FromSubContainerResolve()
        {
            return FromSubContainerResolve(null);
        }

        public FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> FromSubContainerResolve(object subIdentifier)
        {
            return new FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>(
                BindContainer, BindInfo, FactoryBindInfo, subIdentifier);
        }
    }

    // These methods have to be extension methods for the UWP build (with .NET backend) to work correctly
    // When these are instance methods it takes a really long time then fails with StackOverflowException
}
