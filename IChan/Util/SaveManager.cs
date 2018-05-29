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

namespace IChan.Data
{
    static class SaveManager
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
            var serializer = new XmlSerializer(typeof(T));
            var writer = new StreamWriter(path, false, Encoding.UTF8);
            serializer.Serialize(writer, item);
            writer.Close();
        }

        public static T Load<T>(string directory, string name)
        {
            string dir = $"{CurrentDirectory}\\{directory}";

            string path = $"{dir}\\{name}";

            var serializer = new XmlSerializer(typeof(T));
            T result;
            var reader = new StreamReader(path);

            result = (T)serializer.Deserialize(reader);
            reader.Close();

            return result;
        }

        public static Data LoadData(string directory, string name)
        {
            string dir = $"{CurrentDirectory}\\{directory}";

            string path = $"{dir}\\{name}";
            if (!File.Exists(path))
            {
                Data result = new Data();
                Save(result, directory, name);
                return result;
            }

            return Load<Data>(directory, name);
        }
    }
}