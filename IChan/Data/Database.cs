using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IChan.Data
{
    class Database
    {
        private string Serialize<T>(T item)
        {
            return JsonConvert.SerializeObject(item);
        }

        private T Deserialize<T>(string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }

        public void Save<T>(T item, string path)
        {
            Save(Serialize(item), path);
        }

        public T Load<T>(string path)
        {
            return Deserialize<T>(Load(path));
        }
        //あとでかく
        private void Save(string data, string path)
        {

        }

        private string Load(string path)
        {
            return null;
        }
    }
}