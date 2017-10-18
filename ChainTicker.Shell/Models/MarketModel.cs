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
        public bool IsSubscribed
        {
            get => _isSubscribed;
            set
            {
                SetProperty(ref _isSubscribed, value);
                SubscribeCommand.RaiseCanExecuteChanged();
                UnsubscribeCommand.RaiseCanExecuteChanged();
            }
        }

        public string DisplayName => _market.DisplayName;

        public ICoin BaseCoin { get; }
        public ICoin CounterCoin { get; }
        public TickModel CurrentTick { get; } 

        public DelegateCommand SubscribeCommand { get; }
        public DelegateCommand UnsubscribeCommand { get; }



        internal MarketModel(Market market,
                                        ICoin baseCoinInfo,
                                        ICoin counterCoinInfo,
                                        Func<Market, IObservable<ITick>> subscriptionFunc,
                                        Action<Market> unSubscriptionFunc)
        {

            _market = market ?? throw new ArgumentNullException(nameof(market), "market is required!");

            CurrentTick = new TickModel(_market.MidMarketPriceSnapshot);

            BaseCoin = baseCoinInfo;
            CounterCoin = counterCoinInfo;

            _subscriptionFunc = subscriptionFunc;
            _unSubscriptionFunc = unSubscriptionFunc;

            SubscribeCommand = new DelegateCommand(Subscribe, () => IsSubscribed == false &&_market.HasLivePricesAvailable);

            UnsubscribeCommand = new DelegateCommand(UnSubscribe, () => IsSubscribed);
        }

        public void Subscribe()
        {
            if (IsSubscribed) return;

            _subscription = _subscriptionFunc(_market).ObserveOnDispatcher()
                                                                      .Subscribe(t => CurrentTick.Update(t),
                                                                                     ex => Debug.WriteLine(ex.Message),
                                                                                     () => Debug.WriteLine("OnCompleted"));

            IsSubscribed = true;
        }

        public void UnSubscribe()
        {
            if (IsSubscribed == false) return;

            _subscription.Dispose();
            _unSubscriptionFunc(_market);

            IsSubscribed = false;
        }

    }
}
