﻿using Newtonsoft.Json;
using SpeedifyCliWrapper.Converters;
using SpeedifyCliWrapper.Enums;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyState
    {
        [JsonProperty("state")]
        [JsonConverter(typeof(EnumConverter))]
        public ConnectionState State { get; set; }
    }
}