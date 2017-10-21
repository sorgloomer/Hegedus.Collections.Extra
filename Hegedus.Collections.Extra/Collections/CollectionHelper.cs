using System;
using System.Collections.Generic;
using System.Text;

namespace Hegedus.Extra.Collections.Collections
{
    public static class CollectionHelper
    {
        public static void CopyTo<T>(ICollection<T> collection, T[] array, int index)
        {
            CopyTo(collection, array, index, x => x);
        }

        public static void CopyTo<T, U>(ICollection<T> collection, U[] array, int index, Func<T, U> selector)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (index < 0 || index > array.Length)
            {
                throw new IndexOutOfRangeException("index");
            }

            if (array.Length - index < collection.Count)
            {
                throw new ArgumentException("destination array is too small");
            }

            foreach (var item in collection)
            {
                array[index++] = selector(item);
            }
        }

        public static bool ContainsAll<TItem>(ICollection<TItem> collection, IEnumerable<TItem> items) => items.All(collection.Contains);
    }
}
