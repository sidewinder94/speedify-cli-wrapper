using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SpeedifyCliWrapper.Converters;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyState
    {
        [JsonProperty("state")]
        [JsonConverter(typeof(EnumConverter))]
        public ConnectionState State { get; set; }
    }
}