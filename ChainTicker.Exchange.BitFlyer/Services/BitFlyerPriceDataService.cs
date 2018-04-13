using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ChainTicker.Transport.Pubnub;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Exchange.BitFlyer.Services
{
    public class BitFlyerPriceDataService : IPriceDataService, IDisposable
    {
        private readonly IPubnubTransport _pubnubTransport;
        private readonly IPollingPriceService _pollingPriceService;
        private readonly MessageParser _messageParser;


        public BitFlyerPriceDataService(IPollingPriceService pollingPriceService, ISerialize jsonSerializer)
        {
            _pollingPriceService = pollingPriceService;
            _messageParser = new MessageParser(jsonSerializer);

            _pubnubTransport = new PubnubTransport(Config.Info.ApiEndpoints[ApiEndpointType.Pubnub], new DebugLogger());
        }


        public IObservable<ITick> SubscribeToTicks(Market market)
        {
            if (market.HasRealTimeUpdates)
                return SubscribeToLiveMarket(market);
            else
                return SubscribeToTimedUpdates(market);

        }

        public void UnsubscribeFromTicks(Market market)
        {
            if (market.HasRealTimeUpdates)
                _pubnubTransport.UnsubscribeFromChannel(GetChannelName(market));
            else
                _pollingPriceService.Unubscribe(market);
        }

        public bool IsSubscribedToTicks(Market market)
        {
            var channelName = GetChannelName(market);
            return _pubnubTransport.IsSubscribedToChannel(channelName);
        }

        public async Task<ITick> GetCurrentPriceAsync(Market market)
            => await _pollingPriceService.GetCurrentPriceAsync(market);

        private IObservable<ITick> SubscribeToLiveMarket(Market market)
        {
            var channelName = GetChannelName(market);

            _pubnubTransport.SubscribeToChannel(channelName);

            return _pubnubTransport.RecievedMessages.ObserveOn(Scheduler.Default)
                .Where(m => m.ChannelName == channelName)
                .Select(m => _messageParser.ConvertToTick(m));
        }

        private IObservable<ITick> SubscribeToTimedUpdates(Market market) 
            => _pollingPriceService.Subscribe(market);
        
        private string GetChannelName(Market market)
            => "lightning_ticker_" + market.ProductCode;


        public void Dispose() 
            => _pubnubTransport?.Dispose();
    }
}