using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace IChan.Modules
{
    class CommandManager
    {
        private readonly DiscordSocketClient Client;
        private readonly CommandService Commands;
        private readonly IServiceProvider Services;

        public CommandManager(DiscordSocketClient client, CommandService commands, IServiceProvider services)
        {
            Client = client;
            Commands = commands;
            Services = services;
        }

        public async Task CommandRecieved(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            Console.WriteLine($" {message.Channel.Name} {message.Author.Username}:{message}");

            if (message == null) return;

            int argPos = 0;

            if (!(message.HasStringPrefix(IChan.Util.Discord.Commands.Prefix, ref argPos))) return;

            var context = new CommandContext(Client, message);
            var result = await Commands.ExecuteAsync(context, argPos, Services);
            if (result.Error == CommandError.Exception)
            {
                await context.Channel.SendMessageAsync("error");
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
            else if (!result.IsSuccess)
            {
                await context.Channel.SendMessageAsync("notSuccess");
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}
