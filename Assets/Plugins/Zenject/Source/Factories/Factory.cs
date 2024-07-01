using System;

namespace Zenject
{
    [Obsolete("Zenject.Factory has been renamed to PlaceholderFactory.  Zenject.Factory will be removed in future versions")]
    public class Factory<TValue> : PlaceholderFactory<TValue>
    {
    }
}