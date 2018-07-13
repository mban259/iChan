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

        internal User GetTeamLeader(int teamId)
        {
            return new User()
            {
                Address = null,
                UserId = 392568520501624832,
                UserName = "mban"
            };
        }

        internal int AddIdea(IUser user, PendingIdea idea)
        {
            JObject jIdea = new JObject()
            {
                new JProperty("user",new JObject()
                {
                    new JProperty("userid",user.Id),
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

        internal JObject CreateTeam(IUser user, int ideaId, string leaderAddress)
        {
            JObject request = new JObject()
            {
                new JProperty("method","createteam"),
                new JProperty("params",new JObject()
                {
                    new JProperty("user",new JObject()
                    {
                        new JProperty("userid",user.Id),
                        new JProperty("name",user.Username),
                        new JProperty("address",leaderAddress)
                    }),
                    new JProperty("ideaid",ideaId)
                })
            };
            return InvokeMethod(request);
        }

        internal JObject ApprovalJoinTeam(int requestId)
        {
            JObject request = new JObject()
            {
                new JProperty("method","approvalrequestjointeam"),
                new JProperty("params",requestId)
            };
            return InvokeMethod(request);
        }

        internal JObject RequestJoinTeam(IUser user, int teamId, string address)
        {
            JObject request = new JObject()
            {
                new JProperty("method","requestjointeam"),
                new JProperty("params",new JObject()
                {
                    new JProperty("user",new JObject()
                    {
                        new JProperty("userid",user.Id),
                        new JProperty("name",user.Username),
                        new JProperty("address",address)
                    }),
                    new JProperty("teamid",teamId)
                })
            };
            return InvokeMethod(request);
        }

        internal JObject RequestCompleteIdea(IUser user, int teamId)
        {
            JObject request = new JObject()
            {
                new JProperty("method","requestcompleteidea"),
                new JProperty("params",new JObject()
                {
                    new JProperty("user",new JObject()
                    {
                        new JProperty("userid",user.Id),
                        new JProperty("name",user.Username),
                    }),
                    new JProperty("teamid",teamId)
                })
            };
            return InvokeMethod(request);
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
