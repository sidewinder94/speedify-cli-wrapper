using Newtonsoft.Json;
using SpeedifyCliWrapper.Common;
using SpeedifyCliWrapper.Converters;
using SpeedifyCliWrapper.Enums;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyState : SpeedifyReturnedValue
    {
        [JsonProperty("state")]
        [JsonConverter(typeof(EnumConverter))]
        public ConnectionState? State { get; set; }
    }
}