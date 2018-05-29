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
        public ulong UserId;
        public string Address;

        public User(ulong userId, string address)
        {
            UserId = userId;
            Address = address;
        }
    }
}
