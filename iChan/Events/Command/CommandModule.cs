using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Commands.Builders;
using Discord.WebSocket;
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
    }
}
