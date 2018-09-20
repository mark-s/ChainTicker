using System;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins.DTO;

namespace ChainTicker.DataSource.Coins.Services
{
    public interface IWebSource
    {
        Task GetFromWebServiceAsync(Action<AllCoinsResponse> onSuccess, Action<string> onFailure);
    }
}