using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;
using System.Xml.Serialization;
using Discord;
using IChan.Datas;

namespace IChan.Utils
{
    static class Saver
    {
        private static readonly string CurrentDirectory = Environment.CurrentDirectory;

        public static void Save<T>(T item, string directory, string name)
        {
            string dir = $"{CurrentDirectory}\\{directory}";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string path = $"{dir}\\{name}";
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.Write(JsonConvert.SerializeObject(item));
            }
        }

        public static bool TryLoad<T>(string directory, string name, out T item)
        {
            string dir = $"{CurrentDirectory}\\{directory}";

            string path = $"{dir}\\{name}";
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    item = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
                }

                return true;
            }
            else
            {
                item = default(T);
                return false;
            }
        }

        public static bool TryLoadData(out Data data)
        {
            string name = $"{EnvManager.SavedataFilename}.json";
            string dir = $"{CurrentDirectory}\\{EnvManager.SavedataDir}";

            if (TryLoad(dir, name, out data))
            {
                return true;
            }
            else
            {
                data = new Data();
                Save(data, EnvManager.SavedataDir, name);
                return false;
            }
        }

        public static bool TryLoadIdea(int ideaId, out Idea idea)
        {
            string name = $"{ideaId}.json";
            string dir = $"{CurrentDirectory}\\{EnvManager.IdeaDataDir}";
            return TryLoad(dir, name, out idea);
        }

        public static bool TryLoadTeam(int teamid, out Team team)
        {
            string name = $"{teamid}.json";
            string dir = $"{CurrentDirectory}\\{EnvManager.TeamDataDir}";
            return TryLoad(dir, name, out team);
        }
    }
}