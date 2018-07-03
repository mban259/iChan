using System;
using System.Collections.Generic;
using System.Text;
using DotNetEnv;

namespace iChan.Utils
{
    static class EnvManager
    {
        internal static readonly string DiscordToken;
        internal static readonly string URI;

        static EnvManager()
        {
            Env.Load();
            DiscordToken = Env.GetString("DISCORD_TOKEN");
            URI = Env.GetString("ICHANSERVER_URI");
        }
    }
}
