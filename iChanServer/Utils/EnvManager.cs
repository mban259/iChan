using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using DotNetEnv;

namespace iChanServer.Utils
{
    static class EnvManager
    {
        public static readonly string UriPrefix;
        public static readonly string MySqlUserId;
        public static readonly string MySqlPassword;
        public static readonly string MySqlServer;
        public static readonly string MySqlDatabase;
        public static readonly int WebSocketPort;

        static EnvManager()
        {
            Env.Load();
            UriPrefix = Env.GetString("URI_PREFIX");
            MySqlUserId = Env.GetString("MYSQL_USERID");
            MySqlPassword = Env.GetString("MYSQL_PASSWORD");
            MySqlServer = Env.GetString("MYSQL_SERVER");
            MySqlDatabase = Env.GetString("MYSQL_DATABASE");
            WebSocketPort = Env.GetInt("WEBSOCKET_PORT");
        }
    }
}
