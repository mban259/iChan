using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using iChan.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace iChan.Events.Command
{
    class MessageMonitor
    {
        private CommandService CommandService;
        private DiscordSocketClient DiscordSocketClient;
        private IServiceProvider ServiceProvider;
        internal MessageMonitor(DiscordSocketClient client)
        {
            CommandService = new CommandService();
            ServiceProvider = new ServiceCollection().BuildServiceProvider();
        }

        internal async Task AddModulesAsync()
        {
            await CommandService.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        internal async Task MessageRecieved(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            int argPos = 0;
            if (!(message.HasStringPrefix(CommandString.Prefix, ref argPos))) return;
            var context = new CommandContext(DiscordSocketClient, message);
            var result = await CommandService.ExecuteAsync(context, argPos, ServiceProvider);
        }
    }
}
