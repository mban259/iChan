using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Discord;
using iChan.Data;
using iChan.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace iChan.API
{
    class IChanClient
    {
        internal static readonly IChanClient Instance = new IChanClient();
        internal int AddIdea(IUser user, PendingIdea idea)
        {
            JObject jIdea = new JObject()
            {
                new JProperty("user",new JObject()
                {
                    new JProperty("id",user.Id),
                    new JProperty("name",user.Username),
                    new JProperty("address",idea.Address)
                }),
                new JProperty("title",idea.Title),
                new JProperty("overview",idea.Overview),
                new JProperty("detail",idea.Detail)
            };
            JObject request = new JObject()
            {
                new JProperty("method","addidea"),
                new JProperty("params",jIdea)
            };
            var result = InvokeMethod(request);
            return (int)result["result"];
        }

        internal void Ping()
        {

        }

        private JObject InvokeMethod(JObject jObject)
        {
            WebRequest webRequest = WebRequest.Create(EnvManager.URI);
            webRequest.Method = "POST";
            using (Stream stream = webRequest.GetRequestStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(stream))
                {
                    streamWriter.WriteLine(JsonConvert.SerializeObject(jObject));
                }
            }

            JObject result;
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                using (Stream stream = webResponse.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        result = JsonConvert.DeserializeObject<JObject>(streamReader.ReadToEnd());
                    }
                }
            }
            return result;
        }
    }
}
