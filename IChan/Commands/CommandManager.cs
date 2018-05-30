using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using IChan.Utils.Discord;

namespace IChan.Commands
{
    class CommandManager
    {
        private readonly DiscordSocketClient Client;
        private readonly CommandService Commands;
        private readonly IServiceProvider Services;
        private readonly IdeaCommand IdeaCommand;
        private readonly Regex IdeaRegex = new Regex($"^{CommandName.Prefix}{Utils.Discord.CommandName.Idea}(( |\n)+?)(?<text>(\\w|\\W)*)$");

        public CommandManager(DiscordSocketClient client, CommandService commands, IServiceProvider services)
        {
            Client = client;
            Commands = commands;
            Services = services;
            IdeaCommand = new IdeaCommand(client);
        }

        private async Task<bool> TaskIdeaCommand(SocketMessage message)
        {
            var match = IdeaRegex.Match(message.ToString());

            if (match.Success)
            {
                await IdeaCommand.Idea(match.Groups["text"].Value, "あどれす", message.Author);
                return true;
            }

            return false;
        }

        //タスクキューじゃなくていいのか?
        public async Task CommandRecieved(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            Console.WriteLine($" {message.Channel.Name} {message.Author.Username}:{message}");

            if (message == null) return;

            int argPos = 0;

            if (!(message.HasStringPrefix(CommandName.Prefix, ref argPos))) return;
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
