using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Hegedus.Extra.Collections.Collections.MultiDictionary
{
    public class WrappingMultiDictionary<TKey, TValue> : IMultiDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, ICollection<TValue>> dictionary;
        private int count = 0;
        public WrappingMultiDictionary(IDictionary<TKey, ICollection<TValue>> underlyingDictionary)
        {
            dictionary = underlyingDictionary;
        }

        public ICollection<TValue> this[TKey key] => dictionary[key];

        TValue IDictionary<TKey, TValue>.this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<TKey> Keys => throw new NotImplementedException();

        public IEnumerable<ICollection<TValue>> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => throw new NotImplementedException();

        ICollection<TValue> IDictionary<TKey, TValue>.Values => throw new NotImplementedException();

        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, ICollection<TValue>>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out ICollection<TValue> value)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

}
