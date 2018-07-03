using System;
using System.Collections.Generic;
using System.Text;

namespace iChanServer.Utils
{
    static class UnixTime
    {
        public static long ToUnixTime(DateTime time)
        {
            return new DateTimeOffset(time.ToUniversalTime()).ToUnixTimeSeconds();
        }

        public static long NowUnixTime()
        {
            return ToUnixTime(DateTime.Now);
        }
    }
}
