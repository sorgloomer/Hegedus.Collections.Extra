using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Hegedus.Extra.Collections.Collections
{
    public interface IReadOnlySet<T> : IReadOnlyCollection<T>
    {
    }

    public interface IReadOnlyOrderedSet<T> : IReadOnlySet<T>
    {
    }

    public interface IOrderedSet<T> : ISet<T>
    {
    }

    public interface IReadOnlyOrderedDictionary<K, V> : IReadOnlyDictionary<K, V>
    {
    }

    public interface IOrderedDictionary<K, V> : IDictionary<K, V>
    {
    }
}
