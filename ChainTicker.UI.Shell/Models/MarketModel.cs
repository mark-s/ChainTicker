using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Linq;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.UI.Shell.Models
{
    public class MarketModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Market _market;
        private readonly Func<Market, IObservable<ITick>> _subscriptionFunc;
        private readonly Action<Market> _unSubscriptionFunc;
        private IDisposable _subscription;

        private bool _isSubscribed;

        public ICoin BaseCoin { get; }
        public ICoin CounterCoin { get; }

        public TickModel CurrentTick = new TickModel();

        public string DisplayName { get; }

        internal MarketModel(Market market,
                                        ICoin baseCoinInfo,
                                        ICoin counterCoinInfo,
                                        Func<Market, IObservable<ITick>> subscriptionFunc,
                                        Action<Market> unSubscriptionFunc)
        {
            BaseCoin = baseCoinInfo;
            CounterCoin = counterCoinInfo;
            _market = market;
            _subscriptionFunc = subscriptionFunc;
            _unSubscriptionFunc = unSubscriptionFunc;
            DisplayName = market.DisplayName;
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
