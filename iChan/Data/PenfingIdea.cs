using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace iChan.Data
{
    class PendingIdea
    {
        internal ulong UserId { set; get; }
        internal string UserName { set; get; }
        internal string Address { set; get; }
        internal string Title { set; get; }
        internal string Overview { set; get; }
        internal string Detail { set; get; }
    }
}
