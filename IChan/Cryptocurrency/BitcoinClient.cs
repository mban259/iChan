using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace IChan.Cryptocurrency
{
    class BitcoinClient
    {
        private readonly string URI;
        private readonly string RPCUser;
        private readonly string RPCPassword;

        public BitcoinClient(string uri, string rpcUser, string rpcPassword)
        {
            URI = uri;
            RPCUser = rpcUser;
            RPCPassword = rpcPassword;
        }

        public JObject InvokeMethod(string method, params object[] param)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(URI);
            webRequest.Credentials = new NetworkCredential(RPCUser, RPCPassword);
            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";
            webRequest.Timeout = -1;
            JObject joe = new JObject();
            joe["jsonrpc"] = "1.0";
            joe["id"] = "1";
            joe["method"] = method;
            JArray ja = new JArray();
            foreach (var o in param)
            {
                ja.Add(o);
            }

            joe["params"] = ja;
            string json = JsonConvert.SerializeObject(joe);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            webRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            string result;
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                using (Stream str = webResponse.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(str))
                    {
                        result = sr.ReadToEnd();
                    }
                }
            }

            return JsonConvert.DeserializeObject<JObject>(result);
        }
    }
}
