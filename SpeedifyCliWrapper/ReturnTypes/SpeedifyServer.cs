using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyServer
    {
        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("country")]
        public string CountryCode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("num")]
        public uint Num { get; set; }

        [JsonProperty("isPrivate")]
        public bool IsPrivate { get; set; }

        [JsonProperty("torrentAllowed")]
        public bool TorrentAllowed { get; set; }

        [JsonProperty("publicIP")]
        public List<string> PublicIps { get; set; }
    }
}
