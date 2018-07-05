using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using DotNetEnv;

namespace iChanServer.Utils
{
    static class EnvManager
    {
        public static string UriPrefix { get; }
        public static string MySqlUserId { get; }
        public static string MySqlPassword { get; }
        public static string MySqlServer { get; }
        public static string MySqlDatabase { get; }
        public static int Port = 8888;

        static EnvManager()
        {
            Env.Load();
            UriPrefix = Env.GetString("URI_PREFIX");
            MySqlUserId = Env.GetString("MYSQL_USERID");
            MySqlPassword = Env.GetString("MYSQL_PASSWORD");
            MySqlServer = Env.GetString("MYSQL_SERVER");
            MySqlDatabase = Env.GetString("MYSQL_DATABASE");
        }
    }
}
