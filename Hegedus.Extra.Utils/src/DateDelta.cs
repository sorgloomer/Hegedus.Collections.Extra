using System;
using System.Text.RegularExpressions;

namespace Hegedus.Extra.Utils
{
    public struct DateDelta
    {
        public int Years { get; set; }
        public int Months { get; set; }
        public int Weeks { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int Milliseconds { get; set; }

        public static DateDelta operator -(DateDelta d)
        {
            return new DateDelta()
            {
                Years = -d.Years,
                Months = -d.Months,
                Weeks = -d.Weeks,
                Days = -d.Days,
                Hours = -d.Hours,
                Minutes = -d.Minutes,
                Seconds = -d.Seconds,
                Milliseconds = -d.Milliseconds,
            };
        }

        public static DateTime operator +(DateTime dateTime, DateDelta delta)
        {
            return dateTime
                .AddYears(delta.Years)
                .AddMonths(delta.Months)
                .AddDays(delta.Days)
                .AddHours(delta.Hours)
                .AddMinutes(delta.Minutes)
                .AddSeconds(delta.Seconds)
                .AddMilliseconds(delta.Milliseconds);
        }

        public static DateTime operator -(DateTime dateTime, DateDelta delta)
        {
            return dateTime + (-delta);
        }


        public static DateDelta Parse(string input)
        {
            var parser = new DateParser();
            parser.ParseAndAdd(input);
            return parser.Delta;
        }

        public class DateParser
        {
            public DateDelta Delta;

            private Regex regex = new Regex(@"([\+\-]?\d+)\s*([a-zA-Z]+)");
            private Regex zeroRegex = new Regex(@"^\s*0+\s*$");

            public void ParseAndAdd(string input)
            {
                if (!zeroRegex.IsMatch(input))
                {
                    ParseAndAddNonZero(input);
                }
            }

            private void ParseAndAddNonZero(string input)
            {
                var matches = regex.Matches(input);
                foreach (Match match in matches)
                {
                    AddComponent(match.Groups[1].Value, match.Groups[2].Value);
                }
            }

            private void AddComponent(string p1, string p2)
            {
                AddComponent(int.Parse(p1), p2);
            }

            private void AddComponent(int v, string unit)
            {
                if (unit == "y") { Delta.Years += v; return; }
                if (unit == "M") { Delta.Months += v; return; }
                if (unit == "d") { Delta.Days += v; return; }
                if (unit == "H") { Delta.Hours += v; return; }
                if (unit == "m") { Delta.Minutes += v; return; }
                if (unit == "s") { Delta.Seconds += v; return; }
                if (unit == "F") { Delta.Milliseconds += v; return; }
                throw new ArgumentOutOfRangeException("unit");
            }
        }

    }
}
