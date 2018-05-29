using System;
using System.Collections.Generic;
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
        public RestUserMessage Message;
        public User Proposer;
        public List<Team> Teams;

        public Idea(User user, string text, int id, RestUserMessage message)
        {

        }
    }
}
