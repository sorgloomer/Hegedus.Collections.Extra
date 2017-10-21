using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Hegedus.Extra.Collections.Test
{
    public class Person
    {
        public int Age;
        public double Weight;
        public static Person Of(int age, double weight)
            => new Person() { Age = age, Weight = weight };
    }
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void TestMinBy()
        {
            var a = Person.Of(1, 3);
            var b = Person.Of(2, 2);
            var c = Person.Of(3, 1);

            var ps = new[] { b, a, c };

            Assert.Equal(ps.MinBy(p => p.Age), a);
            Assert.Equal(ps.MinBy(p => p.Weight), c);
        }

        [Fact]
        public void TestMaxBy()
        {
            var a = Person.Of(1, 3);
            var b = Person.Of(2, 2);
            var c = Person.Of(3, 1);

            var ps = new[] { b, a, c };

            Assert.Equal(ps.MaxBy(p => p.Age), c);
            Assert.Equal(ps.MaxBy(p => p.Weight), a);
        }

        [Fact]
        public void TestZip()
        {
            var zipped = new[] {
                new []{ 0, 1, 2 },
                new []{ 3, 4, 5, 6 },
                new []{ 7, 8, 9 },
            }.Zip().ToArray();

            Assert.Equal(zipped[0][0], 0);
            Assert.Equal(zipped[0][1], 3);
            Assert.Equal(zipped[0][2], 7);

            Assert.Equal(zipped[1][0], 1);
            Assert.Equal(zipped[1][1], 4);
            Assert.Equal(zipped[1][2], 8);

            Assert.Equal(zipped[2][0], 2);
            Assert.Equal(zipped[2][1], 5);
            Assert.Equal(zipped[2][2], 9);
        }
    }
}
