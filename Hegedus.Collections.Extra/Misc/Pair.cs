using System;
using System.Collections.Generic;

namespace Hegedus.Extra.Collections
{
    public interface IPair<K, V>
    {
        K Key { get; }
        V Value { get; }
    }
    public struct Pair<K, V> : IPair<K, V>
    {
        public K Key { get; private set; }
        public V Value { get; private set; }
        public Pair(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public KeyValuePair<K, V> ToKeyValuePair() => new KeyValuePair<K, V>(Key, Value);
    }
    public static class Pair
    {
        public static Pair<K, V> Of<K, V>(K key, V value) => new Pair<K, V>(key, value);
    }

    public struct MutablePair<K, V> : IPair<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
        public MutablePair(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public Pair<K, V> Freezed { get => Pair.Of(Key, Value); }

        public KeyValuePair<K, V> ToKeyValuePair() => Freezed.ToKeyValuePair();
    }

    public static class MutablePair
    {
        public static MutablePair<K, V> Of<K, V>(K key, V value) => new MutablePair<K, V>(key, value);

        public static MutablePair<K, V> From<K, V>(Pair<K, V> p) => Of(p.Key, p.Value);
    }
}
