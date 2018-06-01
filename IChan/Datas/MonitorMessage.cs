using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace IChan.Datas
{
    abstract class MonitorMessage
    {
        public ulong MessageId { protected set; get; }
        public Emoji UpVoteReaction { protected set; get; }
        public Emoji DownVoteReaction { protected set; get; }
        public int UpVote { set; get; }
        public int DownVote { set; get; }
        public abstract bool IsMajority();
        public abstract void EndVote();

    }
}
