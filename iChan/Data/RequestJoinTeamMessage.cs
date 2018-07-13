using System;
using System.Collections.Generic;
using System.Text;

namespace iChan.Data
{
    class RequestJoinTeamMessage
    {
        internal int RequestId;
        internal ulong LeaderUserId;
        internal ulong ChannelId;
        internal ulong MessageId;
    }
}
