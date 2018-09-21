using System;
using System.Diagnostics;

namespace ChainTicker.Module.Tickers.Models
{
    public class MarketModel : ViewModelBase
    {
        private readonly IMarket _market;
        private IDisposable _subscription;

        public string DisplayName => _market.DisplayName;

        public ICoin BaseCoin { get; }

        public ICoin CounterCoin { get; }

        public TickModel Tick { get; } = new TickModel(decimal.Zero);

        public bool HasLivePricesAvailable => _market.HasRealTimeUpdates;

        public RelayCommand ToggleSubscribeCommand { get; }

        private bool _subscribed;
        public bool Subscribed
        {
            get => _subscribed;
            set
            {
                Set(ref _subscribed, value);
                if (value == true)
                    Subscribe();
                else
                    Unsubscribe();
            }
        }


        private string _exchangeName;
        public string ExchangeName
        {
            get => _exchangeName;
            set => Set(ref _exchangeName, value);
        }



        internal MarketModel(IMarket market,
                                        ICoin baseCoinInfo,
                                        ICoin counterCoinInfo,
                                        string exchangeName)
        {

            _market = EnsureArg.IsNotNull(market, nameof(market));

            BaseCoin = EnsureArg.IsNotNull(baseCoinInfo, nameof(BaseCoin));
            CounterCoin = EnsureArg.IsNotNull(counterCoinInfo, nameof(CounterCoin));
            ExchangeName = EnsureArg.IsNotNull(exchangeName, nameof(ExchangeName));

            ToggleSubscribeCommand = new RelayCommand(() => Subscribed = !Subscribed, () => true);
        }


        private void Subscribe()
        {
            Debug.WriteLine($"Subscribing to {ExchangeName}: {_market.DisplayName}");

            _subscription = _market.SubscribeToTicks()
                                                    .ObserveOnDispatcher()
                                                    .Subscribe(t => Tick.Update(t),
                                                                     ex => Debug.WriteLine(ex.Message),
                                                                     () => Debug.WriteLine("OnCompleted"));

            Messenger.Default.Send(new MarketSubscribed(new MarketInfo(ExchangeName, _market.DisplayName)));

        }

        private void Unsubscribe()
        {
            _subscription.Dispose();
            _market.UnsubscribeFromTicks();
            
            Messenger.Default.Send(new MarketUnsubscribed(new MarketInfo(ExchangeName, _market.DisplayName)));

            Debug.WriteLine($"Unsubscribed from  {ExchangeName}: {_market.DisplayName}");

        }

    }
}
