using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using IChan.Modules;
using Microsoft.Extensions.DependencyInjection;
using IChan.Util.Discord;

namespace IChan
{
    class Program
    {
        public DiscordSocketClient client;
        public CommandService commands;
        public IServiceProvider services;
        private Settings settings;
        private CommandManager CommandManager;
        static void Main(string[] args)
        {
            var program = new Program();
            program.MainAsync().GetAwaiter().GetResult();
        }

        public void StartUp()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();
            services = new ServiceCollection().BuildServiceProvider();
            settings = new Settings();
            settings.GetSetting();
            CommandManager = new CommandManager(client, commands, services);
        }

        public async Task MainAsync()
        {
            StartUp();
            client.MessageReceived += CommandManager.CommandRecieved;
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
            await client.LoginAsync(TokenType.Bot, settings.DiscordToken);
            await client.StartAsync();
        }


    }
}
