using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hegedus.Extra.Collections.Collections
{
    public static class DictionaryExtensions
    {

        public static IDictionary<K, V> RemoveRange<K, V>(this IDictionary<K, V> dict, IEnumerable<K> keys)
        { 
            keys.ForEach(dict.Remove);
            return dict;
        }

        public static IDictionary<K, V> RemoveAll<K, V>(this IDictionary<K, V> dict, Func<K, bool> predicate) 
            => RemoveRange(dict, dict.Keys.Where(predicate).ToList());

        public static IDictionary<K, V> RemoveAll<K, V>(this IDictionary<K, V> dict, Func<KeyValuePair<K, V>, bool> predicate)
            => RemoveRange(dict, dict.Where(predicate).Select(x => x.Key).ToList());

        public static IDictionary<K, V> RemoveAllValues<K, V>(this IDictionary<K, V> dict, Func<V, bool> predicate)
            => RemoveRange(dict, dict.Where(x => predicate(x.Value)).Select(x => x.Key).ToList());

        public static IDictionary<K, V> RetainRange<K, V>(this IDictionary<K, V> dict, IEnumerable<K> keys)
            => RemoveRange(dict, dict.Keys.Except(keys).ToList());

        public static IDictionary<K, V> RetainAll<K, V>(this IDictionary<K, V> dict, Func<K, bool> predicate) 
            => RemoveAll(dict, x => !predicate(x));

        public static IDictionary<K, V> RetainAll<K, V>(this IDictionary<K, V> dict, Func<KeyValuePair<K, V>, bool> predicate) 
            => RemoveAll(dict, x => !predicate(x));

        public static IDictionary<K, V> RetainAllValues<K, V>(this IDictionary<K, V> dict, Func<V, bool> predicate) 
            => RemoveAllValues(dict, x => !predicate(x));


        public static IDictionary<K, V> AddRange<K, V>(this IDictionary<K, V> dict, IEnumerable<KeyValuePair<K, V>> items)
        {
            foreach (var item in items)
            {
                dict.Add(item);
            }
            return dict;
        }

        public static Optional<V> GetOptional<K, V>(this IDictionary<K, V> dict, K key)
        {
            if (dict.TryGetValue(key, out var result))
            {
                return Optional.Some(result);
            }
            return Optional<V>.None;
        }

        public static Optional<V> Get<K, V>(this IDictionary<K, V> dict, K key) => GetOptional(dict, key);
    }
}
