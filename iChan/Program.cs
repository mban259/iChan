using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using iChan.Events.Command;
using iChan.Events.Reaction;
using iChan.Utils;

namespace iChan
{
    class Program
    {
        private DiscordSocketClient _discordSocketClient;
        private MessageMonitor _messageMonitor;
        private ReactionMonitor _reactionMonitor;
        static void Main(string[] args)
        {
            var program = new Program();
            program.Awake();
            program.GetEvents();
            program.MainAsync().GetAwaiter().GetResult();
        }

        internal void Awake()
        {
            _discordSocketClient = new DiscordSocketClient();
            _messageMonitor = new MessageMonitor(_discordSocketClient);
            _reactionMonitor = new ReactionMonitor();
        }

        public void GetEvents()
        {
            _discordSocketClient.MessageReceived += _messageMonitor.MessageRecieved;
            _discordSocketClient.Log += Log;
            _discordSocketClient.ReactionAdded += _reactionMonitor.ReactionAdded;
            _discordSocketClient.ReactionRemoved += _reactionMonitor.ReactionRemoved;
        }

        internal async Task MainAsync()
        {
            await _messageMonitor.AddModulesAsync();
            await DiscordStart();
            await Task.Delay(-1);
        }

        private async Task DiscordStart()
        {
            await _discordSocketClient.LoginAsync(TokenType.Bot, EnvManager.DiscordToken);
            await _discordSocketClient.StartAsync();
        }

        private Task Log(LogMessage message)
        {
            iChan.Utils.Debug.Log(message.Message);
            return Task.CompletedTask;
        }
    }
}
