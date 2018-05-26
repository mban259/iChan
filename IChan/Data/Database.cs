using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;

namespace IChan.Data
{
    static class Database
    {
        public static Dictionary<int, Idea> EnableIdeas;
        public static List<Idea> Ideas;
        public static int UnspentIdeaId;

        static Database()
        {
            EnableIdeas = Deserialize<Dictionary<int, Idea>>(Properties.Settings.Default.EnableIdeas);
            Ideas = Deserialize<List<Idea>>(Properties.Settings.Default.Ideas);
            UnspentIdeaId = Properties.Settings.Default.UnspentIdeaId;
        }
        private static string Serialize<T>(T item)
        {
            return JsonConvert.SerializeObject(item);
        }

        private static T Deserialize<T>(string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }

        public static void SaveUnspentIdeaId()
        {
            Properties.Settings.Default.UnspentIdeaId = UnspentIdeaId;
            Save();
        }

        public static void SaveEnableIdeas()
        {
            Properties.Settings.Default.EnableIdeas = Serialize(EnableIdeas);
            Save();
        }

        public static void SaveIdeas()
        {
            Properties.Settings.Default.Ideas = Serialize(Ideas);
            Save();
        }

        private static void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}