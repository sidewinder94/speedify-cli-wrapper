using Newtonsoft.Json;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyState
    {
        [JsonProperty("state")]
        public ConnectionState State { get; set; }
    }
}