using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using iChan.API;
using iChan.Data;
using iChan.Events.Reaction;
using iChan.MySql;
using iChan.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;

namespace iChan.Events.Socket
{
    class WebSocketMonitor
    {
        private readonly DiscordSocketClient _discordSocketClient;
        private readonly MySqlClient _mySqlClient;
        private readonly WebSocket _webSocket;
        private readonly List<ISocketMessageChannel> _newIdeaNotificationChannels;
        private readonly List<ISocketMessageChannel> _newTeamNotificationChannels;
        private readonly List<ISocketMessageChannel> _requestCompleteIdeaNotificationChannels;
        private readonly ApprovalJoinTeam _approvalJoinTeam;
        public WebSocketMonitor(DiscordSocketClient discordSocketClient, MySqlClient mySqlClient, ApprovalJoinTeam approvalJoinTeam)
        {
            _webSocket = new WebSocket(EnvManager.WebsocketUrl);
            _mySqlClient = mySqlClient;
            _webSocket.Connect();
            _webSocket.OnMessage += OnMessage;
            _discordSocketClient = discordSocketClient;
            _newIdeaNotificationChannels = new List<ISocketMessageChannel>();
            _newTeamNotificationChannels = new List<ISocketMessageChannel>();
            _requestCompleteIdeaNotificationChannels = new List<ISocketMessageChannel>();
            _approvalJoinTeam = approvalJoinTeam;
        }

        public void AddNewIdeaNotificationChannel(ISocketMessageChannel channel)
        {
            _newIdeaNotificationChannels.Add(channel);
        }

        private async void OnMessage(object sender, MessageEventArgs e)
        {
            string message = e.Data;
            Console.WriteLine(message);
            var jMessage = JsonConvert.DeserializeObject<JObject>(message);
            string type = (string)jMessage["type"];
            Console.WriteLine(type);
            switch (type)
            {
                case "newidea":
                    await NewIdea((JObject)jMessage["data"]);
                    break;
                case "newteam":
                    await NewTeam((JObject)jMessage["data"]);
                    break;
                case "requestjointeam":
                    await RequestJoinTeam((JObject)jMessage["data"]);
                    break;
                case "requestcompleteidea":
                    await RequestCompleteIdea((JObject)jMessage["data"]);
                    break;
            }
        }

        private async Task RequestJoinTeam(JObject data)
        {
            ulong leaderUserId = IChanClient.Instance.GetTeamLeader((int)data["teamid"]).UserId;
            var leader = _discordSocketClient.GetUser(leaderUserId);
            var message = await leader.SendMessageAsync(data.ToString());
            _approvalJoinTeam.AddRequest(new RequestJoinTeamMessage()
            {
                ChannelId = message.Channel.Id,
                LeaderUserId = leaderUserId,
                MessageId = message.Id,
                RequestId = (int)data["requestid"]
            });
        }
        //なげぇ
        private async Task NewIdea(JObject data)
        {

            foreach (var newIdeaNotificationChannel in _newIdeaNotificationChannels)
            {
                await newIdeaNotificationChannel.SendMessageAsync(data.ToString());
            }
        }

        private async Task NewTeam(JObject data)
        {
            foreach (var newTeamNotificationChannel in _newTeamNotificationChannels)
            {
                await newTeamNotificationChannel.SendMessageAsync(data.ToString());
            }
        }

        private async Task RequestCompleteIdea(JObject data)
        {
            int ideaid = (int)data["ideaid"];
            foreach (var requestCompleteIdeaNotificationChannel in _requestCompleteIdeaNotificationChannels)
            {
                var m = await requestCompleteIdeaNotificationChannel.SendMessageAsync(data.ToString());
            }
        }
    }
}
