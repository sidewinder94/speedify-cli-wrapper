using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyConnectionStats
    {
        public class Connection
        {
            [JsonProperty("inFlight")]
            public long InFlight { get; set; }

            [JsonProperty("inFlightWindow")]
            public long InFlightWindow { get; set; }

            [JsonProperty("connected")]
            public bool Connected { get; set; }

            [JsonProperty("sleeping")]
            public bool Sleeping { get; set; }

            [JsonProperty("adapterID")]
            public Guid AdapterId { get; set; }

            [JsonProperty("connectionID")]
            public string ConnectionId { get; set; }

            [JsonProperty("localIp")]
            public string LocalIp { get; set; }

            [JsonProperty("lossReceive")]
            public long LossReceive { get; set; }

            [JsonProperty("lossSend")]
            public long LossSend { get; set; }

            [JsonProperty("latencyMs")]
            public long LatencyMs { get; set; }

            [JsonProperty("privateIp")]
            public string PrivateIp { get; set; }

            [JsonProperty("protocol")]
            public string Protocol { get; set; }

            [JsonProperty("remoteIp")]
            public string RemoteIp { get; set; }

            [JsonProperty("totalBps")]
            public long TotalBps { get; set; }

            [JsonProperty("sendBps")]
            public long SendBps { get; set; }

            [JsonProperty("receiveBps")]
            public long ReceiveBps { get; set; }

            [JsonProperty("sendEstimateMbps")]
            public long SendEstimateMbps { get; set; }

            [JsonProperty("receiveEstimateMps")]
            public long ReceiveEstimateMps { get; set; }
        }

        [JsonProperty("time")]
        public long Time { get; set; }

        [NotNull]
        [JsonProperty("connections")]
        public List<Connection> Connections { get; set; } = new List<Connection>();
    }
}