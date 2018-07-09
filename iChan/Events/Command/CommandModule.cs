using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Commands.Builders;
using Discord.WebSocket;
using iChan.API;
using iChan.Utils;

namespace iChan.Events.Command
{
    public class CommandModule : ModuleBase
    {
        [Command(CommandString.Ping)]
        internal async Task Ping()
        {
            await Context.Channel.SendMessageAsync("pong");
        }

        [Command(CommandString.Idea)]
        internal async Task Idea()
        {
            if (IdeaCommandManager.Instance.GetPhase(Context.User) == IdeaCommandPhase.None)
            {
                IdeaCommandManager.Instance.AddPendingIdea(Context.User);
                await IdeaCommandManager.Instance.RequestAddress(Context.User);
            }
            else if (!(Context as CommandContext).IsPrivate)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} アイデア登録中だよ");
            }
        }

        [Command(CommandString.CreateTeam)]
        internal async Task CreateTeam(int ideaId, string address)
        {
            var result = IChanClient.Instance.CreateTeam(Context.User, ideaId, address);
            if ((bool)result["success"])
            {
                await Context.Channel.SendMessageAsync($"ちーむさくせい:{(int)result["result"]}");
            }
            else
            {
                await Context.Channel.SendMessageAsync("なんかあったよ");
            }
        }

        [Command(CommandString.JoinTeam)]
        internal async Task JoinTeam(int teamId, string address)
        {
            var result = IChanClient.Instance.RequestJoinTeam(Context.User, teamId, address);
            if ((bool)result["success"])
            {
                await Context.Channel.SendMessageAsync($"参加申請出したよ");
            }
            else
            {
                await Context.Channel.SendMessageAsync("なんかあったよ");
            }
        }

        [Command(CommandString.Complete)]
        internal async Task Complete(int teamId)
        {
            var result = IChanClient.Instance.RequestCompleteIdea(Context.User, teamId);
            if ((bool)result["success"])
            {
                await Context.Channel.SendMessageAsync("完成申請出したよ");
            }
            else
            {
                await Context.Channel.SendMessageAsync("なんかあったよ");
            }
        }
    }
}
