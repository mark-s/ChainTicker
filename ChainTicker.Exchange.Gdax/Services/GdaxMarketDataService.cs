using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ChainTicker.Exchange.Gdax.DTO.Responses;
using ChainTicker.Transport.WebSocket;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;

namespace ChainTicker.Exchange.Gdax.Services
{
    public class GdaxMarketDataService : IMarketDataService
    {
        private readonly IWebSocketTransport _webSocketTransport;
        private readonly INotRealTimePriceService _priceQueryService;
        private readonly MessageFactory _messageFactory;
        private readonly ChainTickerJsonSerializer _serialiser;

        private readonly HashSet<string> _subscribedProducts = new HashSet<string>();

        public GdaxMarketDataService(IWebSocketTransport webSocketTransport, INotRealTimePriceService priceQueryService)
        {
            _webSocketTransport = webSocketTransport;
            _priceQueryService = priceQueryService;

            _serialiser = new ChainTickerJsonSerializer();

            _messageFactory = new MessageFactory(_serialiser);
        }

        public IObservable<ITick> SubscribeToTicks(Market market)
        {
            _webSocketTransport.Send(_messageFactory.CreateSubscribeMessage(market.ProductCode));

            _subscribedProducts.Add(market.ProductCode);

            return _webSocketTransport.RecievedMessagesObservable
                                      .Where(m => JsonHelpers.GetType<GdaxMessageType>(m) == GdaxMessageType.Ticker)
                                      .Select(m => _serialiser.Deserialize<GdaxTick>(m))
                                      .Where(gt => gt.ProductId == market.ProductCode)
                                      .Select(ConvertToTick);
        }


        public Task<ITick> GetCurrentPriceAsync(Market market) 
            => _priceQueryService.GetCurrentPriceAsync(market);

        public void UnsubscribeFromTicks(Market market)
        {
            if (_subscribedProducts.Contains(market.ProductCode))
            {
                _webSocketTransport.Send(_messageFactory.CreateUnsubscribeMessage(market.ProductCode));
                _subscribedProducts.Remove(market.ProductCode);
            }
            else
            {
                Debug.WriteLine("Trying to subscribe from a market that isn't subscribed to. Ignoring.");
            }
        }

        public bool IsSubscribedToTicks(Market market) 
            => _subscribedProducts.Contains(market.ProductCode);

        private ITick ConvertToTick(GdaxTick gdaxTick)
        {
            return new Tick(gdaxTick.Price,
                                DateTimeOffset.Now,
                                gdaxTick.BestAsk,
                                gdaxTick.BestBid,
                                gdaxTick.Volume24h);
        }

    }
}