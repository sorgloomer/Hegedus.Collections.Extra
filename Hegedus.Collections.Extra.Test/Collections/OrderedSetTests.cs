using Hegedus.Extra.Collections.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Hegedus.Extra.Collections.Test.Collections
{
    public class OrderedSetTests
    {
        [Fact]
        public void RetainsOrder()
        {
            var s = OrderedSet.Of(1, 2, 3);
            Assert.True(Enumerable.SequenceEqual(s, new[] { 1, 2, 3 }));
            s.Add(4);
            Assert.True(Enumerable.SequenceEqual(s, new[] { 1, 2, 3, 4 }));
            s.Remove(2);
            Assert.True(Enumerable.SequenceEqual(s, new[] { 1, 3, 4 }));
            s.Add(3);
            Assert.True(Enumerable.SequenceEqual(s, new[] { 1, 3, 4 }));
            s.Remove(1);
            Assert.True(Enumerable.SequenceEqual(s, new[] { 3, 4 }));
            s.Add(5);
            Assert.True(Enumerable.SequenceEqual(s, new[] { 3, 4, 5 }));
        }

        [Fact]
        public void CanAddNewItems()
        {
            var s = OrderedSet.Of(5, 3, 1);
            Assert.False(s.Contains(2));
            s.Add(2);
            Assert.Equal(4, s.Count);
            Assert.True(s.Contains(2));
        }

        [Fact]
        public void CanAddOldItems()
        {
            var s = OrderedSet.Of(5, 3, 1);
            Assert.True(s.Contains(5));
            s.Add(5);
            Assert.Equal(3, s.Count);
            Assert.True(s.Contains(5));
        }

        [Fact]
        public void CanRemoveNewItems()
        {
            var s = OrderedSet.Of(5, 3, 1);
            Assert.False(s.Contains(2));
            s.Remove(2);
            Assert.Equal(3, s.Count);
            Assert.False(s.Contains(2));
        }

        [Fact]
        public void CanRemoveOldItems()
        {
            var s = OrderedSet.Of(5, 3, 1);
            Assert.True(s.Contains(5));
            s.Remove(5);
            Assert.Equal(2, s.Count);
            Assert.False(s.Contains(5));
        }

    }
}
