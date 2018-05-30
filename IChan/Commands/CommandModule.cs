using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using IChan.Datas;
using IChan.Util.Discord;

namespace IChan.Commands
{
    public class CommandModule : ModuleBase
    {
        /// <summary>
        /// ぴんぽん
        /// </summary>
        /// <returns></returns>
        [Command(CommandName.Ping)]
        public async Task Ping()
        {
            await ReplyAsync("pong");
        }

        /// <summary>
        /// チーム作成
        /// </summary>
        /// <param name="ideaId"></param>
        /// <returns></returns>
        [Command(CommandName.CreateTeam)]
        public async Task CreateTeam(int ideaId)
        {
            var team = new Team();
        }

        /// <summary>
        /// チーム参加申請
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [Command(CommandName.JoinTeam)]
        public async Task JoinTeam(int teamId)
        {

        }

        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="ideaId"></param>
        /// <returns></returns>
        [Command(CommandName.Complete)]
        public async Task Complete(int ideaId)
        {

        }

        [Command(CommandName.Help)]
        public async Task Help(string command)
        {
            switch (command)
            {
                case CommandName.Help:
                    break;
                case CommandName.Complete:
                    break;
                case CommandName.CreateTeam:
                    break;
                case CommandName.Idea:
                    break;
                case CommandName.JoinTeam:
                    break;
                case CommandName.Ping:
                    break;
                case CommandName.Prefix:
                    break;
                default:
                    break;
            }
        }
        [Command(CommandName.Help)]
        public async Task Help()
        {

        }
    }
}
