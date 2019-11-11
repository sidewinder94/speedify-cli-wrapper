using Newtonsoft.Json;
using SpeedifyCliWrapper.Common;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifySessionStats : SpeedifyReturnedValue
    {
        [JsonProperty("bytesReceived")]
        public long BytesReceived { get; set; }

        [JsonProperty("bytesSent")]
        public long BytesSent { get; set; }

        [JsonProperty("daysSinceFirst")]
        public long DaysSinceFirst { get; set; }

        [JsonProperty("numFailovers")]
        public long NumFailovers { get; set; }

        [JsonProperty("numSessions")]
        public long NumSessions { get; set; }

        [JsonProperty("retransBytes")]
        public long RetransBytes { get; set; }

        [JsonProperty("totalConnectedMinutes")]
        public long TotalConnectedMinutes { get; set; }

        [JsonProperty("mbpsDownBenefit")]
        public decimal MbpsDownBenefit { get; set; }

        [JsonProperty("mbpsUpBenefit")]
        public decimal MbpsUpBenefit { get; set; }
    }
}