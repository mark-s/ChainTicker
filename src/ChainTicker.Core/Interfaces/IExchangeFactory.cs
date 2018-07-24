using System.Threading.Tasks;

namespace ChainTicker.Core.Interfaces
{
    public interface IExchangeFactory
    {
        Task<IExchange> GetExchangeAsync();
    }
}