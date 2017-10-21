using System;
using System.Linq;
using Xunit;

namespace Hegedus.Extra.Collections.Tuples.Test
{
    public class EnumerablesTests
    {
        [Fact]
        public void TestZip2()
        {
            var zipped = Enumerables.Zip(
                new[] { 0, 1 },
                new[] { 2, 3, 4 }
            ).ToArray();

            Assert.Equal(zipped.Length, 2);

            Assert.Equal(zipped[0].Item1, 0);
            Assert.Equal(zipped[0].Item2, 2);

            Assert.Equal(zipped[1].Item1, 1);
            Assert.Equal(zipped[1].Item2, 3);
        }

        [Fact]
        public void TestZip3()
        {
            var zipped = Enumerables.Zip(
                new[] { 0, 1 },
                new[] { 2, 3, 4 },
                new[] { 5, 6 }
            ).ToArray();

            Assert.Equal(zipped.Length, 2);

            Assert.Equal(zipped[0].Item1, 0);
            Assert.Equal(zipped[0].Item2, 2);
            Assert.Equal(zipped[0].Item3, 5);

            Assert.Equal(zipped[1].Item1, 1);
            Assert.Equal(zipped[1].Item2, 3);
            Assert.Equal(zipped[1].Item3, 6);
        }

        [Fact]
        public void TestZip4()
        {
            var zipped = Enumerables.Zip(
                new[] { 0, 1 },
                new[] { 2, 3 },
                new[] { "a", "b", "c" },
                new[] { 4, 5, 6, 7 }
            ).ToArray();

            Assert.Equal(zipped.Length, 2);

            Assert.Equal(zipped[0].Item1, 0);
            Assert.Equal(zipped[0].Item2, 2);
            Assert.Equal(zipped[0].Item3, "a");
            Assert.Equal(zipped[0].Item4, 4);

            Assert.Equal(zipped[1].Item1, 1);
            Assert.Equal(zipped[1].Item2, 3);
            Assert.Equal(zipped[1].Item3, "b");
            Assert.Equal(zipped[1].Item4, 5);
        }
    }
}
