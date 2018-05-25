using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IChan.Data
{
    class Team
    {
        public int IdeaId;
        public int TeamId;
        public User Leader;
        public List<User> Menber;
    }
}
