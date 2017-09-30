using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChanTicker.Core.Interfaces
{
    public interface ICoinPriceDataSource
    {
        Task<List<IMarketPair>> GetAvailableMarketsAsync();

        Task<ICoinPairPrice> GetCurrentPriceAsync(ICoinPair coinPair);

        Task<IObservable<ICoinPairPrice>> SubscribeToPriceTicker(ICoinPair coinPair);
    }
}