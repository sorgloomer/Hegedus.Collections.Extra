using Hegedus.Extra.Collections.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hegedus.Extra.Collections
{
    public static class TupleEnumerableExtensions
    {
        private static IOrderedDictionary<K, V> GenerateOrderedDictionary<T, K, V>(IEnumerable<T> items, 
            Action<IOrderedDictionary<K, V>, T> setter)
        {
            return FillDictionary(new OrderedDictionary<K, V>(), items, setter);
        }

        private static IDictionary<K, V> GenerateDictionary<T, K, V>(IEnumerable<T> items,
    Action<IDictionary<K, V>, T> setter)
        {
            return FillDictionary(new Dictionary<K, V>(), items, setter);
        }

        private static D FillDictionary<D, T>(D dict, IEnumerable<T> items, Action<D, T> setter)
        {
            foreach (var item in items)
            {
                setter(dict, item);
            }
            return dict;
        }

        public static IOrderedDictionary<K, V> ToOrderedDictionary<K, V>(this IEnumerable<(K, V)> enumerable)
        {
            return GenerateOrderedDictionary<(K, V), K, V>(enumerable, (r, x) => r[x.Item1] = x.Item2);
        }

        public static IOrderedDictionary<K, V> ToOrderedDictionaryNoOverwrite<K, V>(this IEnumerable<(K, V)> enumerable)
        {
            return GenerateOrderedDictionary<(K, V), K, V>(enumerable, (r, x) => r.Add(x.Item1, x.Item2));
        }

        public static IDictionary<K, V> ToDictionary<K, V>(this IEnumerable<(K, V)> enumerable)
        {
            return GenerateDictionary<(K, V), K, V>(enumerable, (r, x) => r[x.Item1] = x.Item2);
        }

        public static IDictionary<K, V> ToDictionaryNoOverwrite<K, V>(this IEnumerable<(K, V)> enumerable)
        {
            return GenerateDictionary<(K, V), K, V>(enumerable, (r, x) => r.Add(x.Item1, x.Item2));
        }

        public static IEnumerable<(T1, T2)> Zip<T1, T2>(this IEnumerable<T1> e1, IEnumerable<T2> e2)
            => e1.Zip(e2, Extend);

        public static IEnumerable<(T1, T2, T3)> Zip<T1, T2, T3>(this IEnumerable<T1> e1, IEnumerable<T2> e2, IEnumerable<T3> e3)
            => Zip(e1, e2).Zip(e3, Extend);

        public static IEnumerable<(T1, T2, T3, T4)> Zip<T1, T2, T3, T4>(this IEnumerable<T1> e1, IEnumerable<T2> e2,
            IEnumerable<T3> e3, IEnumerable<T4> e4)
            => Zip(e1, e2, e3).Zip(e4, Extend);

        public static IEnumerable<(T1, T2, T3, T4, T5)> Zip<T1, T2, T3, T4, T5>(this IEnumerable<T1> e1, IEnumerable<T2> e2,
            IEnumerable<T3> e3, IEnumerable<T4> e4, IEnumerable<T5> e5)
            => Zip(e1, e2, e3, e4).Zip(e5, Extend);

        public static (T1, T2) Extend<T1, T2>(T1 a, T2 b) => (a, b);
        public static (T1, T2, T3) Extend<T1, T2, T3>((T1, T2) a, T3 b) => (a.Item1, a.Item2, b);
        public static (T1, T2, T3, T4) Extend<T1, T2, T3, T4>((T1, T2, T3) a, T4 b) => (a.Item1, a.Item2, a.Item3, b);
        public static (T1, T2, T3, T4, T5) Extend<T1, T2, T3, T4, T5>((T1, T2, T3, T4) a, T5 b) => (a.Item1, a.Item2, a.Item3, a.Item4, b);
    }
}
