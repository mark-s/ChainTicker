using System.Threading.Tasks;

namespace ChanTicker.Core.Interfaces
{
    public interface IExchangeDataSource
    {
        bool IsConnected { get; }

        Task<bool> ConnectAsync();

        void Disconnect();
    }
}
