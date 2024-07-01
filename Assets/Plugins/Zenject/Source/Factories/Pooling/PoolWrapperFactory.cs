using System;

namespace Zenject
{
    public class PoolWrapperFactory<T> : IFactory<T>
        where T : IDisposable
    {
        readonly IMemoryPool<T> _pool;

        public PoolWrapperFactory(IMemoryPool<T> pool)
        {
            _pool = pool;
        }

        public T Create()
        {
            return _pool.Spawn();
        }
    }
}