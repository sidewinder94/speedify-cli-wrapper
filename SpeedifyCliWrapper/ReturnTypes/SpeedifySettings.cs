using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpeedifyCliWrapper.Converters;
using SpeedifyCliWrapper.Enums;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifySettings
    {
        public class ForwardedPort
        {
            [JsonProperty("protocol")]
            public string Protocol { get; set; }

            [JsonProperty("port")]
            public int Port { get; set; }
        }

        public class PerConnectionEncryptionSetting
        {
            [JsonProperty("adapterID")]
            public Guid AdapterId { get; set; }

            [JsonProperty("encrypted")]
            public bool Encrypted { get; set; }
        }

        [JsonProperty("jumboPackets")]
        public bool JumboPackets { get; set; }

        [JsonProperty("encrypted")]
        public bool Encrypted { get; set; }

        [JsonProperty("allowChaChaEncryption")]
        public bool AllowChaChaEncryption { get; set; }

        [JsonProperty("bondingMode")]
        [JsonConverter(typeof(EnumConverter))]
        public BondingMode? BondingMode { get; set; }

        [JsonProperty("startupConnect")]
        public bool StartupConnect { get; set; }

        [JsonProperty("transportMode")]
        [JsonConverter(typeof(EnumConverter))]
        public TransportMode? TransportMode { get; set; }

        [JsonProperty("perConnectionEncryptionEnabled")]
        public bool PerConnectionEncryptionEnabled { get; set; }

        [JsonProperty("perConnectionEncryptionSettings")]
        public List<PerConnectionEncryptionSetting> PerConnectionEncryptionSettings { get; set; }

        [JsonProperty("overflowThreshold")]
        public int OverflowThreshold { get; set; }

        [JsonProperty("forwardedPorts")]
        public List<ForwardedPort> ForwardedPorts { get; set; }

    }
}