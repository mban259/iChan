using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.Rpc;
using Discord.WebSocket;
using IChan.Data;

namespace IChan.Commands
{
    class IdeaCommand
    {
        private readonly DiscordSocketClient Client;
        public IdeaCommand(DiscordSocketClient client)
        {
            Client = client;
        }

        public async Task Idea(string text, string address, IUser user)
        {
            var channel = Client.GetChannel(EnvManager.BoardChannelId) as SocketTextChannel;
            var embedBuilder = new EmbedBuilder();
            int id = DataManager.Data.UnspentIdeaId;
            string sendText = $"{id}";
            var sendmessage = await channel.SendMessageAsync(sendText, false, embedBuilder.Build());
            await AddIdea(text, address, user, sendmessage, id);
            DataManager.Data.UnspentIdeaId++;
            DataManager.SaveData();
        }

        private async Task AddIdea(string text, string address, IUser user, RestUserMessage message, int id)
        {
            User u = new User(user, address);
            Idea idea = new Idea(u, text, id, message);

        }
    }
}
