using Newtonsoft.Json;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyVersion
    {
        [JsonProperty("maj")]
        public int Major { get; set; }

        [JsonProperty("min")]
        public int Minor { get; set; }

        [JsonProperty("bug")]
        public int Bug { get; set; }

        [JsonProperty("build")]
        public int Build { get; set; }

        public static implicit operator System.Version(SpeedifyVersion v)
        {
            return new System.Version(v.Major, v.Minor, v.Bug, v.Build);
        }
    }
}