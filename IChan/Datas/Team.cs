using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IChan.Datas
{
    class Team
    {
        public int IdeaId;
        public int TeamId;
        public User Leader;
        public List<User> Menber;

        public Team(int ideaId, int teamId, User leader)
        {
            IdeaId = ideaId;
            TeamId = teamId;
            Leader = leader;
            Menber = new List<User>();
        }
    }
}
