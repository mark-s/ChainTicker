using System;

namespace ChainTicker.App.Models
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}
