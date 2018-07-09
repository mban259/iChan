using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace iChanServer.Data
{
    class Team
    {
        public int TeamId;
        public User User;
        public int IdeaId;
        public long UnixTime;
    }
}
