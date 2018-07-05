using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Discord;
using iChan.Events.Command;
using iChan.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace iChan.Utils
{
    class MyJsonConverter
    {
        internal static JObject SerializePendingIdea(IUser user, PendingIdea idea)
        {
            return new JObject()
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
        }

        internal static Idea DeserializeIdea(string s)
        {
            JObject json = JsonConvert.DeserializeObject<JObject>(s);
            return DeserializeIdea(json);
        }

        internal static Idea DeserializeIdea(JObject json)
        {
            var user = json["user"];
            return new Idea()
            {
                Id = (int)json["id"],
                UserName = (string)user["name"],
                UserAddress = (string)user["address"],
                UserId = (ulong)user["id"],
                Title = (string)json["title"],
                Overview = (string)json["overview"],
                Detail = (string)json["detail"],
                IdeaAddress = (string)json["idea_address"],
                UnixTime = (long)json["time"]
            };
        }
    }
}
