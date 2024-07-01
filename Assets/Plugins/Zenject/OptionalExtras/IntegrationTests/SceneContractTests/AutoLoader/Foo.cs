using ModestTree;

namespace Zenject.Tests.AutoLoadSceneTests
{
    public class Foo
    {
        public Foo(Bar bar)
        {
            Log.Trace("Created Foo");
        }
    }
}