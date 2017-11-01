using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hegedus.Extra.Collections.Test
{
    public class OptionalTests
    {
        [Fact]
        public void CanConstructFromReferenceType()
        {
            var value = new DummyReference();
            var x = new Optional<DummyReference>(value);
            Assert.Equal(x.Value, value);
        }

        [Fact]
        public void CanConstructFromValueType()
        {
            var x = new Optional<int>(7);
            Assert.Equal(x.Value, 7);
        }

        [Fact]
        public void CanConstructFromNullableType()
        {
            var x = new Optional<int?>(7);
            Assert.Equal(x.Value, 7);
        }

        [Fact]
        public void CanCastFromReferenceType()
        {
            var value = new DummyReference();
            var x = (Optional<DummyReference>)value;
            Assert.Equal(x.Value, value);
        }

        [Fact]
        public void CanCastFromValueType()
        {
            var x = (Optional<int>)9;
            Assert.Equal(x.Value, 9);
        }

        [Fact]
        public void CanCastFromNullableType()
        {
            var x = (Optional<int>)(int?)5;
            Assert.Equal(x.Value, 5);
        }

        [Fact]
        public void EqualityTests()
        {
            var Null = new Optional<DummyReference>(null);
            var ValA = new Optional<DummyReference>(new DummyReference());
            var ValB = new Optional<DummyReference>(new DummyReference());
            var None = Optional<DummyReference>.None;

#pragma warning disable CS1718 // Comparison made to same variable
            Assert.True(Null == Null);
            Assert.True(ValA != Null);
            Assert.True(ValA == ValA);
            Assert.True(ValA != ValB);
            Assert.True(None != Null);
            Assert.True(None != ValA);
            Assert.True(None == None);

            Assert.False(Null != Null);
            Assert.False(ValA == Null);
            Assert.False(ValA != ValA);
            Assert.False(ValA == ValB);
            Assert.False(None == Null);
            Assert.False(None == ValA);
            Assert.False(None != None);

            Assert.@True(Null.Equals(Null));
            Assert.False(ValA.Equals(Null));
            Assert.@True(ValA.Equals(ValA));
            Assert.False(ValA.Equals(ValB));
            Assert.False(None.Equals(Null));
            Assert.False(None.Equals(ValA));
            Assert.@True(None.Equals(None));
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [Fact]
        public void TestEqualityUses()
        {
            Assert.NotEqual(Optional<int>.None, 6);
            Assert.Equal(Optional.Some(7), 7);
        }

        [Fact]
        public void ComparisonTests()
        {
            var Val1 = new Optional<int>(1);
            var Val2 = new Optional<int>(2);

#pragma warning disable CS1718 // Comparison made to same variable
            Assert.False(Val1 < Val1);
            Assert.False(Val1 > Val1);
            Assert.@True(Val1 <= Val1);
            Assert.@True(Val1 >= Val1);

            Assert.@True(Val1 < Val2);
            Assert.False(Val2 < Val1);
            Assert.False(Val1 > Val2);
            Assert.@True(Val2 > Val1);
            Assert.@True(Val1 <= Val2);
            Assert.False(Val2 <= Val1);
            Assert.False(Val1 >= Val2);
            Assert.@True(Val2 >= Val1);
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [Fact]
        public void ComparisonTestsWithNone()
        {
            var Val1 = new Optional<int>(1);
            var None = Optional<int>.None;

#pragma warning disable CS1718 // Comparison made to same variable
            Assert.False(None < None);
            Assert.False(None > None);
            Assert.@True(None <= None);
            Assert.@True(None >= None);

            Assert.@True(None < Val1);
            Assert.False(Val1 < None);
            Assert.False(None > Val1);
            Assert.@True(Val1 > None);
            Assert.@True(None <= Val1);
            Assert.False(Val1 <= None);
            Assert.False(None >= Val1);
            Assert.@True(Val1 >= None);
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [Fact]
        public void TestSorting()
        {
            var array = new[] {
                Optional<int>.None,
                Optional.From(5),
                Optional.From(4),
                Optional.From(2),
                Optional.From(4),
                Optional<int>.None,
                Optional.From(7),
                Optional.From(4),
                Optional.From(5),
                Optional.From(2),
            };
            var expectedOrder = new[] {
                Optional<int>.None,
                Optional<int>.None,
                Optional.From(2),
                Optional.From(2),
                Optional.From(4),
                Optional.From(4),
                Optional.From(4),
                Optional.From(5),
                Optional.From(5),
                Optional.From(7),
            };
            Array.Sort(array);
            Assert.True(Enumerable.SequenceEqual(array, expectedOrder));
        }

        [Fact]
        public void ConstructUsingFrom()
        {
            Assert.True(Optional.From("foo") == new Optional<string>("foo"));
        }

        [Fact]
        public void FromNullBecomesNone()
        {
            Assert.True(Optional.From<string>(null) == Optional<string>.None);
        }

        [Fact]
        public void ConstructUsingSome()
        {
            Assert.True(Optional.Some("bar") == new Optional<string>("bar"));
        }

        [Fact]
        public void SomeNullDoesNotBecomeNone()
        {
            Assert.True(Optional.Some<string>(null) != Optional<string>.None);
        }

        [Fact]
        public void TestNoneIfNull()
        {
            Assert.True(Optional.Some<string>(null).NoneIfNull == Optional<string>.None);
        }

        [Fact]
        public void TestSomeValue()
        {
            Assert.Equal(Optional.Some(4).Value, 4);
        }

        [Fact]
        public void TestNoneValueThrows()
        {
            Assert.Throws<MissingValueException>(() => Optional<int>.None.Value);
        }

        [Fact]
        public void TestToString()
        {
            Assert.Equal("None", Optional<long>.None.ToString());
            Assert.Equal("Some(5)", Optional.Some(5).ToString());
            Assert.Equal("Some(bar)", Optional.Some("bar").ToString());
            Assert.Equal("Some(null)", Optional.Some<string>(null).ToString());
        }

        [Fact]
        public void TestCount()
        {
            Assert.Equal(0, Optional<long>.None.Count);
            Assert.Equal(1, Optional.Some(5).Count);
            Assert.Equal(1, Optional.Some<string>(null).Count);
        }

        [Fact]
        public void TestContains()
        {
            Assert.False(Optional<int>.None.Contains(0));
            Assert.False(Optional<int>.None.Contains(1));
            Assert.True(Optional.Some(0).Contains(0));
            Assert.False(Optional.Some(0).Contains(1));
            Assert.True(Optional.Some<string>(null).Contains(null));
        }

        [Fact]
        public void TestIsReadOnly()
        {
            Assert.True(Optional<long>.None.IsReadOnly);
            Assert.True(Optional.Some(5).IsReadOnly);
            Assert.True(Optional.Some<DummyReference>(null).IsReadOnly);
        }

        [Fact]
        public void TestGetHashCode()
        {
            Assert.Equal(Optional<long>.None.GetHashCode(), Optional<long>.None.GetHashCode());
            Assert.Equal(Optional.Some(9).GetHashCode(), Optional.Some(9).GetHashCode());
            Assert.NotEqual(Optional.Some(9).GetHashCode(), Optional<int>.None.GetHashCode());
            Assert.NotEqual(Optional.Some(8).GetHashCode(), Optional.Some(9).GetHashCode());
        }

        [Fact]
        public void TestIsReadonlyCollection()
        {
            ICollection<string> singleton = Optional.Some("b");
            Assert.Equal(true, singleton.IsReadOnly);
            Assert.ThrowsAny<NotImplementedException>(() => singleton.Remove("a"));
            Assert.ThrowsAny<NotImplementedException>(() => singleton.Add("a"));
        }
    }
}
