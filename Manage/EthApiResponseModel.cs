using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jmsTools.Manage
{


    public class EthItem
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("balance")]
        public string Balance { get; set; }

        [JsonProperty("normalTxCount")]
        public int NormalTxCount { get; set; }
    }


    public class EthApiResponseModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("enMsg")]
        public string EnMsg { get; set; }

        [JsonProperty("data")]
        public List<EthItem> Data { get; set; }
    }
}
