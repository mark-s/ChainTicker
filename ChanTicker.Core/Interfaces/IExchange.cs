namespace ChanTicker.Core.Interfaces
{
    public interface IExchange
    {

        ISourceInfo SourceInfo { get; }

        IExchangeDataSource ExchangeDataSource { get; }

        ICoinPriceDataSource CoinPriceDataSource { get; }
    }
}
