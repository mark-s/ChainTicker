using System.Collections.Generic;
using RestSharp.Deserializers;

namespace ChainTicker.DataSource.Coins.DTO
{
    public class AllCoinsResponse
    {
        public string Response { get; set; }
        public string Message { get; set; }
        public string BaseImageUrl { get; set; }
        public string BaseLinkUrl { get; set; }

        public int Type { get; set; }

        [DeserializeAs(Name = "Data")]
        public Dictionary<string, CoinInfo> Coins { get; set; }
    }
}