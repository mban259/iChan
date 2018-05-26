using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using IChan.Util.Discord;

namespace IChan.Modules
{
    class CommandManager
    {
        private readonly DiscordSocketClient Client;
        private readonly CommandService Commands;
        private readonly IServiceProvider Services;
        private readonly IdeaCommand IdeaCommand = new IdeaCommand();
        Regex IdeaRegex = new Regex($"^{Util.Discord.Commands.Prefix}{Util.Discord.Commands.Idea}(( |\n)+?)(?<text>(\\w|\\W)*)$");

        public CommandManager(DiscordSocketClient client, CommandService commands, IServiceProvider services)
        {
            Client = client;
            Commands = commands;
            Services = services;
        }

        private async Task<bool> TaskIdeaCommand(SocketMessage message)
        {
            var match = IdeaRegex.Match(message.ToString());

            if (match.Success)
            {
                await IdeaCommand.Idea(match.Groups["text"].Value);
                return true;
            }

            return false;
        }

        public async Task CommandRecieved(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            Console.WriteLine($" {message.Channel.Name} {message.Author.Username}:{message}");

            if (message == null) return;

            int argPos = 0;

            if (!(message.HasStringPrefix(IChan.Util.Discord.Commands.Prefix, ref argPos))) return;
            //アイデア登録だけ正規表現で取得上手い方法だれかおしえて
            if (await TaskIdeaCommand(message)) return;

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
