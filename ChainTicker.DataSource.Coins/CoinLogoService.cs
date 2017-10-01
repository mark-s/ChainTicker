using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;

namespace ChainTicker.DataSource.Coins
{
    public class CoinLogoService : ICoinLogoService
    {
        private readonly IconDownloader _iconDownloader;
        private readonly IFileIOService _fileIOService;

        public CoinLogoService(IconDownloader iconDownloader, IFileIOService fileIOService)
        {
            _iconDownloader = iconDownloader;
            _fileIOService = fileIOService;
        }

        public async Task GetAllAvailableImagesAsync(IEnumerable<ICoin> coins)
        {
            var coinsWithNoImages = coins.Where(coin => _fileIOService.FileExists(GetCoinImageFilePath(coin)) == false);

            await _iconDownloader.DownloadImagesAsync(coinsWithNoImages);
        }

        public string GetCoinImageFilePath(ICoin coin)
            => _iconDownloader.GetImageNameForDisk(coin);
    }
}