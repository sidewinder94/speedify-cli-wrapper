using JetBrains.Annotations;
using Newtonsoft.Json;
using SpeedifyCliWrapper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyServers : SpeedifyReturnedValue
    {
        [NotNull]
        [JsonProperty("public")]
        public List<SpeedifyServer> PublicServers { get; set; } = new List<SpeedifyServer>();

        [NotNull]
        [JsonProperty("private")]
        public List<SpeedifyServer> PrivateServers { get; set; } = new List<SpeedifyServer>();

        [NotNull]
        public IEnumerable<SpeedifyServer> Servers { get => this.PublicServers.Union(this.PrivateServers); }
    }
}
