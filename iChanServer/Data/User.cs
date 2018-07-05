using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace iChanServer.Data
{
    [JsonObject("user")]
    struct User
    {
        [JsonProperty("id")]
        public ulong Id;
        [JsonProperty("name")]
        public string UserName;
        [JsonProperty("address")]
        public string Address;
    }
}
