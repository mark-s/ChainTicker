namespace ChanTicker.Core.Interfaces
{
    public interface ICoinMarketInfo
    {
        ICoin Coin { get; }

        string LongName { get; }
        double? Cap24HrChange { get; }
        double? CapPercent { get; }
        double Delta { get; }
        double? MarketCap { get; }
        double MarketCap24HrChangePercent { get; }
        double PercentageChange { get; }
        int Position { get; }
        double Price { get; }
        bool Shapeshift { get; }
        string ShortName { get; }
        long? Supply { get; }
        ulong Time { get; }
        double? UsdVolume { get; }
        double? Volume { get; }
    }
}