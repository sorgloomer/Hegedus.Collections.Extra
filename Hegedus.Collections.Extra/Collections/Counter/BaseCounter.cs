using System;
using System.Collections.Generic;
using System.Text;

namespace Hegedus.Extra.Collections.Collections
{
    public class BaseCounter<TItem, TDict> : WrappingCounter<TItem> where TDict : IDictionary<TItem, int>, new()
    {
        public BaseCounter() : base(new TDict())
        {
        }

        public BaseCounter(IEnumerable<TItem> items) : this()
        {
            AddRange(items);
        }

        public BaseCounter(IEnumerable<KeyValuePair<TItem, int>> pairs) : this()
        {
            AddRange(pairs);
        }
    }
}
