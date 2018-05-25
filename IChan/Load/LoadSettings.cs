using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;

namespace IChan.Load
{
    class LoadSettings
    {
        public LoadSettings()
        {
            Env.Load();
        }

        public void GetSetting()
        {
            Settings.DiscordToken = Env.GetString("DISCORD_TOKEN");
        }
    }
}
