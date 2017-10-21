using System.Linq;
using Xunit;

namespace Hegedus.Extra.Collections.Test
{
    public class OptionalExtensionsTests
    {
        [Fact]
        public void TestNullIfNone()
        {
            Assert.Equal(Optional<string>.None.NullIfNone(), null);
            Assert.Equal(Optional.Some("foo").NullIfNone(), "foo");
        }

        [Fact]
        public void TestAsNullable()
        {
            Assert.Equal(Optional<int>.None.AsNullable(), null);
            Assert.Equal(Optional.Some(4).AsNullable(), 4);
        }

        [Fact]
        public void TestSelect()
        {
            Assert.Equal(Optional<int>.None.Select(x => x.ToString()), Optional<string>.None);
            Assert.Equal(Optional.Some(7).Select(x => x.ToString()), Optional.Some("7"));

            Assert.Equal(Optional.Some(8).Select(x => Optional<string>.None), Optional<string>.None);
        }

        [Fact]
        public void TestOr()
        {
            Assert.Equal(Optional<int>.None.Or(Optional.Some(6)), Optional.Some(6));
            Assert.Equal(Optional.Some(5).Or(Optional.Some(6)), Optional.Some(5));
            Assert.Equal(Optional<int>.None.Or(Optional<int>.None), Optional<int>.None);

            Assert.Equal(Optional<int>.None.Or(6), Optional.Some(6));
            Assert.Equal(Optional.Some(5).Or(6), Optional.Some(5));
        }

        [Fact]
        public void TestSomeValueOrDefault()
        {
            Assert.Equal(Optional.Some(4).ValueOrDefault(), 4);
            Assert.Equal(Optional.Some("baz").ValueOrDefault(), "baz");
        }

        [Fact]
        public void TestNoneValueOrDefault()
        {
            Assert.Equal(Optional<long>.None.ValueOrDefault(), 0L);
            Assert.Equal(Optional<string>.None.ValueOrDefault(), null);
        }

        [Fact]
        public void TestSelectMany()
        {
            var items = new[]
            {
                Optional<int>.None,
                Optional.Some(1),
                Optional.Some(2),
                Optional.Some(3),
                Optional<int>.None,
                Optional.Some(4),
                Optional<int>.None,
            };
            Assert.True(Enumerable.SequenceEqual(items.SelectMany<int, Optional<int>>(), new[] { 1, 2, 3, 4 }));
            Assert.True(Enumerable.SequenceEqual(items.SelectMany(), new[] { 1, 2, 3, 4 }));
        }

    }
}
