using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins
{
    public class CoinLogoService : ICoinLogoService
    {
        private readonly ImageDownloader _imageDownloader;
        private readonly IDiskIOService _diskIOService;

        public CoinLogoService(ImageDownloader imageDownloader, IDiskIOService diskIOService)
        {
            _imageDownloader = imageDownloader;
            _diskIOService = diskIOService;
        }

        public async Task GetAllAvailableImagesAsync(IEnumerable<ICoin> coins)
        {
            var coinsWithNoImages = coins.Where(coin => _diskIOService.FileExists(GetCoinImageFilePath(coin)) == false);

            await _imageDownloader.DownloadImagesAsync(coinsWithNoImages);
        }

        public string GetCoinImageFilePath(ICoin coin)
            => _imageDownloader.GetImageNameForDisk(coin.Urls.ImageFileName);
    }
}