using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using iChan.API;
using iChan.Data;
using iChan.Utils;

namespace iChan.Events.Command
{
    class IdeaCommandManager
    {
        internal static readonly IdeaCommandManager Instance = new IdeaCommandManager();
        private readonly Dictionary<ulong, PendingIdea> _pendingIdeas;
        private readonly Dictionary<ulong, IdeaCommandPhase> _ideaCommandPhases;

        internal IdeaCommandPhase GetPhase(IUser user)
        {
            IdeaCommandPhase phase;
            if (_ideaCommandPhases.TryGetValue(user.Id, out phase))
            {
                return phase;
            }

            return IdeaCommandPhase.None;
        }

        internal IdeaCommandManager()
        {
            _pendingIdeas = new Dictionary<ulong, PendingIdea>();
            _ideaCommandPhases = new Dictionary<ulong, IdeaCommandPhase>();
        }

        internal void AddPendingIdea(IUser user)
        {
            _pendingIdeas[user.Id] = new PendingIdea();
            _ideaCommandPhases[user.Id] = IdeaCommandPhase.None;
        }

        internal async Task RequestAddress(IUser user)
        {
            await user.SendMessageAsync("アドレス?");
            _ideaCommandPhases[user.Id] = IdeaCommandPhase.Address;
        }

        internal async Task ReceivedAddress(IUser user, string address)
        {
            _pendingIdeas[user.Id].Address = address;
            await RequestTitle(user);
        }

        internal async Task RequestTitle(IUser user)
        {
            await user.SendMessageAsync("タイトル?");
            _ideaCommandPhases[user.Id] = IdeaCommandPhase.Title;
        }

        internal async Task ReceivedTitle(IUser user, string title)
        {
            _pendingIdeas[user.Id].Title = title;
            await RequestOverview(user);
        }

        internal async Task RequestOverview(IUser user)
        {
            await user.SendMessageAsync("概要?");
            _ideaCommandPhases[user.Id] = IdeaCommandPhase.Overview;
        }

        internal async Task ReceivedOverview(IUser user, string overview)
        {
            _pendingIdeas[user.Id].Overview = overview;
            await RequestDetail(user);
        }

        internal async Task RequestDetail(IUser user)
        {
            await user.SendMessageAsync("詳細?");
            _ideaCommandPhases[user.Id] = IdeaCommandPhase.Detail;
        }

        internal async Task ReceivedDetail(IUser user, string detail)
        {
            _pendingIdeas[user.Id].Detail = detail;
            await CompleteIdea(user, _pendingIdeas[user.Id]);
        }

        public async Task CompleteIdea(IUser user, PendingIdea idea)
        {
            _ideaCommandPhases[user.Id] = IdeaCommandPhase.None;

            var ideaId = IChanClient.Instance.AddIdea(user, idea);
            await user.SendMessageAsync($"あいであ登録かんりょー id:{ideaId}");
        }
    }
}
