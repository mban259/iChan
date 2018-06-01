using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Discord;
using IChan.Utils;

namespace IChan.Datas
{
    class ApprovalMessage : MonitorMessage
    {
        public int TeamId;
        public ulong UserId;
        public string Address;
        public ApprovalMessage(ulong messageId, int teamId)
        {
            MessageId = messageId;
            UpVoteReaction = new Emoji("👍");
            DownVoteReaction = new Emoji("👎");
            UpVote = 0;
            DownVote = 0;
        }

        public override void EndVote()
        {
            Team t;
            Saver.TryLoadTeam(TeamId, out t);
            var u = new User(UserId, Address);
            t.Menber.Add(new User(UserId, Address));
            t.Applicants.Remove(t.Applicants.Find(a => a.User.Equals(u)));
            Saver.Save(t, EnvManager.TeamDataDir, $"{t.TeamId}.json");
        }

        public override bool IsMajority()
        {
            return UpVote > DownVote;

        }
    }
}
