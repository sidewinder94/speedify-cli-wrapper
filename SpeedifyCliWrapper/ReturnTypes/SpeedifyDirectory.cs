using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyDirectory
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }
    }
}
