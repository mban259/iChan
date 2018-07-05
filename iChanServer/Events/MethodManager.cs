using System;
using System.Collections.Generic;
using System.Text;
using iChanServer.CryptoCurrency;
using iChanServer.Data;
using iChanServer.MySql;
using iChanServer.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp.Server;

namespace iChanServer.Events
{
    class MethodManager
    {
        private readonly MySqlClient _mySqlClient;
        private readonly XPC _xpc;
        private readonly WebSocketServiceManager _serviceManager;
        internal MethodManager(MySqlClient mySqlClient, WebSocketServiceManager serviceManager, XPC xpc)
        {
            _mySqlClient = mySqlClient;
            _xpc = xpc;
            _serviceManager = serviceManager;
        }

        internal string Response(string request)
        {
            var json = JsonConvert.DeserializeObject<JObject>(request);
            JObject result;
            try
            {
                string method = (string)json["method"];
                switch (method)
                {
                    case "addidea":
                        result = AddIdea(json["params"]);
                        break;
                    default:
                        result = UnknownMethod(method);
                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = Failed("something happend");
            }

            return result.ToString();
        }

        internal JObject AddIdea(JToken token)
        {
            var user = token["user"];
            int id = _mySqlClient.GetIdeaCount();
            Idea idea = new Idea()
            {
                Id = id,
                User = JsonConvert.DeserializeObject<User>(user.ToString()),
                Title = (string)token["title"],
                Overview = (string)token["overview"],
                Detail = (string)token["detail"],
                Completed = false,
                IdeaAddress = _xpc.GetAddress(id),
                UnixTime = UnixTime.NowUnixTime()
            };
            _mySqlClient.SaveIdea(idea);
            BroadcastObject("newidea", JsonConvert.SerializeObject(idea));
            return Success(id);
        }

        internal void BroadcastObject(string type, object data)
        {
            var jObject = new JObject()
            {
                new JProperty("type",type),
                new JProperty("data",data)
            };
            Debug.Log($"broadcast:{jObject.ToString()}");
            _serviceManager.Broadcast(jObject.ToString());
        }

        internal JObject Success(object result)
        {
            return new JObject()
            {
                new JProperty("result",result),
                new JProperty("success",true)
            };
        }

        public JObject Failed(string error)
        {
            Console.WriteLine(error);
            return new JObject()
            {
                new JProperty("result",error),
                new JProperty("success",false)
            };
        }

        internal JObject UnknownMethod(string method)
        {
            return Failed($"{method} is Unknown");
        }
    }
}
