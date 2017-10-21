using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ChainTicker.Transport.Rest
{
    public class SubscribableRestService<T> : ISubscribableRestService<T>
    {
        private readonly string _restQuery;
        private readonly Func<string, T> _deserializer;
        private readonly IObservable<long> _observableTimer;
        private readonly IRestService _restService;

        public IObservable<T> RecievedMessagesObservable => _messageSubject.AsObservable();
        private readonly Subject<T> _messageSubject = new Subject<T>();

        private IDisposable _timerSubscription;


        public SubscribableRestService(IRestService restService, string restQuery, Func<string, T> deserializer, TimeSpan updateTimeSpan)
        {
            _restService = restService;
            _restQuery = restQuery;
            _deserializer = deserializer;

            _observableTimer = Observable.Timer(TimeSpan.FromSeconds(0), updateTimeSpan);

        }

        public void Subscribe()
        {
            
            _timerSubscription = _observableTimer.Select(_ =>  _restService.GetAsync(_restQuery, _deserializer))
                                   .Subscribe(m => _messageSubject.OnNext(m.Result.Data));
        }


        public void Unsubscribe()
        {
            _timerSubscription.Dispose();
        }




    }
}