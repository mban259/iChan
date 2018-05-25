using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using IChan.Util.Discord;

namespace IChan.Modules
{
    public class CommandModule : ModuleBase
    {
        /// <summary>
        /// ぴんぽん
        /// </summary>
        /// <returns></returns>
        [Command(Commands.Ping)]
        public async Task Ping()
        {
            await ReplyAsync("pong");
        }

        /// <summary>
        /// アイデア登録
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [Command(Commands.Idea)]
        public async Task Idea(string text)
        {

        }

        /// <summary>
        /// チーム作成
        /// </summary>
        /// <param name="ideaId"></param>
        /// <returns></returns>
        [Command(Commands.CreateTeam)]
        public async Task CreateTeam(int ideaId)
        {

        }

        /// <summary>
        /// チーム参加申請
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [Command(Commands.JoinTeam)]
        public async Task JoinTeam(int teamId)
        {

        }

        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="ideaID"></param>
        /// <returns></returns>
        [Command(Commands.Complete)]
        public async Task Complete(int ideaID)
        {

        }
    }
}
