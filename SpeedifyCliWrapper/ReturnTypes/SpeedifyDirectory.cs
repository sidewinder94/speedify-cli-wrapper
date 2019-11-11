using Newtonsoft.Json;
using SpeedifyCliWrapper.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyDirectory : SpeedifyReturnedValue
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }
    }
}
