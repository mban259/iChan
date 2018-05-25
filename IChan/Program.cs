using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using IChan.Util.Discord;
using IChan.Load;

namespace IChan
{
    class Program
    {
        public DiscordSocketClient client;
        public CommandService commands;
        public IServiceProvider services;
        static void Main(string[] args)
        {
            new LoadSettings().GetSetting();
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();
            services = new ServiceCollection().BuildServiceProvider();
            client.MessageReceived += CommandRecieved;
            client.Log += Log;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
            await DiscordStart();
            await Task.Delay(-1);
        }

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        private async Task DiscordStart()
        {
            await client.LoginAsync(TokenType.Bot, Settings.DiscordToken);
            await client.StartAsync();
        }

        private async Task CommandRecieved(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            Console.WriteLine($" {message.Channel.Name} {message.Author.Username}:{message}");

            if (message == null) return;

            int argPos = 0;

            if (!(message.HasStringPrefix(Commands.Prefix, ref argPos))) return;

            var context = new CommandContext(client, message);
            var result = await commands.ExecuteAsync(context, argPos, services);
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
