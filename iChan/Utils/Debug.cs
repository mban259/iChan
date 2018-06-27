using System;
using System.Collections.Generic;
using System.Text;

namespace iChan.Utils
{
    static class Debug
    {
        internal static void Log(string s)
        {
            Console.WriteLine($"{DateTime.Now} {s}");
        }
    }
}
