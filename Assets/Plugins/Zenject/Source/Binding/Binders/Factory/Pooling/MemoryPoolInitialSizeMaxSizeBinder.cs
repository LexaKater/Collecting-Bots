namespace Zenject
{
    [NoReflectionBaking]
    public class MemoryPoolInitialSizeMaxSizeBinder<TContract> : MemoryPoolMaxSizeBinder<TContract>
    {
        public MemoryPoolInitialSizeMaxSizeBinder(
            DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, MemoryPoolBindInfo poolBindInfo)
            : base(bindContainer, bindInfo, factoryBindInfo, poolBindInfo)
        {
        }

        public MemoryPoolMaxSizeBinder<TContract> WithInitialSize(int size)
        {
            MemoryPoolBindInfo.InitialSize = size;
            return this;
        }

        public FactoryArgumentsToChoiceBinder<TContract> WithFixedSize(int size)
        {
            MemoryPoolBindInfo.InitialSize = size;
            MemoryPoolBindInfo.MaxSize = size;
            MemoryPoolBindInfo.ExpandMethod = PoolExpandMethods.Disabled;
            return this;
        }
    }
}