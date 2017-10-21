using System;
using System.Collections.Generic;
using System.Linq;

namespace Hegedus.Extra.Collections
{
    public static class EnumerableOptionalExtensions
    {
        public static Optional<T> AsOptional<T>(this IEnumerable<T> enumerable, bool single)
        {
            var iterator = enumerable.GetEnumerator();
            if (iterator.MoveNext())
            {
                var result = Optional<T>.Some(iterator.Current);
                if (single && iterator.MoveNext())
                {
                    throw new InvalidOperationException("SingleOptional requires at most one value");
                }
                return result;
            }
            return Optional<T>.None;
        }

        public static Optional<T> FirstOptional<T>(this IEnumerable<T> enumerable) => AsOptional(enumerable, false);

        public static Optional<T> SingleOptional<T>(this IEnumerable<T> enumerable) => AsOptional(enumerable, true);

        public static IEnumerable<T> SelectMany<T, U>(this IEnumerable<U> enumerable) where U : IEnumerable<T> 
            => enumerable.SelectMany(x => x);

        public static IEnumerable<T> SelectMany<T>(this IEnumerable<Optional<T>> enumerable)
            => enumerable.SelectMany<T, Optional<T>>();
    }
}
