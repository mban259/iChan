using System;
using System.Collections.Generic;
using System.Text;
using iChanServer.Data;
using Newtonsoft.Json.Linq;

namespace iChanServer.Utils
{
    class MyJsonConverter
    {
        internal static JObject ToBroadcastJIdea(Idea idea)
        {
            return new JObject()
            {
                new JProperty("ideaid", idea.IdeaId),
                new JProperty("user", ToBroadcastJUser(idea.User)),
                new JProperty("title", idea.Title),
                new JProperty("overview", idea.Overview),
                new JProperty("detail", idea.Detail),
                new JProperty("address", idea.IdeaAddress),
                new JProperty("time", idea.UnixTime),
                new JProperty("completed", false)
            };
        }

        internal static JObject ToBroadcastTeam(Team team)
        {
            return new JObject()
            {
                new JProperty("teamid",team.TeamId),
                new JProperty("user",ToBroadcastJUser(team.User)),
                new JProperty("ideaid",team.IdeaId),
                new JProperty("time",team.UnixTime)
            };
        }

        internal static JObject ToBroadcastRequestJoinTeamData(RequestJoinTeamData request)
        {
            return new JObject()
            {
                new JProperty("user",ToBroadcastJUser(request.User)),
                new JProperty("teamid",request.TeamId),
                new JProperty("time",UnixTime.NowUnixTime()),
                new JProperty("approved",request.Approved)
            };
        }

        internal static Idea ToIdea(JObject jReceiveIdea, int ideaId, string ideaAddress)
        {
            return new Idea()
            {
                IdeaId = ideaId,
                User = ToUser((JObject)jReceiveIdea["user"]),
                Title = (string)jReceiveIdea["title"],
                Overview = (string)jReceiveIdea["overview"],
                Detail = (string)jReceiveIdea["detail"],
                IdeaAddress = ideaAddress,
                UnixTime = UnixTime.NowUnixTime(),
                Completed = false
            };
        }

        internal static User ToUser(JObject jUser)
        {
            return new User()
            {
                UserId = (ulong)jUser["userid"],
                UserName = (string)jUser["name"],
                Address = (string)jUser["address"]
            };
        }

        internal static Team ToTeam(JObject jReceiveparam, int teamId)
        {
            return new Team()
            {
                TeamId = teamId,
                User = ToUser((JObject)jReceiveparam["user"]),
                IdeaId = (int)jReceiveparam["ideaid"],
                UnixTime = UnixTime.NowUnixTime()
            };
        }

        internal static RequestJoinTeamData ToRequestJoinTeamData(JObject jRequest)
        {
            return new RequestJoinTeamData()
            {
                User = ToUser((JObject)jRequest["user"]),
                TeamId = (int)jRequest["teamid"],
                UnixTime = UnixTime.NowUnixTime(),
                Approved = false
            };
        }

        private static JObject ToBroadcastJUser(User user)
        {
            return new JObject()
            {
                new JProperty("userid",user.UserId),
                new JProperty("name",user.UserName)
            };
        }
    }
}
