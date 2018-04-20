using System;
using System.Diagnostics;
using System.Reactive.Linq;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using EnsureThat;
using Prism.Commands;
using Prism.Mvvm;

namespace ChainTicker.Shell.Models
{
    public class MarketModel : BindableBase
    {
        private readonly IMarket _market;


        private IDisposable _subscription;

        private bool _isSubscribed;
        public bool IsSubscribed
        {
            get => _isSubscribed;
            set
            {
                SetProperty(ref _isSubscribed, value);
                OnSubscribeChange(value);
            }
        }

        private void OnSubscribeChange(bool subscribeRequested)
        {
            if (subscribeRequested)
                Subscribe();
            else
                Unsubscribe();
        }

        public string DisplayName => _market.DisplayName;

        public ICoin BaseCoin { get; }
        public ICoin CounterCoin { get; }


        private string _exchangeName;
        public string ExchangeName
        {
            get => _exchangeName;
            set => SetProperty(ref _exchangeName, value);
        }


        public TickModel Tick { get; }


        public bool HasLivePricesAvailable => _market.HasRealTimeUpdates;

        public DelegateCommand ToggleSubscribeCommand { get; }

        internal MarketModel(IMarket market,
                                        ICoin baseCoinInfo,
                                        ICoin counterCoinInfo,
                                        string exchangeName)
        {
            _market = EnsureArg.IsNotNull(market, nameof(market));

            Tick = new TickModel(decimal.Zero);

            BaseCoin = baseCoinInfo;
            CounterCoin = counterCoinInfo;
            ExchangeName = exchangeName;


            ToggleSubscribeCommand = new DelegateCommand(() => IsSubscribed = !IsSubscribed, () => true);

        }

        private void Subscribe()
        {
            Debug.WriteLine($"Subscribing to {ExchangeName}: {_market.DisplayName}");

            _subscription = _market.SubscribeToTicks()
                                                    .ObserveOnDispatcher()
                                                    .Subscribe(t => Tick.Update(t),
                                                                     ex => Debug.WriteLine(ex.Message),
                                                                     () => Debug.WriteLine("OnCompleted"));
        }

        private void Unsubscribe()
        {
            _subscription.Dispose();
            _market.UnsubscribeFromTicks();

            Debug.WriteLine($"Unsubscribed from  {ExchangeName}: {_market.DisplayName}");

        }
    }
}
