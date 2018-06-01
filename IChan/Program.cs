using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using IChan.Commands;
using Microsoft.Extensions.DependencyInjection;
using IChan.Utils.Discord;
using System.Data;
using IChan.Datas;
using IChan.Utils;
using IChan.Events.Reactions;

namespace IChan
{
    class Program
    {

        public DiscordSocketClient client;
        public CommandService commands;
        public IServiceProvider services;
        private CommandManager CommandManager;
        private ReactionManager ReactionManager;

        static void Main(string[] args)
        {
            EnvManager.GetEnv();
            DataManager.GetData();
            var program = new Program();
            program.MainAsync().GetAwaiter().GetResult();

        }

        public async Task StartUp()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();
            services = new ServiceCollection().BuildServiceProvider();
            CommandManager = new CommandManager(client, commands, services);
            ReactionManager = new ReactionManager(client);
        }

        public async Task MainAsync()
        {
            await StartUp();
            client.MessageReceived += CommandManager.CommandRecieved;
            client.ReactionAdded += ReactionManager.ReactionAdded;
            client.Ready += Ready;
            client.Log += Log;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
            await DiscordStart();
            await Task.Delay(-1);
        }

        private async Task Ready()
        {
            
        }

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        private async Task DiscordStart()
        {
            await client.LoginAsync(TokenType.Bot, EnvManager.DiscordToken);
            await client.StartAsync();
        }


    }
}
