using System;

namespace Hegedus.Extra.Collections
{
    public static class OptionalExtensions
    {
        public static T? AsNullable<T>(this Optional<T> optional) where T : struct
        {
            return optional._hasValue ? (T?)optional._value : null;
        }

        public static T NullIfNone<T>(this Optional<T> optional) where T : class
        {
            return optional._hasValue ? optional._value : null;
        }

        public static Optional<U> Select<T, U>(this Optional<T> optional, Func<T, U> selector)
        {
            return optional._hasValue ? Optional.Some(selector(optional._value)) : Optional<U>.None;
        }

        public static Optional<U> Select<T, U>(this Optional<T> optional, Func<T, Optional<U>> selector)
        {
            return optional._hasValue ? selector(optional._value) : Optional<U>.None;
        }

        public static T Or<T>(this Optional<T> optional, T fallback)
        {
            return optional._hasValue ? optional._value : fallback;
        }

        public static Optional<T> Or<T>(this Optional<T> optional, Optional<T> other)
        {
            return optional._hasValue ? optional : other;
        }

        public static T ValueOrDefault<T>(this Optional<T> optional)
        {
            return Or(optional, default(T));
        }
    }
}
