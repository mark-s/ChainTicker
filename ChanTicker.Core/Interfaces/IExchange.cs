namespace ChanTicker.Core.Interfaces
{
    public interface IExchange
    {

        IExchangeInfo ExchangeInfo { get; }

        IMarketDataSource MarketDataSource { get; }
    }
}
