using System;
using System.Collections.Generic;
using System.Text;
using DotNetEnv;

namespace iChan.Utils
{
    static class EnvManager
    {
        internal static readonly string DiscordToken;

        static EnvManager()
        {
            Env.Load();
            DiscordToken = Env.GetString("DISCORD_TOKEN");
        }
    }
}
