using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using IChan.Utils;

namespace IChan.Datas
{
    class CompleteVoteMessage : MonitorMessage
    {
        public int TeamId;
        public CompleteVoteMessage(ulong messageId,int teamId)
        {
            MessageId = messageId;
            UpVoteReaction = new Emoji("👍");
            DownVoteReaction = new Emoji("👎");
            UpVote = 0;
            DownVote = 0;
            TeamId = teamId;
        }

        public override void EndVote()
        {
            //ぶんぱいはあとで
            Team t;
            Saver.TryLoadTeam(TeamId, out t);
            Idea i;
            Saver.TryLoadIdea(t.IdeaId, out i);
            DataManager.Data.EnableIdea.Remove(i.IdeaId);
            DataManager.SaveData();
        }

        public override bool IsMajority()
        {
            return UpVote > DownVote + 10;
        }
    }
}
