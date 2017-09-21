using System;
using System.Threading.Tasks;

namespace ChanTicker.Core.Interfaces
{
    public interface ICoinPriceDataSource
    {
        Task<ICoinPair[]> GetAvailableCoinPairsAsync();

        Task<ICoinPairPrice> GetCurrentPriceAsync(ICoinPair coinPair);

        IObservable<ICoinPairPrice> SubscribeToPriceTicker(ICoinPair coinPair);
    }
}