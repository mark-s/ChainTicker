using System;
using System.Diagnostics;
using System.Reactive.Linq;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace ChainTicker.Shell.Models
{
    public class MarketModel : BindableBase
    {

        private readonly Market _market;
        private readonly Func<Market, IObservable<ITick>> _subscriptionFunc;
        private readonly Action<Market> _unSubscriptionFunc;
        private IDisposable _subscription;

        private bool _isSubscribed;

        private ICoin _baseCoin;

        public ICoin BaseCoin
        {
            get => _baseCoin;
            private set => SetProperty(ref _baseCoin, value);
        }

        private ICoin _counterCoin;
        public ICoin CounterCoin
        {
            get => _counterCoin;
            private set => SetProperty(ref _counterCoin, value);
        }





        private TickModel _currentTick;
        public TickModel CurrentTick
        {
            get => _currentTick;
            set => SetProperty(ref _currentTick, value);
        }





        public DelegateCommand SubscribeCommand { get; }



        public string DisplayName { get; }

        internal MarketModel(Market market,
                                        ICoin baseCoinInfo,
                                        ICoin counterCoinInfo,
                                        Func<Market, IObservable<ITick>> subscriptionFunc,
                                        Action<Market> unSubscriptionFunc)
        {

            _market = market ?? throw new ArgumentNullException(nameof(market), "market is required!");

            BaseCoin = baseCoinInfo;
            CounterCoin = counterCoinInfo;
            _subscriptionFunc = subscriptionFunc;
            _unSubscriptionFunc = unSubscriptionFunc;
            DisplayName = market.DisplayName;

            SubscribeCommand = new DelegateCommand(StartSubscribe);
            _currentTick = new TickModel();
    }

        public void StartSubscribe()
        {
            if (_isSubscribed)
                return;
            _isSubscribed = true;

            _subscription = _subscriptionFunc(_market).ObserveOnDispatcher()
                                                                    .Subscribe(
                                                                       t => CurrentTick.Update(t),
                                                                       ex => Debug.WriteLine(ex.Message),
                                                                       () => Debug.WriteLine("OnCompleted"));

        }

        public void UnSubscribe()
        {
            _subscription.Dispose();
            _unSubscriptionFunc(_market);
        }



    }
}
