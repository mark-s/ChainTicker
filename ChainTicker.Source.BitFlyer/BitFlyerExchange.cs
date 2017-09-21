using System;
using System.Threading.Tasks;
using ChainTicker.Transport.Pubnub;
using ChanTicker.Core.Entities;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Source.BitFlyer
{
    public class BitFlyerExchange : IExchange
    {
        public ISourceInfo SourceInfo { get; }
        public IExchangeDataSource ExchangeDataSource { get; }
        public ICoinPriceDataSource CoinPriceDataSource { get; }

        public BitFlyerExchange(IPubnubTransport transport)
        {
            SourceInfo = new BitFlyerSourceInfo();

            ExchangeDataSource = new BitFlyerExchangeDataSource(transport);

            CoinPriceDataSource = new BitFlyerCoinPriceDataSource(ExchangeDataSource);
        }

    }

    public class BitFlyerCoinPriceDataSource : ICoinPriceDataSource
    {
        private readonly IExchangeDataSource _exchangeDataSource;

        public BitFlyerCoinPriceDataSource(IExchangeDataSource exchangeDataSource)
        {
            _exchangeDataSource = exchangeDataSource;
        }

        public Task<ICoinPair[]> GetAvailableCoinPairsAsync()
        {
        }

        public Task<ICoinPairPrice> GetCurrentPriceAsync(ICoinPair coinPair)
        {
        }

        public IObservable<ICoinPairPrice> SubscribeToPriceTicker(ICoinPair coinPair)
        {
            
        }
    }

    public class BitFlyerExchangeDataSource : IExchangeDataSource
    {
        private readonly IPubnubTransport _transport;

        public BitFlyerExchangeDataSource(IPubnubTransport transport)
        {
            _transport = transport;
        }

        public bool IsConnected { get; }
        public Task<bool> ConnectAsync()
        {
            _transport.Conenct
        }

        public void Disconnect()
        {
        }
    }
}
