using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.Rpc;
using Discord.WebSocket;
using IChan.Datas;
using IChan.Utils;

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
            string sendText = $"{id} {user.Mention}";
            var sendmessage = await channel.SendMessageAsync(sendText, false, embedBuilder.Build());
            AddIdea(text, address, user.Id, sendmessage, id);
            DataManager.Data.UnspentIdeaId++;
            
            DataManager.SaveData();
        }

        private void AddIdea(string text, string address, ulong userId, RestUserMessage message, int id)
        {
            User u = new User(userId, address);
            Idea idea = new Idea(u, text, id, message.Id);

            //何のエラー吐こう
            if (!DataManager.Data.EnableIdea.Add(id)) throw new Exception();

            Saver.Save(idea, EnvManager.IdeaDataDir, $"{id}.xml");
        }
    }
}
