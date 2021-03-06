﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using iChan.API;
using iChan.Events.Command;
using iChan.Events.Reaction;
using iChan.Events.Socket;
using iChan.MySql;
using iChan.Utils;

namespace iChan
{
    class Program
    {
        private DiscordSocketClient _discordSocketClient;
        private MessageMonitor _messageMonitor;
        private ReactionMonitor _reactionMonitor;
        private IChanClient _iChanClient;
        private WebSocketMonitor _webSocketMonitor;
        private ApprovalJoinTeam _approvalJoinTeam;
        private MySqlClient _mySqlClient;
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
            _iChanClient = new IChanClient();
            _messageMonitor = new MessageMonitor(_discordSocketClient);
            _approvalJoinTeam = new ApprovalJoinTeam();
            _reactionMonitor = new ReactionMonitor(_approvalJoinTeam);

            _mySqlClient = new MySqlClient();
            _webSocketMonitor = new WebSocketMonitor(_discordSocketClient, _mySqlClient, _approvalJoinTeam);

        }

        public void GetEvents()
        {
            _discordSocketClient.MessageReceived += _messageMonitor.MessageRecieved;
            _discordSocketClient.Log += Log;
            _discordSocketClient.ReactionAdded += _reactionMonitor.ReactionAdded;
            _discordSocketClient.ReactionRemoved += _reactionMonitor.ReactionRemoved;
            _discordSocketClient.Ready += Ready;
        }

        private async Task Ready()
        {
            var g = _discordSocketClient.GetGuild(393028721923063808);
            var c = g.GetTextChannel(450023560677818369);
            _webSocketMonitor.AddNewIdeaNotificationChannel(c);
            await c.SendMessageAsync("aaaaa");
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
