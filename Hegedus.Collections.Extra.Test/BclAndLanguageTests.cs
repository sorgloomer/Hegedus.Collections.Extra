using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hegedus.Extra.Collections.Test
{
    public class BclAndLanguageTests
    {
        [Fact]
        public void NullableThrows()
        {
            Assert.Throws<InvalidOperationException>(() => {
                var x = ((int?)null).Value;
            });
        }

        [Fact]
        public void EmptyEnumerableSingleThrows()
        {
            Assert.Throws<InvalidOperationException>(() => {
                (new int[0]).Single();
            });
        }

        [Fact]
        public void LongerEnumerableSingleThrows()
        {
            Assert.Throws<InvalidOperationException>(() => {
                (new int[] { 1, 2 }).Single();
            });
        }

        [Fact]
        public void LongerEnumerableSingleOrDefaultThrows()
        {
            Assert.Throws<InvalidOperationException>(() => {
                (new int[] { 1, 2 }).Single();
            });
        }

        [Fact]
        public void NullEqualsNull()
        {
            Assert.True(EqualsNull<object>(null));
        }

        [Fact]
        public void NullEqualsNullableNull()
        {
            Assert.True(EqualsNull<int?>(null));
        }

        [Fact]
        public void NullNotEqualsReferenceType()
        {
            Assert.False(EqualsNull(new DummyReference()));
        }

        [Fact]
        public void NullNotEqualsValueType()
        {
            Assert.False(EqualsNull(5));
        }

        public bool EqualsNull<T>(T value)
        {
            return null == value;
        }

        [Fact]
        public void ModifyKeySet()
        {
            IDictionary<int, string> d = new Dictionary<int, string>() {
                { 1, "x1" },
                { 2, "x2" },
                { 3, "x3" },
            };
            Assert.Throws<NotSupportedException>(() => d.Keys.Remove(2));
            Assert.True(d.ContainsKey(2));
            Assert.True(d.ContainsKey(3));
        }

        [Fact]
        public void DictionaryDoubleAdd()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Dictionary<int, string> d = new Dictionary<int, string>();
                d.Add(4, "a");
                d.Add(4, "b");
            });
        }

        [Fact]
        public void DictionaryInterfaceIsNotReadOnly() {
            Assert.Throws<InvalidCastException>(() =>
            {
                var dummy = new DummyDictionary<int, int>();
                var ro = (IReadOnlyDictionary<int, int>)dummy;
            });
        }
    }
}

