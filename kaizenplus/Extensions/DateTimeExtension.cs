using System;
using System.Runtime.InteropServices;

namespace kaizenplus.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime SystemNow()
        {
            return DateTime.UtcNow;

            //DateTime timeUtc = DateTime.UtcNow;
            //TimeZoneInfo cstZone;

            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //{
            //    cstZone = TimeZoneInfo.FindSystemTimeZoneById("Jordan Standard Time");
            //}
            //else
            //{
            //    cstZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Amman");
            //}

            //return TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
        }
    }
}