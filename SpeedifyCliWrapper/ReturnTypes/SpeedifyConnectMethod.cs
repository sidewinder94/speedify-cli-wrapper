using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpeedifyCliWrapper.Converters;
using SpeedifyCliWrapper.Enums;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyConnectMethod
    {
        [JsonIgnore]
        [JsonConverter(typeof(EnumConverter))]
        public ConnectMethod? ConnectMethod { get; private set; }

        /// <summary>
        /// Gets or sets the Connect Method, in case the enum is not complete
        /// </summary>
        [JsonIgnore]
        public string ConnectMethodString { get; private set; }

        [JsonProperty("connectMethod")]
        internal JValue JInner
        {
            set
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new EnumConverter());
                ConnectMethod = value.ToObject<ConnectMethod?>(serializer);
                ConnectMethodString = value.ToString();
            }
        }


        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("num")]
        public int Num { get; set; }
    }
}
