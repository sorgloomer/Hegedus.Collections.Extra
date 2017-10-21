using System;
using System.Collections.Generic;
using System.Text;

namespace Hegedus.Extra.Collections.Collections
{
    public static partial class OrderedDictionary
    {
        public static OrderedDictionary<K, V> Of<K, V>(params (K, V)[] items)
        {
            var result = new OrderedDictionary<K, V>();
            foreach (var item in items)
            {
                result.Add(item.Item1, item.Item2);
            }
            return result;
        }
    }
}
