using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using IChan.Utils;

namespace IChan.Datas
{
    class IdeaVoteMessage : MonitorMessage
    {
        public int IdeaId;
        public IdeaVoteMessage(ulong messageId)
        {
            MessageId = messageId;
            UpVoteReaction = new Emoji("👎");
            DownVoteReaction = new Emoji("👍");
            UpVote = 0;
            DownVote = 0;
        }

        public override void EndVote()
        {
            Idea i;
            Saver.TryLoadIdea(IdeaId, out i);
            DataManager.Data.EnableIdea.Remove(i.IdeaId);
            DataManager.SaveData();

        }

        public override bool IsMajority()
        {
            return UpVote > DownVote + 10;

        }
    }
}
