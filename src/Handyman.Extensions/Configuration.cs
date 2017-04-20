using System;

namespace Handyman.Extensions
{
    internal static class Configuration
    {
        public static Func<DateTimeOffset> Now;

        static Configuration()
        {
            Reset();
        }

        public static void Reset()
        {
            Now = () => DateTimeOffset.Now;
        }
    }
}