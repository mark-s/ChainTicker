using System.Threading.Tasks;

namespace ChainTicker.App.Services
{
    public interface IMarketSubscriptionService
    {
        Task SaveSubscribedMarketsAsync();

        Task<bool> WasSubscribedToAsync(string exchangeName, string marketDescription);

    }
}