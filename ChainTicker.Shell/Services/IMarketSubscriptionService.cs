using System.Threading.Tasks;
using ChainTicker.Shell.Models;

namespace ChainTicker.Shell.Services
{
    public interface IMarketSubscriptionService
    {
        Task SaveSubscribedMarketsAsync(ExchangeCollectionModel exchangeCollection);
        Task<bool> WasSubscribedToAsync(string exchangeName, string marketDescription);
    }
}