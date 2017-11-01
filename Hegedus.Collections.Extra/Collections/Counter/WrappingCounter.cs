using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hegedus.Extra.Collections.Collections
{

    public class WrappingCounter<TItem> : ICounter<TItem>
    {
        private IDictionary<TItem, int> dictionary;
        protected int count = 0;

        public WrappingCounter(IDictionary<TItem, int> underlyingDictionary)
        {
            dictionary = underlyingDictionary;
        }

        public long this[TItem key] => dictionary.Get(key, 0);

        public int Count => count;

        public bool IsReadOnly => false;

        public IEnumerable<TItem> Keys => dictionary.Keys;

        public IEnumerable<int> Values => dictionary.Values;

        ICollection<TItem> IDictionary<TItem, int>.Keys => dictionary.Keys;

        ICollection<int> IDictionary<TItem, int>.Values => dictionary.Values;

        int IDictionary<TItem, int>.this[TItem key] {
            get => CountOf(key);
            set
            {
                if (value < 0) throw new InvalidOperationException();
                CounterSet(key, value);
            }
        }

        protected int CountOf(TItem item) => dictionary.Get(item, 0);

        public bool CounterAdd(TItem item, int value)
        {
            var oldCount = CountOf(item);
            var newCount = oldCount + value;
            return CounterSetHelper(item, oldCount, newCount);
        }

        protected bool CounterSet(TItem item, int value)
            => CounterSetHelper(item, CountOf(item), value);

        private bool CounterSetHelper(TItem item, int oldCount, int newCount)
            => CounterSetHelperUnsafe(item, oldCount, Math.Max(newCount, 0));
        private bool CounterSetHelperUnsafe(TItem item, int oldCount, int newCount)
        {
            if (newCount == 0)
            {
                dictionary.Remove(item);
            }
            else
            {
                dictionary[item] = newCount;
            }
            count += newCount - oldCount;
            return newCount != oldCount;
        }

        protected void CounterClear()
        {
            dictionary.Clear();
            count = 0;
        }

        public void Clear()
        {
            CounterClear();
        }

        public bool Contains(TItem item) => CountOf(item) > 0;

        public bool ContainsKey(TItem key) => dictionary.ContainsKey(key);

        public void Add(TItem key, int value)
        {
            CounterAdd(key, value);
        }

        public bool Remove(TItem key)
        {
            return CounterAdd(key, -1);
        }

        public bool TryGetValue(TItem key, out int value)
        {
            value = CountOf(key);
            return true;
        }

        public void Add(KeyValuePair<TItem, int> item)
        {
            Add(item.Key, item.Value);
        }

        public bool Contains(KeyValuePair<TItem, int> item)
        {
            return CountOf(item.Key) >= item.Value;
        }

        public void CopyTo(KeyValuePair<TItem, int>[] array, int arrayIndex)
        {
            dictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TItem, int> item)
        {
            return CounterAdd(item.Key, -item.Value);
        }

        public void CopyTo(TItem[] array, int arrayIndex)
        {
            foreach (var item in this)
            {
                array[arrayIndex++] = item;
            }
        }

        protected IEnumerable<TItem> GetEnumerable()
            => dictionary.SelectMany(kv => Enumerable.Repeat(kv.Key, kv.Value));

        public IEnumerator<TItem> GetEnumerator()
            => GetEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(TItem item) => CounterAdd(item, 1);

        IEnumerator<KeyValuePair<TItem, int>> IEnumerable<KeyValuePair<TItem, int>>.GetEnumerator()
            => dictionary.GetEnumerator();

        public void AddRange(IEnumerable<TItem> items) => items.ForEach(Add);
        public void AddRange(IEnumerable<KeyValuePair<TItem, int>> pairs) => pairs.ForEach(Add);
        public void AddRange(IEnumerable<IPair<TItem, int>> pairs) => pairs.ForEach(Add);

        public void Add(IPair<TItem, int> obj) => Add(obj.Key, obj.Value);
    }
}
