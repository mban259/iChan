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

namespace IChan.Datas
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

        public static T Load<T>(string directory, string name)
        {
            string dir = $"{CurrentDirectory}\\{directory}";

            string path = $"{dir}\\{name}";
            T result;
            using (StreamReader reader = new StreamReader(path))
            {
                result = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }

            return result;
        }

        public static Data LoadData(string directory, string name)
        {
            string dir = $"{CurrentDirectory}\\{directory}";

            string path = $"{dir}\\{name}";
            Data result;
            if (!File.Exists(path))
            {
                result = new Data();
                Save(result, directory, name);
                return result;
            }
            result = Load<Data>(directory, name);
            return result;
        }
    }
}