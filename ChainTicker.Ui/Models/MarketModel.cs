using System;
using System.Diagnostics;
using System.Reactive.Linq;
using ChainTicker.Core.Domain;
using ChainTicker.Core.EventTypes;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;
using EnsureThat;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace ChainTicker.Ui.Models
{
    public class MarketModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMarket _market;
        private IDisposable _subscription;

        public string DisplayName => _market.DisplayName;

        public ICoin BaseCoin { get; }

        public ICoin CounterCoin { get; }

        public TickModel Tick { get; } = new TickModel(decimal.Zero);

        public bool HasLivePricesAvailable => _market.HasRealTimeUpdates;

        public DelegateCommand ToggleSubscribeCommand { get; }

        private bool _subscribed;
        public bool Subscribed
        {
            get => _subscribed;
            set
            {
                SetProperty(ref _subscribed, value);
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
            set => SetProperty(ref _exchangeName, value);
        }

        

        internal MarketModel(IMarket market,
                                        ICoin baseCoinInfo,
                                        ICoin counterCoinInfo,
                                        string exchangeName,
                                        IEventAggregator eventAggregator)
        {
            _eventAggregator = EnsureArg.IsNotNull(eventAggregator, nameof(eventAggregator));
            _market = EnsureArg.IsNotNull(market, nameof(market));

            BaseCoin = EnsureArg.IsNotNull(baseCoinInfo, nameof(BaseCoin));
            CounterCoin = EnsureArg.IsNotNull(counterCoinInfo, nameof(CounterCoin));
            ExchangeName = EnsureArg.IsNotNull(exchangeName, nameof(ExchangeName));
           
            ToggleSubscribeCommand = new DelegateCommand(() => Subscribed = !Subscribed, () => true);
        }


        private void Subscribe()
        {
            Debug.WriteLine($"Subscribing to {ExchangeName}: {_market.DisplayName}");

            _subscription = _market.SubscribeToTicks()
                                                    .ObserveOnDispatcher()
                                                    .Subscribe(t => Tick.Update(t),
                                                                     ex => Debug.WriteLine(ex.Message),
                                                                     () => Debug.WriteLine("OnCompleted"));

            _eventAggregator.GetEvent<MarketSubscribed>().Publish(new MarketInfo(ExchangeName, _market.DisplayName));
        }

        private void Unsubscribe()
        {
            _subscription.Dispose();
            _market.UnsubscribeFromTicks();
            _eventAggregator.GetEvent<MarketUnsubscribed>().Publish(new MarketInfo(ExchangeName, _market.DisplayName));

            Debug.WriteLine($"Unsubscribed from  {ExchangeName}: {_market.DisplayName}");

        }

    }
}
