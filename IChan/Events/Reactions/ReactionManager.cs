using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;

namespace IChan.Events.Reactions
{
    class ReactionManager
    {
        private readonly DiscordSocketClient Client;

        public ReactionManager(DiscordSocketClient client)
        {
            Client = client;
        }

        public async Task ReactionAdded(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            
        }

        public async Task ReactionRemoved()
        {

        }
    }
}
