using System;

namespace TemperatureFunctions.Grafana.Extensions
{
    public static class DateTimeExtensions
    {
        public static long MillisecondsFromUnixEpoch(this DateTime dateTime)
        {
            return (long) dateTime.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
        }

        public static long SecondsFromUnixEpoch(this DateTime dateTime)
        {
            return (long)dateTime.Subtract(DateTime.MinValue.AddYears(1969)).TotalSeconds;
        }
    }
}