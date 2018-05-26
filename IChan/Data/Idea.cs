using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IChan.Data
{
    class Idea
    {
        public int IdeaId;
        public string Text;
        public User Proposer;
        public List<Team> Teams;
    }
}
