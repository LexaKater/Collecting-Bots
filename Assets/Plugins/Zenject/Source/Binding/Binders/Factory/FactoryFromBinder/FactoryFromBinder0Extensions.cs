using System;
using UnityEngine;

namespace Zenject
{
    public static class FactoryFromBinder0Extensions
    {
        public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TContract, TMemoryPool>(
                this FactoryFromBinder<TContract> fromBinder,
                Action<MemoryPoolInitialSizeMaxSizeBinder<TContract>> poolBindGenerator)
            // Unfortunately we have to pass the same contract in again to satisfy the generic
            // constraints below
            where TContract : IPoolable<IMemoryPool>
            where TMemoryPool : MemoryPool<IMemoryPool, TContract>
        {
            // Use a random ID so that our provider is the only one that can find it and so it doesn't
            // conflict with anything else
            var poolId = Guid.NewGuid();

            // Important to use NoFlush otherwise the binding will be finalized early
            var binder = fromBinder.BindContainer.BindMemoryPoolCustomInterfaceNoFlush<TContract, TMemoryPool, TMemoryPool>().WithId(poolId);

            // Always make it non lazy by default in case the user sets an InitialSize
            binder.NonLazy();

            poolBindGenerator(binder);

            fromBinder.ProviderFunc =
                (container) => { return new PoolableMemoryPoolProvider<TContract, TMemoryPool>(container, poolId); };

            return new ArgConditionCopyNonLazyBinder(fromBinder.BindInfo);
        }

        public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TContract>(
                this FactoryFromBinder<TContract> fromBinder)
            // Unfortunately we have to pass the same contract in again to satisfy the generic
            // constraints below
            where TContract : IPoolable<IMemoryPool>
        {
            return fromBinder.FromPoolableMemoryPool<TContract>(x => {});
        }

        public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TContract>(
                this FactoryFromBinder<TContract> fromBinder,
                Action<MemoryPoolInitialSizeMaxSizeBinder<TContract>> poolBindGenerator)
            // Unfortunately we have to pass the same contract in again to satisfy the generic
            // constraints below
            where TContract : IPoolable<IMemoryPool>
        {
            return fromBinder.FromPoolableMemoryPool<TContract, PoolableMemoryPool<IMemoryPool, TContract>>(poolBindGenerator);
        }

#if !NOT_UNITY3D
        public static ArgConditionCopyNonLazyBinder FromMonoPoolableMemoryPool<TContract>(
                this FactoryFromBinder<TContract> fromBinder)
            // Unfortunately we have to pass the same contract in again to satisfy the generic
            // constraints below
            where TContract : Component, IPoolable<IMemoryPool>
        {
            return fromBinder.FromMonoPoolableMemoryPool<TContract>(x => {});
        }

        public static ArgConditionCopyNonLazyBinder FromMonoPoolableMemoryPool<TContract>(
                this FactoryFromBinder<TContract> fromBinder,
                Action<MemoryPoolInitialSizeMaxSizeBinder<TContract>> poolBindGenerator)
            // Unfortunately we have to pass the same contract in again to satisfy the generic
            // constraints below
            where TContract : Component, IPoolable<IMemoryPool>
        {
            return fromBinder.FromPoolableMemoryPool<TContract, MonoPoolableMemoryPool<IMemoryPool, TContract>>(poolBindGenerator);
        }
#endif

        public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TContract, TMemoryPool>(
                this FactoryFromBinder<TContract> fromBinder)
            // Unfortunately we have to pass the same contract in again to satisfy the generic
            // constraints below
            where TContract : IPoolable<IMemoryPool>
            where TMemoryPool : MemoryPool<IMemoryPool, TContract>
        {
            return fromBinder.FromPoolableMemoryPool<TContract, TMemoryPool>(x => {});
        }

        public static ArgConditionCopyNonLazyBinder FromIFactory<TContract>(
            this FactoryFromBinder<TContract> fromBinder,
            Action<ConcreteBinderGeneric<IFactory<TContract>>> factoryBindGenerator)
        {
            Guid factoryId;
            factoryBindGenerator(
                fromBinder.CreateIFactoryBinder<IFactory<TContract>>(out factoryId));

            fromBinder.ProviderFunc =
                (container) => { return new IFactoryProvider<TContract>(container, factoryId); };

            return new ArgConditionCopyNonLazyBinder(fromBinder.BindInfo);
        }
    }
}