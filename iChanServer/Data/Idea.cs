﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace iChanServer.Data
{
    class Idea
    {
        public int IdeaId;
        public User User;
        public string Title;
        public string Overview;
        public string Detail;
        public string IdeaAddress;
        public long UnixTime;
        public bool Completed;
    }
}
