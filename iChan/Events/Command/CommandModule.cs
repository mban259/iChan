using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
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
    }
}
