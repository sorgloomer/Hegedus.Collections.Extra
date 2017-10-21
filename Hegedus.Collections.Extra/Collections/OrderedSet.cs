using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hegedus.Extra.Collections.Collections
{
    internal struct Void { public static readonly Void Value = new Void(); }
    public class OrderedSet<TItem> : IOrderedSet<TItem>, IReadOnlyOrderedSet<TItem>
    {
        private OrderedDictionary<TItem, Void> dictionary = new OrderedDictionary<TItem, Void>();

        public OrderedSet() { }

        public OrderedSet(IEnumerable<TItem> items) {
            UnionWith(items);
        }

        public int Count => dictionary.Count;

        public bool IsReadOnly => false;

        public bool Add(TItem item)
        {
            return dictionary.TryAdd(item, Void.Value);
        }

        public void Clear()
        {
            dictionary.Clear();
        }

        public bool Contains(TItem item)
        {
            return dictionary.ContainsKey(item);
        }

        public void CopyTo(TItem[] array, int arrayIndex)
        {
            CollectionHelper.CopyTo(this, array, arrayIndex);
        }

        public void ExceptWith(IEnumerable<TItem> other) => dictionary.RemoveRange(other);

        public IEnumerator<TItem> GetEnumerator()
        {
            return dictionary.Keys.GetEnumerator();
        }

        public void IntersectWith(IEnumerable<TItem> other)
        {
            var removing = this.ToHashSet();
            removing.ExceptWith(other);
            ExceptWith(removing);
        }

        private ISet<T> MakeSetViewOf<T>(IEnumerable<T> values)
        {
            return (values as ISet<T>) ?? values.ToHashSet();
        }

        public bool IsProperSubsetOf(IEnumerable<TItem> other)
        {
            var otherSet = MakeSetViewOf(other);
            return otherSet.Count > Count && CollectionHelper.ContainsAll(otherSet, this);
        }

        public bool IsProperSupersetOf(IEnumerable<TItem> other)
        {
            var otherSet = MakeSetViewOf(other);
            return Count > otherSet.Count && CollectionHelper.ContainsAll(this, other);
        }

        public bool IsSubsetOf(IEnumerable<TItem> other)
        {
            return CollectionHelper.ContainsAll(MakeSetViewOf(other), this);
        }

        public bool IsSupersetOf(IEnumerable<TItem> other)
        {
            return CollectionHelper.ContainsAll(this, other);
        }

        public bool Overlaps(IEnumerable<TItem> other)
        {
            // TODO: optimize
            return other.Any(Contains);
        }

        public bool Remove(TItem item)
        {
            return dictionary.Remove(item);
        }

        public bool SetEquals(IEnumerable<TItem> other)
        {
            // TODO: optimize
            return other.ToHashSet().SetEquals(this);
        }

        public void SymmetricExceptWith(IEnumerable<TItem> other) => other.ForEach(Toggle); // TODO: optimize

        private bool Toggle(TItem item)
        {
            if (Add(item))
            {
                return true;
            }
            Remove(item);
            return false;
        }

        public void UnionWith(IEnumerable<TItem> other) => other.ForEach(Add);

        void ICollection<TItem>.Add(TItem item) => Add(item);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public static class OrderedSet
    {
        public static OrderedSet<T> Of<T>(params T[] items)
        {
            return new OrderedSet<T>(items);
        }
    }
}
