using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChainTicker.Module.Tickers.Services
{

    public class MarketSubscriptionService : IMarketSubscriptionService
    {
        private readonly IChainTickerFileService _fileService;
        private const string FILENAME = "subs.json";

        // NOTE: this could be a bit fruity if manipulated by multiple threads...
        private HashSet<MarketInfo> _subscribedMarkets = new HashSet<MarketInfo>();
        private bool _loaded;

        public MarketSubscriptionService(IChainTickerFileService fileService)
        {
            _fileService = fileService;

            Messenger.Default.Register<MarketUnsubscribed>(this, m => _subscribedMarkets.Remove(m.MarketInfo));
            Messenger.Default.Register<MarketSubscribed>(this, m => _subscribedMarkets.Add(m.MarketInfo));
        }


        public async Task<bool> WasSubscribedToAsync(string exchangeName, string marketDescription)
        {
            await LoadIfNeededAsync();

            return _subscribedMarkets.Contains(new MarketInfo(exchangeName, marketDescription));
        }

        public async Task SaveSubscribedMarketsAsync()
            => await _fileService.SaveAndSerializeAsync(AppFolder.ApplicationBase, FILENAME, _subscribedMarkets);


        private async Task LoadIfNeededAsync()
        {
            if (_loaded == false)
            {
                var fromDisk = await _fileService.LoadAndDeserializeAsync<HashSet<MarketInfo>>(AppFolder.ApplicationBase, FILENAME);
                if (fromDisk != null)
                    _subscribedMarkets = fromDisk;
            }
            _loaded = true;
        }
    }



}
