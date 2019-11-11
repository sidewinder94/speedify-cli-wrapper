using JetBrains.Annotations;
using Newtonsoft.Json;
using SpeedifyCliWrapper.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyPrivacy : SpeedifyReturnedValue
    {
        [JsonProperty("CrashReports")]
        public bool CrashReports { get; set; }

        [JsonProperty("killswitch")]
        public bool KillSwitch { get; set; }

        [JsonProperty("dnsleak")]
        public bool DnsLeak { get; set; }

        [NotNull]
        [JsonProperty("dnsAddreses")]
        public List<string> DnsAddresses { get; set; } = new List<string>();
    }
}
