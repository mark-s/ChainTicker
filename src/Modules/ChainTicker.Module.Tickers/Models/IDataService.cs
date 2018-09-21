using System;

namespace ChainTicker.Module.Tickers.Models
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}
