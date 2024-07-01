using UnityEngine;

namespace Zenject.Tests.Factories.BindFactory
{
    public class Foo : MonoBehaviour, IFoo
    {
        public class Factory : PlaceholderFactory<Foo>
        {
        }
    }
}
