using System;
using System.Collections.Generic;
using System.Text;
using iChanServer.CryptoCurrency;
using iChanServer.Data;
using iChanServer.MySql;
using iChanServer.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace iChanServer
{
    class MethodManager
    {
        private readonly MySqlClient _mySqlClient;
        private readonly XPC _xpc;
        internal MethodManager(MySqlClient mySqlClient, XPC xpc)
        {
            _mySqlClient = mySqlClient;
            _xpc = xpc;
        }

        internal string AddIdea(JToken token)
        {
            int ideaId = _mySqlClient.GetIdeaCount();
            string ideaAddress = _xpc.GetAddress(ideaId);
            long time = UnixTime.NowUnixTime();
            Console.WriteLine("idIdea");
            var idea = new Idea()
            {
                UserName = (string)token["user"]["name"],
                UserId = (ulong)token["user"]["id"],
                UserAddress = (string)token["user"]["address"],
                Title = (string)token["title"],
                Overview = (string)token["overview"],
                Detail = (string)token["detail"],
                Id = ideaId,
                IdeaAddress = ideaAddress,
                UnixTime = time
            };
            try
            {
                _mySqlClient.SaveIdea(idea);
                var jresult = new JObject()
                {
                    new JProperty("result", ideaId),
                    new JProperty("success", true)
                };
                return JsonConvert.SerializeObject(jresult);
            }
            catch (Exception e)
            {
                return Failed("something happend");
            }
        }

        internal string Ping()
        {
            return new JObject()
            {
                ["result"] = "pong",
                ["success"] = true
            }.ToString();
        }

        public string Failed(string error)
        {
            Console.WriteLine(error);
            return JsonConvert.SerializeObject(new JObject()
            {
                ["result"] = error,
                ["success"] = false
            });
        }

        internal string UnknownMethod(string method)
        {
            return Failed($"{method} is Unknown");
        }
    }
}
