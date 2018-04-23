using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;
using ChainTicker.Shell.Models;

namespace ChainTicker.Shell.Services
{
    public class MarketSubscriptionService : IMarketSubscriptionService
    {
        private readonly IChainTickerFileService _fileService;
        private const string FILENAME = "subs.json";

        private List<SubscribedMarket> _subscribedMarkets;

        public MarketSubscriptionService(IChainTickerFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task SaveSubscribedMarketsAsync(ExchangeCollectionModel exchangeCollection)
        {
            var subscribedMarkets = new List<SubscribedMarket>();

            foreach (var exchange in exchangeCollection.Exchanges)
            {
                subscribedMarkets.AddRange( exchange.Markets.Where(m => m.IsSubscribed)
                    .Select(m => new SubscribedMarket(m.ExchangeName, m.DisplayName)));

            }

            await _fileService.SaveAndSerializeAsync(ChainTickerFolder.ApplicationBase, FILENAME, subscribedMarkets);
        }


        public async Task<bool> WasSubscribedToAsync(string exchangeName, string marketDescription)
        {
            await LoadIfNeededAsync();

            return _subscribedMarkets.Contains(new SubscribedMarket(exchangeName, marketDescription));
        }

        private async Task LoadIfNeededAsync()
        {
            if (_subscribedMarkets == null)
                _subscribedMarkets = await _fileService.LoadAndDeserializeAsync<List<SubscribedMarket>>(ChainTickerFolder.ApplicationBase, FILENAME);

        }
    }



}
