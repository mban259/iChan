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
        public ulong MessageId;

        public Applicant(User user, ulong messageId)
        {
            User = user;
            MessageId = messageId;
        }
    }
}
