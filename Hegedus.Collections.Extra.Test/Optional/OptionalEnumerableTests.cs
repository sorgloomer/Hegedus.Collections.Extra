using System.Collections;
using System.Linq;
using Xunit;

namespace Hegedus.Extra.Collections.Test
{
    public class OptionalEnumerableTests
    {
        [Fact]
        public void Test_NoneEnumerableInterface()
        {
            var enumerator = ((IEnumerable)Optional<int>.None).GetEnumerator();
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void Test_NoneEnumerableGenericInterface()
        {
            Assert.True(Enumerable.SequenceEqual(Optional<int>.None, new int[] { }));
        }

        [Fact]
        public void Test_SomeEnumerableInterface()
        {
            var enumerator = ((IEnumerable)Optional.Some(2)).GetEnumerator();
            Assert.True(enumerator.MoveNext());
            Assert.Equal(enumerator.Current, 2);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void Test_SomeEnumerableGenericInterface()
        {
            Assert.True(Enumerable.SequenceEqual(Optional.Some(3), new int[] { 3 }));
        }
    }
}
