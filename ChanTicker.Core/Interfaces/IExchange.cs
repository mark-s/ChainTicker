namespace ChanTicker.Core.Interfaces
{
    public interface IExchange
    {

        ISourceInfo SourceInfo { get; }

        ICoinPriceDataSource CoinPriceData { get; }
    }
}
