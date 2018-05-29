using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace IChan.Data
{
    class User
    {
        public IUser IUser;
        public string Address;

        public User(IUser user, string address)
        {
            IUser = user;
            Address = address;
        }
    }
}
