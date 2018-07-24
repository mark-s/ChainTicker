using System;

namespace ChainTicker.Transport.Rest
{
    public interface ISubscribableRestService<out T>
    {
        IObservable<T> RecievedMessagesObservable { get; }

        void Subscribe();

        void Unsubscribe();
    }
}