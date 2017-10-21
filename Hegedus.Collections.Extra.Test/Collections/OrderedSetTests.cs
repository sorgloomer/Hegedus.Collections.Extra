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
    }
}
