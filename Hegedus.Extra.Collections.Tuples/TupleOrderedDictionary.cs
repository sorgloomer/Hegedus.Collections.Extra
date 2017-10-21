using Hegedus.Extra.Collections.Collections;

namespace Hegedus.Extra.Collections.Tuples
{
    public static class TupleOrderedDictionary
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
