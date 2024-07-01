using ModestTree;

namespace Zenject.Tests.DecoratorTests
{
    public class Foo
    {
        public Foo(Bar bar)
        {
            Log.Trace("Created Foo");
        }
    }
}