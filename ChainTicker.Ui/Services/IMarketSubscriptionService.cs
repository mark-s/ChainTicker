using System.Threading.Tasks;

namespace ChainTicker.Ui.Services
{
    public interface IMarketSubscriptionService
    {
        Task SaveSubscribedMarketsAsync();

        Task<bool> WasSubscribedToAsync(string exchangeName, string marketDescription);

    }
}