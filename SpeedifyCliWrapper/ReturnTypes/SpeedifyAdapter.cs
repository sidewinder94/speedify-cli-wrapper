using System;
using Newtonsoft.Json;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyAdapter
    {
        public class AdapterDataUsage
        {
            [JsonProperty("usageMonthly")]
            public int UsageMonthly { get; set; }
            [JsonProperty("usageDaily")]
            public int UsageDaily { get; set; }
            [JsonProperty("usageMonthlyLimit")]
            public int UsageMonthlyLimit { get; set; }
            [JsonProperty("usageMonthlyResetDay")]
            public int UsageMonthlyResetDay { get; set; }
            [JsonProperty("usageDailyLimit")]
            public int UsageDailyLimit { get; set; }
            [JsonProperty("usageDailyBoost")]
            public int UsageDailyBoost { get; set; }
            [JsonProperty("overlimitRatelimit")]
            public int OverlimitRatelimit { get; set; }
        }

        [JsonProperty("adapterID")]
        public Guid AdapterId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("state")]
        public SpeedifyState State { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("priority")]
        public Priority Priority { get; set; }
        [JsonProperty("connectedNetworkName")]
        public string ConnectedNetworkName { get; set; }
        [JsonProperty("connectedNetworkBSSID")]
        public string ConnectedNetworkBssid { get; set; }
        [JsonProperty("rateLimit")]
        public long RateLimit { get; set; }
        [JsonProperty("AdapterDataUsage")]
        public AdapterDataUsage DataUsage { get; set; }
    }
}