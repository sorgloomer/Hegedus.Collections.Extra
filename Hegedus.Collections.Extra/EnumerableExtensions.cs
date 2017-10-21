using Hegedus.Extra.Collections.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hegedus.Extra.Collections
{
    public static class EnumerableExtensions
    {
        public static Optional<TItem> BestBy<TItem, TKey>(this IEnumerable<TItem> enumerable,
            Func<TKey, TKey, bool> secondIsBetter, Func<TItem, TKey> key)
            => enumerable.Select(x => Pair.Of(key(x), x)).BestBy(secondIsBetter);

        public static Optional<TItem> BestBy<TItem, TKey>(this IEnumerable<Pair<TKey, TItem>> enumerable, Func<TKey, TKey, bool> secondIsBetter)
        {
            var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return Optional<TItem>.None;
            }
            var best = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (secondIsBetter(best.Key, current.Key))
                {
                    best = current;
                }
            }
            return best.Value;
        }

        public static Optional<TItem> MinBy<TItem, TKey>(this IEnumerable<TItem> enumerable, Func<TItem, TKey> key)
        {
            return enumerable.BestBy((a, b) => Comparer<TKey>.Default.Compare(a, b) > 0, key);
        }

        public static Optional<TItem> MaxBy<TItem, TKey>(this IEnumerable<TItem> enumerable, Func<TItem, TKey> key)
        {
            return enumerable.BestBy((a, b) => Comparer<TKey>.Default.Compare(a, b) < 0, key);
        }

        public static IEnumerable<T> Tap<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<T> Tap<T, U>(this IEnumerable<T> enumerable, Func<T, U> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
                yield return item;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static void ForEach<T, U>(this IEnumerable<T> enumerable, Func<T, U> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        private static bool AllAnyHelper(IEnumerable<bool> enumerable, bool isAny)
        {
            foreach (var item in enumerable)
            {
                if (item == isAny)
                {
                    return isAny;
                }
            }
            return !isAny;
        }
        public static bool All(this IEnumerable<bool> enumerable)
        {
            return AllAnyHelper(enumerable, false);
        }

        public static bool All<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Select(predicate).All();
        }

        public static bool Any(this IEnumerable<bool> enumerable)
        {
            return AllAnyHelper(enumerable, true);
        }

        public static bool Any<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Select(predicate).Any();
        }

        public static U[] ToArray<T, U>(this IEnumerable<T> enumerable, Func<T, U> selector)
        {
            return enumerable.Select(selector).ToArray();
        }

        public static IList<U> ToList<T, U>(this IEnumerable<T> enumerable, Func<T, U> selector)
        {
            return enumerable.Select(selector).ToList();
        }

        public static IOrderedSet<T> ToOrderedSet<T>(this IEnumerable<T> enumerable)
        {
            return new OrderedSet<T>(enumerable);
        }

        public static IOrderedDictionary<K, V> ToOrderedDictionary<K, V>(this IEnumerable<KeyValuePair<K, V>> enumerable)
        {
            return new OrderedDictionary<K, V>(enumerable);
        }

        public static IOrderedDictionary<K, V> ToOrderedDictionary<K, V>(this IEnumerable<Pair<K, V>> enumerable)
        {
            return OrderedDictionary.From(enumerable);
        }

        public static IOrderedDictionary<K, V> ToOrderedDictionary<K, V>(this IEnumerable<MutablePair<K, V>> enumerable)
        {
            return OrderedDictionary.From(enumerable);
        }

        public static IEnumerable<T[]> Zip<T>(this IEnumerable<IEnumerable<T>> enumerables)
        {
            return Zip<T, IEnumerable<T>>(enumerables);
        }

        public static IEnumerable<T[]> Zip<T, U>(this IEnumerable<U> enumerables) where U : IEnumerable<T>
        {
            return Zip(enumerables.Select(e => e.GetEnumerator()));
        }

        public static IEnumerable<T[]> Zip<T>(this IEnumerable<IEnumerator<T>> enumerators)
        {
            var columns = enumerators.ToArray();
            for (; ; )
            {
                if (!columns.All(c => c.MoveNext()))
                {
                    yield break;
                }
                yield return columns.ToArray(e => e.Current);
            }
        }
    }
}
