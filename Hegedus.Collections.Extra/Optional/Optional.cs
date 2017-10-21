using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Hegedus.Extra.Collections
{
    public struct Optional<T> : IEnumerable<T>, IEquatable<Optional<T>>, IComparable<Optional<T>>
    {
        internal readonly bool _hasValue;
        internal readonly T _value;

        public bool HasValue => _hasValue;
        public T Value {
            get
            {
                if (!_hasValue)
                {
                    throw new MissingValueException();
                }
                return _value;
            }
        }

        public Optional<T> NoneIfNull
        {
            get
            {
                return (_hasValue && (_value != null)) ? this : None;
            }
        }

        public Optional(T value)
        {
            _hasValue = true;
            _value = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_hasValue)
            {
                yield return _value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        public bool Equals(Optional<T> other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return (obj is Optional<T>) && (this == (Optional<T>)obj);
        }

        private static string ToString(T value)
        {
            return value?.ToString() ?? "null";
        }

        public override string ToString()
        {
            return _hasValue ? string.Format("Some({0})", ToString(_value)) : "None";
        }

        public int CompareTo(Optional<T> other)
        {
            return Compare(this, other);
        }

        public static int Compare(Optional<T> a, Optional<T> b)
        {
            if (a._hasValue)
            {
                return b._hasValue ? Comparer<T>.Default.Compare(a._value, b._value) : 1;
            }
            else
            {
                return b._hasValue ? -1 : 0;
            }
        }

        public override int GetHashCode()
        {
            if (!_hasValue) return 1;
            return EqualityComparer<T>.Default.GetHashCode(_value);
        }

        public static Optional<T> Some(T value)
        {
            return new Optional<T>(value);
        }

        public static explicit operator T(Optional<T> optional)
        {
            return optional.Value;
        }

        public static implicit operator Optional<T>(T value)
        {
            return Some(value);
        }

        public static bool operator ==(Optional<T> a, Optional<T> b)
        {
            return (a._hasValue == b._hasValue) && (EqualityComparer<T>.Default.Equals(a._value, b._value));
        }

        public static bool operator !=(Optional<T> a, Optional<T> b)
        {
            return !(a == b);
        }

        public static bool operator <(Optional<T> a, Optional<T> b)
        {
            return Compare(a, b) < 0;
        }
        public static bool operator >(Optional<T> a, Optional<T> b)
        {
            return Compare(a, b) > 0;
        }

        public static bool operator <=(Optional<T> a, Optional<T> b)
        {
            return Compare(a, b) <= 0;
        }
        public static bool operator >=(Optional<T> a, Optional<T> b)
        {
            return Compare(a, b) >= 0;
        }

        public static readonly Optional<T> Empty = new Optional<T>();
        public static readonly Optional<T> None = Empty;
    }

    public static class Optional
    {
        public static Optional<T> Some<T>(T value)
        {
            return new Optional<T>(value);
        }

        public static Optional<T> None<T>()
        {
            return Optional<T>.None;
        }

        public static Optional<T> From<T>(T value)
        {
            return Some(value).NoneIfNull;
        }

        public static Optional<T> From<T>(T? value) where T : struct
        {
            return value.HasValue ? Some(value.Value) : Optional<T>.None;
        }
    }
}
