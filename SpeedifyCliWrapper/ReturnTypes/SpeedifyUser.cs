using Newtonsoft.Json;
using SpeedifyCliWrapper.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyUser : SpeedifyReturnedValue
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("isAutoAccount")]
        public bool IsAutoAccount { get; set; }

        [JsonProperty("isTeam")]
        public bool IsTeam { get; set; }

        [JsonProperty("bytesUsed")]
        public long BytesUsed { get; set; }

        [JsonProperty("bytesAvailable")]
        public long BytesAvailable { get; set; }
    }
}
