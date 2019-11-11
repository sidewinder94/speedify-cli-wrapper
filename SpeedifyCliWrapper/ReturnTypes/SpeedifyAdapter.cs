using System;
using Newtonsoft.Json;
using SpeedifyCliWrapper.Common;
using SpeedifyCliWrapper.Converters;
using SpeedifyCliWrapper.Enums;
using SpeedifyCliWrapper.Modules;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyAdapter : SpeedifyReturnedValue
    {
        public class AdapterDataUsage
        {
            [JsonProperty("usageMonthly")]
            public long UsageMonthly { get; set; }

            [JsonProperty("usageDaily")]
            public long UsageDaily { get; set; }

            [JsonProperty("usageMonthlyLimit")]
            public long UsageMonthlyLimit { get; set; }

            [JsonProperty("usageMonthlyResetDay")]
            public long UsageMonthlyResetDay { get; set; }

            [JsonProperty("usageDailyLimit")]
            public long UsageDailyLimit { get; set; }

            [JsonProperty("usageDailyBoost")]
            public long UsageDailyBoost { get; set; }

            [JsonProperty("overlimitRatelimit")]
            public long OverlimitRatelimit { get; set; }
        }

        [JsonProperty("adapterID")]
        public Guid AdapterId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("state")]
        [JsonConverter(typeof(EnumConverter))]
        public AdapterState? State { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("priority")]
        [JsonConverter(typeof(EnumConverter))]
        public Priority? Priority { get; set; }

        [JsonProperty("connectedNetworkName")]
        public string ConnectedNetworkName { get; set; }

        [JsonProperty("connectedNetworkBSSID")]
        public string ConnectedNetworkBssid { get; set; }

        [JsonProperty("rateLimit")]
        public long RateLimit { get; set; }

        [JsonProperty("dataUsage")]
        public AdapterDataUsage DataUsage { get; set; }
    }
}