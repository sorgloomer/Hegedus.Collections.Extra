using System;
using Xunit;

namespace Hegedus.Extra.Utils.Test
{
    public class DateDeltaUnitTests
    {
        [Fact]
        public void Test_ParseEmpty()
        {
            var d = DateDelta.Parse("");
            Assert.Equal(0, d.Years);
            Assert.Equal(0, d.Months);
            Assert.Equal(0, d.Days);
            Assert.Equal(0, d.Hours);
            Assert.Equal(0, d.Minutes);
            Assert.Equal(0, d.Seconds);
            Assert.Equal(0, d.Milliseconds);
        }

        [Fact]
        public void Test_ParseZero()
        {
            var d = DateDelta.Parse(" 0  ");
            Assert.Equal(0, d.Years);
            Assert.Equal(0, d.Months);
            Assert.Equal(0, d.Days);
            Assert.Equal(0, d.Hours);
            Assert.Equal(0, d.Minutes);
            Assert.Equal(0, d.Seconds);
            Assert.Equal(0, d.Milliseconds);
        }

        [Fact]
        public void Test_ParseYears()
        {
            var d = DateDelta.Parse("-6y");
            Assert.Equal(-6, d.Years);
            Assert.Equal(0, d.Months);
            Assert.Equal(0, d.Days);
            Assert.Equal(0, d.Hours);
            Assert.Equal(0, d.Minutes);
            Assert.Equal(0, d.Seconds);
            Assert.Equal(0, d.Milliseconds);
        }

        [Fact]
        public void Test_ParseMonths()
        {
            var d = DateDelta.Parse("-5M");
            Assert.Equal(0, d.Years);
            Assert.Equal(-5, d.Months);
            Assert.Equal(0, d.Days);
            Assert.Equal(0, d.Hours);
            Assert.Equal(0, d.Minutes);
            Assert.Equal(0, d.Seconds);
            Assert.Equal(0, d.Milliseconds);
        }

        [Fact]
        public void Test_ParseDays()
        {
            var d = DateDelta.Parse("2d");
            Assert.Equal(0, d.Years);
            Assert.Equal(0, d.Months);
            Assert.Equal(2, d.Days);
            Assert.Equal(0, d.Hours);
            Assert.Equal(0, d.Minutes);
            Assert.Equal(0, d.Seconds);
            Assert.Equal(0, d.Milliseconds);
        }

        [Fact]
        public void Test_ParseHours()
        {
            var d = DateDelta.Parse("1H");
            Assert.Equal(0, d.Years);
            Assert.Equal(0, d.Months);
            Assert.Equal(0, d.Days);
            Assert.Equal(1, d.Hours);
            Assert.Equal(0, d.Minutes);
            Assert.Equal(0, d.Seconds);
            Assert.Equal(0, d.Milliseconds);
        }

        [Fact]
        public void Test_ParseMinutes()
        {
            var d = DateDelta.Parse("7 m");
            Assert.Equal(0, d.Years);
            Assert.Equal(0, d.Months);
            Assert.Equal(0, d.Days);
            Assert.Equal(0, d.Hours);
            Assert.Equal(7, d.Minutes);
            Assert.Equal(0, d.Seconds);
            Assert.Equal(0, d.Milliseconds);
        }

        [Fact]
        public void Test_ParseSeconds()
        {
            var d = DateDelta.Parse("91 s");
            Assert.Equal(0, d.Years);
            Assert.Equal(0, d.Months);
            Assert.Equal(0, d.Days);
            Assert.Equal(0, d.Hours);
            Assert.Equal(0, d.Minutes);
            Assert.Equal(91, d.Seconds);
            Assert.Equal(0, d.Milliseconds);
        }

        [Fact]
        public void Test_ParseMilliseconds()
        {
            var d = DateDelta.Parse("732F");
            Assert.Equal(0, d.Years);
            Assert.Equal(0, d.Months);
            Assert.Equal(0, d.Days);
            Assert.Equal(0, d.Hours);
            Assert.Equal(0, d.Minutes);
            Assert.Equal(0, d.Seconds);
            Assert.Equal(732, d.Milliseconds);
        }

        [Fact]
        public void Test_ParseComplex()
        {
            var d = DateDelta.Parse("1 y -2 M  3d2H1m -45s999F");
            Assert.Equal(1, d.Years);
            Assert.Equal(-2, d.Months);
            Assert.Equal(3, d.Days);
            Assert.Equal(2, d.Hours);
            Assert.Equal(1, d.Minutes);
            Assert.Equal(-45, d.Seconds);
            Assert.Equal(999, d.Milliseconds);
        }

        [Fact]
        public void Test_DateAddYear()
        {
            var stamp = new DateTime(1991, 7, 23, 3, 4, 5, 6);
            var delta = new DateDelta()
            {
                Years = 1
            };
            var res = stamp + delta;
            Assert.Equal(1992, res.Year);
            Assert.Equal(7, res.Month);
            Assert.Equal(23, res.Day);
            Assert.Equal(3, res.Hour);
            Assert.Equal(4, res.Minute);
            Assert.Equal(5, res.Second);
            Assert.Equal(6, res.Millisecond);
        }

        [Fact]
        public void Test_DateAddMonth()
        {
            var stamp = new DateTime(1991, 7, 23, 3, 4, 5, 6);
            var delta = new DateDelta()
            {
                Months = 2
            };
            var res = stamp + delta;
            Assert.Equal(1991, res.Year);
            Assert.Equal(9, res.Month);
            Assert.Equal(23, res.Day);
            Assert.Equal(3, res.Hour);
            Assert.Equal(4, res.Minute);
            Assert.Equal(5, res.Second);
            Assert.Equal(6, res.Millisecond);
        }

        [Fact]
        public void Test_DateAddMonthTurnover()
        {
            var stamp = new DateTime(1991, 7, 23, 3, 4, 5, 6);
            var delta = new DateDelta()
            {
                Months = 13
            };
            var res = stamp + delta;
            Assert.Equal(1992, res.Year);
            Assert.Equal(8, res.Month);
            Assert.Equal(23, res.Day);
            Assert.Equal(3, res.Hour);
            Assert.Equal(4, res.Minute);
            Assert.Equal(5, res.Second);
            Assert.Equal(6, res.Millisecond);
        }

        [Fact]
        public void Test_DateAddComplex()
        {
            var stamp = new DateTime(1991, 7, 23, 3, 4, 5, 6);
            var delta = new DateDelta()
            {
                Years = 26,
                Months = 1,
                Days = 2,
                Hours = 3,
                Minutes = 4,
                Seconds = 5,
                Milliseconds = 6,
            };
            var res = stamp + delta;
            Assert.Equal(2017, res.Year);
            Assert.Equal(8, res.Month);
            Assert.Equal(25, res.Day);
            Assert.Equal(6, res.Hour);
            Assert.Equal(8, res.Minute);
            Assert.Equal(10, res.Second);
            Assert.Equal(12, res.Millisecond);
        }
    }
}
