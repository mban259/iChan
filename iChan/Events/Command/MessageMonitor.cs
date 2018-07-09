using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
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
            Debug.Log($"user:{message.Author.Username} channel:{message.Channel.Id} text:{ message.ToString()}");
            var context = new CommandContext(DiscordSocketClient, message);
            if (context.IsPrivate)
            {
                switch (IdeaCommandManager.Instance.GetPhase(message.Author))
                {
                    case IdeaCommandPhase.Address:
                        await IdeaCommandManager.Instance.ReceivedAddress(message.Author, message.ToString());
                        return;
                    case IdeaCommandPhase.Title:
                        await IdeaCommandManager.Instance.ReceivedTitle(message.Author, message.ToString());
                        return;
                    case IdeaCommandPhase.Overview:
                        await IdeaCommandManager.Instance.ReceivedOverview(message.Author, message.ToString());
                        return;
                    case IdeaCommandPhase.Detail:
                        await IdeaCommandManager.Instance.ReceivedDetail(message.Author, message.ToString());
                        return;
                }
            }
            int argPos = 0;
            if (!(message.HasStringPrefix(CommandString.Prefix, ref argPos))) return;

            var result = await CommandService.ExecuteAsync(context, argPos, ServiceProvider);
            Console.WriteLine(result);
        }
    }
}
