using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;

namespace IChan.Utils
{
    static class EnvManager
    {
        public static string DiscordToken { private set; get; }
        public static ulong BoardChannelId { private set; get; }
        public static string SavedataDir { private set; get; }
        public static string SavedataFilename { private set; get; }
        public static string IdeaDataDir { private set; get; }
        public static ulong TeamNotificationChannel { private set; get; }
        public static string TeamDataDir { private set; get; }
        public static ulong CompleteNotificationChannel { private set; get; }
        public static void GetEnv()
        {
            Env.Load();
            DiscordToken = Env.GetString("DISCORD_TOKEN");
            BoardChannelId = ulong.Parse(Env.GetString("BOARD_CHANNEL_ID"));
            SavedataDir = Env.GetString("SAVEDATA_DIR");
            SavedataFilename = Env.GetString("SAVEDATA_FILENAME");
            IdeaDataDir = Env.GetString("IDEADATA_DIR");
            TeamNotificationChannel = ulong.Parse(Env.GetString("TEAM_NOTIFICATION_CHANNEL"));
            TeamDataDir = Env.GetString("TEAMDATA_DIR");
            CompleteNotificationChannel = ulong.Parse(Env.GetString("COMPLETE_NOTIFICATION_CHANNEL"));
        }
    }
}
