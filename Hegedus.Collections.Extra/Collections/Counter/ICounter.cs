using System.Collections.Generic;

namespace Hegedus.Extra.Collections.Collections
{
    public interface ICounter<T> : IDictionary<T, int>, ICollection<T>
    {

    }
}
