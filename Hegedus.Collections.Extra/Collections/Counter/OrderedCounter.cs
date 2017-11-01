using System.Collections.Generic;

namespace Hegedus.Extra.Collections.Collections
{
    public class OrderedCounter<TItem> : BaseCounter<TItem, OrderedDictionary<TItem, int>>
    {
        public OrderedCounter()
        {
        }

        public OrderedCounter(IEnumerable<TItem> items) : base(items)
        {
        }

        public OrderedCounter(IEnumerable<KeyValuePair<TItem, int>> pairs) : base(pairs)
        {
        }
    }

}
