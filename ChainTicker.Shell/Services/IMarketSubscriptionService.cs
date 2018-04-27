using System.Threading.Tasks;

namespace ChainTicker.Shell.Services
{
    public interface IMarketSubscriptionService
    {
        Task SaveSubscribedMarketsAsync();

        Task<bool> WasSubscribedToAsync(string exchangeName, string marketDescription);

    }
}