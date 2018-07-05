using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace iChanServer.Data
{
    class Idea
    {
        [JsonProperty("id")]
        public int Id;
        [JsonProperty("user")]
        public User User;
        [JsonProperty("title")]
        public string Title;
        [JsonProperty("overview")]
        public string Overview;
        [JsonProperty("detail")]
        public string Detail;
        [JsonProperty("address")]
        public string IdeaAddress;
        [JsonProperty("time")]
        public long UnixTime;
        [JsonProperty("completed")]
        public bool Completed;
    }
}
