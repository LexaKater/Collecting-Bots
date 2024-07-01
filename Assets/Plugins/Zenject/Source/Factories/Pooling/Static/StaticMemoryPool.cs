using System;
using ModestTree;

namespace Zenject
{
    // Zero parameters

    [NoReflectionBaking]
    public class StaticMemoryPool<TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TValue>
        where TValue : class, new()
    {
        Action<TValue> _onSpawnMethod;

        public StaticMemoryPool(
            Action<TValue> onSpawnMethod = null, Action<TValue> onDespawnedMethod = null)
            : base(onDespawnedMethod)
        {
            _onSpawnMethod = onSpawnMethod;
        }

        public Action<TValue> OnSpawnMethod
        {
            set { _onSpawnMethod = value; }
        }

        public TValue Spawn()
        {
#if ZEN_MULTITHREADING
            lock (_locker)
#endif
            {
                var item = SpawnInternal();

                if (_onSpawnMethod != null)
                {
                    _onSpawnMethod(item);
                }

                return item;
            }
        }
    }

    // One parameter

    [NoReflectionBaking]
    public class StaticMemoryPool<TParam1, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TValue>
        where TValue : class, new()
    {
        Action<TParam1, TValue> _onSpawnMethod;

        public StaticMemoryPool(
            Action<TParam1, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
            : base(onDespawnedMethod)
        {
            // What's the point of having a param otherwise?
            Assert.IsNotNull(onSpawnMethod);
            _onSpawnMethod = onSpawnMethod;
        }

        public Action<TParam1, TValue> OnSpawnMethod
        {
            set { _onSpawnMethod = value; }
        }

        public TValue Spawn(TParam1 param)
        {
#if ZEN_MULTITHREADING
            lock (_locker)
#endif
            {
                var item = SpawnInternal();

                if (_onSpawnMethod != null)
                {
                    _onSpawnMethod(param, item);
                }

                return item;
            }
        }
    }

    // Two parameter

    [NoReflectionBaking]
    public class StaticMemoryPool<TParam1, TParam2, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TValue>
        where TValue : class, new()
    {
        Action<TParam1, TParam2, TValue> _onSpawnMethod;

        public StaticMemoryPool(
            Action<TParam1, TParam2, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
            : base(onDespawnedMethod)
        {
            // What's the point of having a param otherwise?
            Assert.IsNotNull(onSpawnMethod);
            _onSpawnMethod = onSpawnMethod;
        }

        public Action<TParam1, TParam2, TValue> OnSpawnMethod
        {
            set { _onSpawnMethod = value; }
        }

        public TValue Spawn(TParam1 p1, TParam2 p2)
        {
#if ZEN_MULTITHREADING
            lock (_locker)
#endif
            {
                var item = SpawnInternal();

                if (_onSpawnMethod != null)
                {
                    _onSpawnMethod(p1, p2, item);
                }

                return item;
            }
        }
    }

    // Three parameters

    [NoReflectionBaking]
    public class StaticMemoryPool<TParam1, TParam2, TParam3, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TValue>
        where TValue : class, new()
    {
        Action<TParam1, TParam2, TParam3, TValue> _onSpawnMethod;

        public StaticMemoryPool(
            Action<TParam1, TParam2, TParam3, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
            : base(onDespawnedMethod)
        {
            // What's the point of having a param otherwise?
            Assert.IsNotNull(onSpawnMethod);
            _onSpawnMethod = onSpawnMethod;
        }

        public Action<TParam1, TParam2, TParam3, TValue> OnSpawnMethod
        {
            set { _onSpawnMethod = value; }
        }

        public TValue Spawn(TParam1 p1, TParam2 p2, TParam3 p3)
        {
#if ZEN_MULTITHREADING
            lock (_locker)
#endif
            {
                var item = SpawnInternal();

                if (_onSpawnMethod != null)
                {
                    _onSpawnMethod(p1, p2, p3, item);
                }

                return item;
            }
        }
    }

    // Four parameters

    [NoReflectionBaking]
    public class StaticMemoryPool<TParam1, TParam2, TParam3, TParam4, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TValue>
        where TValue : class, new()
    {
#if !NET_4_6
        ModestTree.Util.
#endif
            Action<TParam1, TParam2, TParam3, TParam4, TValue> _onSpawnMethod;

        public StaticMemoryPool(
#if !NET_4_6
            ModestTree.Util.
#endif
            Action<TParam1, TParam2, TParam3, TParam4, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
            : base(onDespawnedMethod)
        {
            // What's the point of having a param otherwise?
            Assert.IsNotNull(onSpawnMethod);
            _onSpawnMethod = onSpawnMethod;
        }

        public
#if !NET_4_6
            ModestTree.Util.
#endif
            Action<TParam1, TParam2, TParam3, TParam4, TValue> OnSpawnMethod
        {
            set { _onSpawnMethod = value; }
        }

        public TValue Spawn(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4)
        {
#if ZEN_MULTITHREADING
            lock (_locker)
#endif
            {
                var item = SpawnInternal();

                if (_onSpawnMethod != null)
                {
                    _onSpawnMethod(p1, p2, p3, p4, item);
                }

                return item;
            }
        }
    }

    // Five parameters

    [NoReflectionBaking]
    public class StaticMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>
        where TValue : class, new()
    {
#if !NET_4_6
        ModestTree.Util.
#endif
            Action<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> _onSpawnMethod;

        public StaticMemoryPool(
#if !NET_4_6
            ModestTree.Util.
#endif
            Action<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
            : base(onDespawnedMethod)
        {
            // What's the point of having a param otherwise?
            Assert.IsNotNull(onSpawnMethod);
            _onSpawnMethod = onSpawnMethod;
        }

        public
#if !NET_4_6
            ModestTree.Util.
#endif
            Action<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> OnSpawnMethod
        {
            set { _onSpawnMethod = value; }
        }

        public TValue Spawn(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5)
        {
#if ZEN_MULTITHREADING
            lock (_locker)
#endif
            {
                var item = SpawnInternal();

                if (_onSpawnMethod != null)
                {
                    _onSpawnMethod(p1, p2, p3, p4, p5, item);
                }

                return item;
            }
        }
    }

    // Six parameters

    [NoReflectionBaking]
    public class StaticMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>
        where TValue : class, new()
    {
#if !NET_4_6
        ModestTree.Util.
#endif
            Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> _onSpawnMethod;

        public StaticMemoryPool(
#if !NET_4_6
            ModestTree.Util.
#endif
            Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
            : base(onDespawnedMethod)
        {
            // What's the point of having a param otherwise?
            Assert.IsNotNull(onSpawnMethod);
            _onSpawnMethod = onSpawnMethod;
        }

        public
#if !NET_4_6
            ModestTree.Util.
#endif
            Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> OnSpawnMethod
        {
            set { _onSpawnMethod = value; }
        }

        public TValue Spawn(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6)
        {
#if ZEN_MULTITHREADING
            lock (_locker)
#endif
            {
                var item = SpawnInternal();

                if (_onSpawnMethod != null)
                {
                    _onSpawnMethod(p1, p2, p3, p4, p5, p6, item);
                }

                return item;
            }
        }
    }

    // Seven parameters

    [NoReflectionBaking]
    public class StaticMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue>
        where TValue : class, new()
    {
#if !NET_4_6
        ModestTree.Util.
#endif
            Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> _onSpawnMethod;

        public StaticMemoryPool(
#if !NET_4_6
            ModestTree.Util.
#endif
            Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
            : base(onDespawnedMethod)
        {
            // What's the point of having a param otherwise?
            Assert.IsNotNull(onSpawnMethod);
            _onSpawnMethod = onSpawnMethod;
        }

        public
#if !NET_4_6
            ModestTree.Util.
#endif
            Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> OnSpawnMethod
        {
            set { _onSpawnMethod = value; }
        }

        public TValue Spawn(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TParam7 p7)
        {
#if ZEN_MULTITHREADING
            lock (_locker)
#endif
            {
                var item = SpawnInternal();

                if (_onSpawnMethod != null)
                {
                    _onSpawnMethod(p1, p2, p3, p4, p5, p6, p7, item);
                }

                return item;
            }
        }
    }
}
