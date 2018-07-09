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
                        result = AddIdea((JObject)json["params"]);
                        break;
                    case "createteam":
                        result = CreateTeam((JObject)json["params"]);
                        break;
                    case "requestjointeam":
                        result = RequestJoinTeam((JObject)json["params"]);
                        break;
                    case "requestcompleteidea":
                        result = RequestCompleteIdea((JObject)json["params"]);
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

        internal JObject CreateTeam(JObject param)
        {
            int teamid = _mySqlClient.GetTeamCount();
            var team = MyJsonConverter.ToTeam(param, teamid);
            _mySqlClient.SaveTeam(team);
            JObject broadcastData = MyJsonConverter.ToBroadcastTeam(team);
            BroadcastObject("newteam", broadcastData);
            return Success(teamid);
        }

        internal JObject AddIdea(JObject param)
        {
            int ideaId = _mySqlClient.GetIdeaCount();
            var idea = MyJsonConverter.ToIdea(param, ideaId, _xpc.GetAddress(ideaId));
            _mySqlClient.SaveIdea(idea);
            JObject broadcastData = MyJsonConverter.ToBroadcastJIdea(idea);
            BroadcastObject("newidea", broadcastData);
            return Success(ideaId);
        }

        internal JObject RequestJoinTeam(JObject param)
        {
            var request = MyJsonConverter.ToRequestJoinTeamData(param);
            _mySqlClient.SaveRequestJoinTeamData(request);
            JObject broadcastData = MyJsonConverter.ToBroadcastRequestJoinTeamData(request);
            BroadcastObject("requestjointeam", broadcastData);
            return Success("receiverequest");
        }

        internal JObject RequestCompleteIdea(JObject param)
        {
            int ideaId = _mySqlClient.GetIdeaId((int)param["teamid"]);
            BroadcastObject("requestcompleteidea", ideaId);
            return Success("receiverequest");
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
