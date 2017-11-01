using System.Collections.Generic;

namespace Hegedus.Extra.Collections.Collections
{
    public class Counter<TItem> : BaseCounter<TItem, Dictionary<TItem, int>>
    {
        public Counter()
        {
        }

        public Counter(IEnumerable<TItem> items) : base(items)
        {
        }

        public Counter(IEnumerable<KeyValuePair<TItem, int>> pairs) : base(pairs)
        {
        }
    }
}
