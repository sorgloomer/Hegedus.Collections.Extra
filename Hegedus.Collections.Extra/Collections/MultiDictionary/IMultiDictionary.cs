using System.Collections.Generic;

namespace Hegedus.Extra.Collections.Collections.MultiDictionary
{
    public interface IMultiDictionary<TKey, TValue> 
        : IReadOnlyDictionary<TKey, ICollection<TValue>>,
        IDictionary<TKey, TValue> {
    }
}
