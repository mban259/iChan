using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IChan.Datas
{
    class Applicant
    {
        public User User;
        public ulong Id;

        public Applicant(User user, ulong id)
        {
            User = user;
            Id = id;
        }
    }
}
