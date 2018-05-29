using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Rest;

namespace IChan.Data
{
    class Idea
    {
        public int IdeaId;
        public string Text;
        public ulong MessageId;
        public User Proposer;//発案者
        public HashSet<int> Teams;//参加してるチーム(id)

        public Idea(User user, string text, int id, ulong messageId)
        {
            IdeaId = id;
            Text = text;
            MessageId = messageId;
            Proposer = user;
            Teams = new HashSet<int>();
        }
    }
}
