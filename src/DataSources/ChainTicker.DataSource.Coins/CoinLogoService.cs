using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins
{
    public class CoinLogoService : ICoinLogoService
    {
        private readonly ImageDownloader _imageDownloader;
        private readonly IFileIOService _fileIOService;

        public CoinLogoService(ImageDownloader imageDownloader, IFileIOService fileIOService)
        {
            _imageDownloader = imageDownloader;
            _fileIOService = fileIOService;
        }

        public async Task GetAllAvailableImagesAsync(IEnumerable<ICoin> coins)
        {
            var coinsWithNoImages = coins.Where(coin => _fileIOService.FileExists(GetCoinImageFilePath(coin)) == false);

            await _imageDownloader.DownloadImagesAsync(coinsWithNoImages);
        }

        public string GetCoinImageFilePath(ICoin coin)
            => _imageDownloader.GetImageNameForDisk(coin.Urls.ImageFileName);
    }
}