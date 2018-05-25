using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;

namespace IChan
{
    class Settings
    {
        public string DiscordToken { private set; get; }
        public Settings()
        {
            Env.Load();
        }
        public void GetSetting()
        {
            DiscordToken = Env.GetString("DISCORD_TOKEN");
        }
    }
}
