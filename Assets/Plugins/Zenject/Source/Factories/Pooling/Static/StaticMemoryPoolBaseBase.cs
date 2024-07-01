using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
    [NoReflectionBaking]
    public abstract class StaticMemoryPoolBaseBase<TValue> : IDespawnableMemoryPool<TValue>, IDisposable
        where TValue : class
    {
        // I also tried using ConcurrentBag instead of Stack + lock here but that performed much much worse
        readonly Stack<TValue> _stack = new Stack<TValue>();

        Action<TValue> _onDespawnedMethod;
        int _activeCount;

#if ZEN_MULTITHREADING
        protected readonly object _locker = new object();
#endif

        public StaticMemoryPoolBaseBase(Action<TValue> onDespawnedMethod)
        {
            _onDespawnedMethod = onDespawnedMethod;

#if UNITY_EDITOR
            StaticMemoryPoolRegistry.Add(this);
#endif
        }

        public Action<TValue> OnDespawnedMethod
        {
            set { _onDespawnedMethod = value; }
        }

        public int NumTotal
        {
            get { return NumInactive + NumActive; }
        }

        public int NumActive
        {
            get
            {
#if ZEN_MULTITHREADING
                lock (_locker)
#endif
                {
                    return _activeCount;
                }
            }
        }

        public int NumInactive
        {
            get
            {
#if ZEN_MULTITHREADING
                lock (_locker)
#endif
                {
                    return _stack.Count;
                }
            }
        }

        public Type ItemType
        {
            get { return typeof(TValue); }
        }

        public void Resize(int desiredPoolSize)
        {
#if ZEN_MULTITHREADING
            lock (_locker)
#endif
            {
                ResizeInternal(desiredPoolSize);
            }
        }

        // We assume here that we're in a lock
        void ResizeInternal(int desiredPoolSize)
        {
            Assert.That(desiredPoolSize >= 0, "Attempted to resize the pool to a negative amount");

            while (_stack.Count > desiredPoolSize)
            {
                _stack.Pop();
            }

            while (desiredPoolSize > _stack.Count)
            {
                _stack.Push(Alloc());
            }

            Assert.IsEqual(_stack.Count, desiredPoolSize);
        }

        public void Dispose()
        {
#if UNITY_EDITOR
            StaticMemoryPoolRegistry.Remove(this);
#endif
        }

        public void ClearActiveCount()
        {
#if ZEN_MULTITHREADING
            lock (_locker)
#endif
            {
                _activeCount = 0;
            }
        }

        public void Clear()
        {
            Resize(0);
        }

        public void ShrinkBy(int numToRemove)
        {
#if ZEN_MULTITHREADING
            lock (_locker)
#endif
            {
                ResizeInternal(_stack.Count - numToRemove);
            }
        }

        public void ExpandBy(int numToAdd)
        {
#if ZEN_MULTITHREADING
            lock (_locker)
#endif
            {
                ResizeInternal(_stack.Count + numToAdd);
            }
        }

        // We assume here that we're in a lock
        protected TValue SpawnInternal()
        {
            TValue element;

            if (_stack.Count == 0)
            {
                element = Alloc();
            }
            else
            {
                element = _stack.Pop();
            }

            _activeCount++;
            return element;
        }

        void IMemoryPool.Despawn(object item)
        {
            Despawn((TValue)item);
        }

        public void Despawn(TValue element)
        {
            if (_onDespawnedMethod != null)
            {
                _onDespawnedMethod(element);
            }

#if ZEN_MULTITHREADING
            lock (_locker)
#endif
            {
                Assert.That(!_stack.Contains(element), "Attempted to despawn element twice!");

                _activeCount--;
                _stack.Push(element);
            }
        }

        protected abstract TValue Alloc();
    }
}