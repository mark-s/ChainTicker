using System.Collections.Generic;
using System.Threading.Tasks;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins
{
    public interface ICoinLogoService
    {
        Task GetAllAvailableImagesAsync(IEnumerable<ICoin> coins);

        string GetCoinImageFilePath(ICoin coin);
    }
}