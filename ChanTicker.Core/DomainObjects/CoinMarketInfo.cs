using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.DomainObjects
{
    public class CoinMarketInfo : ICoinMarketInfo
    {
        public ICoin Coin { get; set; }
        public string LongName { get; set; }
        public double? Cap24HrChange { get; set; }
        public double? CapPercent { get; set; }
        public double Delta { get; set; }
        public double? MarketCap { get; set; }
        public double MarketCap24HrChangePercent { get; set; }
        public double PercentageChange { get; set; }
        public int Position { get; set; }
        public double Price { get; set; }
        public bool Shapeshift { get; set; }
        public string ShortName { get; set; }
        public long? Supply { get; set; }
        public ulong Time { get; set; }
        public double? UsdVolume { get; set; }
        public double? Volume { get; set; }
    }
}