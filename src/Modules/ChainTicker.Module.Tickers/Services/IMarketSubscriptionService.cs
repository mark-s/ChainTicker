using System.Threading.Tasks;

namespace ChainTicker.Module.Tickers.Services
{
    public interface IMarketSubscriptionService
    {
        Task SaveSubscribedMarketsAsync();

        Task<bool> WasSubscribedToAsync(string exchangeName, string marketDescription);

    }
}