using System.Collections.Generic;
using System.Threading.Tasks;
using ChainTicker.Core.EventTypes;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;
using Prism.Events;

namespace ChainTicker.Shell.Services
{

    public class MarketSubscriptionService : IMarketSubscriptionService
    {
        private readonly IChainTickerFileService _fileService;
        private const string FILENAME = "subs.json";

        // NOTE: this could be a bit fruity if manipulated by multiple threads...
        private HashSet<SubscribedMarket> _subscribedMarkets = new HashSet<SubscribedMarket>();
        private bool _loaded;

        public MarketSubscriptionService(IChainTickerFileService fileService, IEventAggregator eventAggregator)
        {
            _fileService = fileService;

            eventAggregator.GetEvent<MarketUnsubscribed>().Subscribe( m => _subscribedMarkets.Remove(m));
            eventAggregator.GetEvent<MarketSubscribed>().Subscribe(m => _subscribedMarkets.Add(m));
        }


        public async Task<bool> WasSubscribedToAsync(string exchangeName, string marketDescription)
        {
            await LoadIfNeededAsync();

            return _subscribedMarkets.Contains(new SubscribedMarket(exchangeName, marketDescription));
        }

        public async Task SaveSubscribedMarketsAsync() 
            => await _fileService.SaveAndSerializeAsync(ChainTickerFolder.ApplicationBase, FILENAME, _subscribedMarkets);


        private async Task LoadIfNeededAsync()
        {
            if (_loaded == false)
            {
                var fromDisk = await _fileService.LoadAndDeserializeAsync<HashSet<SubscribedMarket>>(ChainTickerFolder.ApplicationBase, FILENAME);
                if (fromDisk != null)
                    _subscribedMarkets = fromDisk;
            }
            _loaded = true;
        }
    }



}
