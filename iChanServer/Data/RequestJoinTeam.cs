using System;
using System.Collections.Generic;
using System.Text;

namespace iChanServer.Data
{
    class RequestJoinTeamData
    {
        internal User User;
        internal int TeamId;
        internal long UnixTime;
        //承認された
        internal bool Approved;
    }
}
