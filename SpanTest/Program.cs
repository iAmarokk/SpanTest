using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Text.RegularExpressions;

namespace SpanTest
{
    public class Program
    {

        public static void Main()
        {
            BenchmarkRunner.Run<Bench>();
        }

        [MemoryDiagnoser]
        public class Bench
        {
            private static readonly string _dateAsText = "05 01 2022";
            [Benchmark]
            public (int day, int month, int year) DateWithStringAndSubstring()
            {
                var dayAsText = _dateAsText.Substring(0, 2);
                var monthAsText = _dateAsText.Substring(3, 2);
                var yearAsText = _dateAsText.Substring(6);
                int day = int.Parse(dayAsText);
                int month = int.Parse(monthAsText);
                int year = int.Parse(yearAsText);
                return (day, month, year);
            }

            [Benchmark]
            public (int day, int month, int year) DateWithStringSpan()
            {
                ReadOnlySpan<char> dateAsSpan = _dateAsText;
                var dayAsText = dateAsSpan.Slice(0, 2);
                var monthAsText = dateAsSpan.Slice(3, 2);
                var yearAsText = dateAsSpan.Slice(6);
                var day = int.Parse(dayAsText);
                var month = int.Parse(monthAsText);
                var year = int.Parse(yearAsText);
                return (day, month, year);
            }

            [Benchmark]
            public (int day, int month, int year) DateWithStringRegex()
            {
                string[] dateAsArray = Regex.Split(_dateAsText, " ");
                var dayAsText = dateAsArray[0];
                var monthAsText = dateAsArray[1];
                var yearAsText = dateAsArray[2];
                var day = int.Parse(dayAsText);
                var month = int.Parse(monthAsText);
                var year = int.Parse(yearAsText);
                return (day, month, year);
            }
        }

    }
}