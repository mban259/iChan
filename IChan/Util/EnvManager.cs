using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;

namespace IChan.Data
{
    static class EnvManager
    {
        public static string DiscordToken { private set; get; }
        public static ulong BoardChannelId { private set; get; }
        public static string SavedataDir { private set; get; }
        public static string SavedataFilename { private set; get; }
        public static void GetEnv()
        {
            Env.Load();
            DiscordToken = Env.GetString("DISCORD_TOKEN");
            BoardChannelId = ulong.Parse(Env.GetString("BOARD_CHANNEL_ID"));
            SavedataDir = Env.GetString("SAVEDATA_DIR");
            SavedataFilename = Env.GetString("SAVEDATA_FILENAME");
        }
    }
}
