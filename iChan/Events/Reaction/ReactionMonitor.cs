using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using iChan.API;
using iChan.Utils;

namespace iChan.Events.Reaction
{
    class ReactionMonitor
    {
        private ApprovalJoinTeam _approvalJoinTeam;

        public ReactionMonitor(ApprovalJoinTeam approvalJoinTeam)
        {
            _approvalJoinTeam = approvalJoinTeam;
        }

        internal async Task ReactionAdded(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            int requestId;
            if (_approvalJoinTeam.TryGetRequestId(arg2.Id, arg3.MessageId, out requestId))
            {
                Debug.Log($"approvaljointeam:{requestId}");
                var result = IChanClient.Instance.ApprovalJoinTeam(requestId);
                if ((bool)result["success"])
                {
                    _approvalJoinTeam.RemoveRequest(requestId);
                    Debug.Log("approval success");
                }
            }
        }

        internal async Task ReactionRemoved(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {

        }
    }
}
