using System.Collections.Generic;

namespace ChainTicker.DataSource.Coins.DTO
{
    internal class AllCoinsResponse
    {
        public string Response { get; set; }
        public string Message { get; set; }
        public string BaseImageUrl { get; set; }
        public string BaseLinkUrl { get; set; }

        public int Type { get; set; }

        public Dictionary<string, CoinInfo> Data { get; set; }
    }
}