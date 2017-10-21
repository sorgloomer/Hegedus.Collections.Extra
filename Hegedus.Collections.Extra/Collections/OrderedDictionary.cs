using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hegedus.Extra.Collections.Collections
{
    public class OrderedDictionary<TKey, TValue> : IOrderedDictionary<TKey, TValue>, IReadOnlyOrderedDictionary<TKey, TValue>
    {
        private LinkedList<MutablePair<TKey, TValue>> linkedList = new LinkedList<MutablePair<TKey, TValue>>();
        private Dictionary<TKey, LinkedListNode<MutablePair<TKey, TValue>>> dictionary = new Dictionary<TKey, LinkedListNode<MutablePair<TKey, TValue>>>();

        public OrderedDictionary() { }

        public OrderedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            AddRange(items);
        }

        public TValue this[TKey key] {
            get => GetValue(key);
            set => SetValue(key, value);
        }

        public TValue GetValue(TKey key) => dictionary[key].Value.Value;

        public void SetValue(TKey key, TValue value)
        {
            if (!TryOverwriteEntryValue(key, value))
            {
                AddNewNodeUnchecked(key, value);
            }
        }

        public void SetValue(KeyValuePair<TKey, TValue> pair) => SetValue(pair.Key, pair.Value);

        public void SetValue<U>(U pair) where U : IPair<TKey, TValue> => SetValue(pair.Key, pair.Value);

        public ICollection<TKey> Keys => new KeyCollection(this);

        public ICollection<TValue> Values => new ValueCollection(this);

        public int Count => dictionary.Count;

        public bool IsReadOnly => false;

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => Keys;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => Values;

        public void Add(TKey key, TValue value)
        {
            if (!TryAdd(key, value))
            { 
                throw new ArgumentException(string.Format("An item with the same key has already been added. Key: {0}", key), "key");
            }
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items) => items.ForEach(Add);

        public void SetRange(IEnumerable<KeyValuePair<TKey, TValue>> items) => items.ForEach(SetValue);

        public void AddRange<U>(IEnumerable<U> items) where U : IPair<TKey, TValue> => items.ForEach(Add);

        public void SetRange<U>(IEnumerable<U> items) where U : IPair<TKey, TValue> => items.ForEach(SetValue);

        public bool TryAdd(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                return false;
            }
            AddNewNodeUnchecked(key, value);
            return true;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add<U>(U item) where U : IPair<TKey, TValue>
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            dictionary.Clear();
            linkedList.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (dictionary.TryGetValue(item.Key, out var node))
            {
                return EqualityComparer<TValue>.Default.Equals(node.Value.Value, item.Value);
            }
            return false;
        }

        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

        public bool ContainsValue(TValue value) => linkedList.Any(x => EqualityComparer<TValue>.Default.Equals(x.Value, value));

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            CollectionHelper.CopyTo(this, array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => linkedList.Select(v => v.ToKeyValuePair()).GetEnumerator();

        public bool Remove(TKey key)
        {
            if (dictionary.Remove(key, out var node))
            {
                linkedList.Remove(node);
                return true;
            }
            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (Contains(item))
            {
                return Remove(item);
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (dictionary.TryGetValue(key, out var node))
            {
                value = node.Value.Value;
                return true;
            }
            value = default(TValue);
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private LinkedListNode<MutablePair<TKey, TValue>> AppendNewNodeUnchecked(TKey key, TValue value)
        {
            return linkedList.AddLast(MutablePair.Of(key, value));
        }

        private bool TryOverwriteEntryValue(TKey key, TValue value)
        {
            if (dictionary.TryGetValue(key, out var node))
            {
                var temp = node.Value;
                temp.Value = value;
                node.Value = temp;
                return true;
            }
            return false;
        }

        private void AddNewNodeUnchecked(TKey key, TValue value)
        {
            dictionary[key] = AppendNewNodeUnchecked(key, value);
        }

        public class KeyCollection : ICollection<TKey>
        {
            private readonly OrderedDictionary<TKey, TValue> _parent;
            public KeyCollection(OrderedDictionary<TKey, TValue> parent)
            {
                _parent = parent;
            }

            public int Count => _parent.Count;

            public bool IsReadOnly => true;

            public bool Contains(TKey item) => _parent.ContainsKey(item);

            protected InvalidOperationException MutationUnsupported()
            {
                return new InvalidOperationException("Mutating KeyCollection is unsupported");
            }

            public void Add(TKey item) { throw MutationUnsupported(); }

            public void Clear() { throw MutationUnsupported(); }

            public void CopyTo(TKey[] array, int arrayIndex) { throw MutationUnsupported(); }


            public bool Remove(TKey item) { throw MutationUnsupported(); }

            public IEnumerator<TKey> GetEnumerator() => _parent.linkedList.Select(node => node.Key).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class ValueCollection : ICollection<TValue>
        {
            private readonly OrderedDictionary<TKey, TValue> parent;
            public ValueCollection(OrderedDictionary<TKey, TValue> parent)
            {
                this.parent = parent;
            }

            public int Count => parent.Count;

            public bool IsReadOnly => true;

            public bool Contains(TValue item) => parent.ContainsValue(item);

            protected InvalidOperationException MutationUnsupported()
            {
                return new InvalidOperationException("Mutating ValueCollection is unsupported");
            }

            public void Add(TValue item) { throw MutationUnsupported(); }

            public void Clear() { throw MutationUnsupported(); }

            public void CopyTo(TValue[] array, int arrayIndex) { CollectionHelper.CopyTo(parent.linkedList, array, arrayIndex, x => x.Value); }


            public bool Remove(TValue item) { throw MutationUnsupported(); }

            public IEnumerator<TValue> GetEnumerator() => parent.linkedList.Select(node => node.Value).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
    public static partial class OrderedDictionary
    {
        public static OrderedDictionary<K, V> From<K, V, U>(IEnumerable<U> items) where U : IPair<K, V>
        {
            var result = new OrderedDictionary<K, V>();
            result.AddRange(items);
            return result;
        }

        public static OrderedDictionary<K, V> From<K, V>(IEnumerable<Pair<K, V>> items)
        {
            return From<K, V, Pair<K, V>>(items);
        }

        public static OrderedDictionary<K, V> From<K, V>(IEnumerable<MutablePair<K, V>> items)
        {
            return From<K, V, MutablePair<K, V>>(items);
        }
    }
}
