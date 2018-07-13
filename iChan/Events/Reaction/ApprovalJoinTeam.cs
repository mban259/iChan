using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iChan.Data;

namespace iChan.Events.Reaction
{
    class ApprovalJoinTeam
    {
        private readonly List<RequestJoinTeamMessage> _requests;

        public ApprovalJoinTeam()
        {
            _requests = new List<RequestJoinTeamMessage>();
        }

        public void AddRequest(RequestJoinTeamMessage request)
        {
            _requests.Add(request);
        }

        public void RemoveRequest(int requestId)
        {
            foreach (var request in _requests)
            {
                if (request.RequestId == requestId)
                {
                    _requests.Remove(request);
                    return;
                }
            }
        }

        public bool TryGetRequestId(ulong channelId, ulong messageId, out int requestId)
        {
            //せんけいたんさく～
            foreach (var request in _requests)
            {
                if ((request.ChannelId == channelId) && (request.MessageId == messageId))
                {
                    requestId = request.RequestId;
                    return true;
                }
            }

            requestId = 0;
            return false;
        }
    }
}
