using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;

namespace ChainTicker.DataSource.Coins
{
    public class ImageDownloader
    {
        private readonly IFileIOService _fileIOService;

        public ImageDownloader(IFileIOService fileIOService)
        {
            _fileIOService = fileIOService;
        }
        
        public async Task DownloadImageAsync(ICoin coin)
        {
            if(coin.Urls is CoinUrlsUnknown)
                return;
            
            using (var webClient = new WebClient())
            {
                await webClient.DownloadFileTaskAsync(new Uri(coin.Urls.ImageUrlFull), GetImageNameForDisk(coin.Urls.ImageFileName)).ConfigureAwait(false);
            }
        }
        
        public async Task DownloadImagesAsync(IEnumerable<ICoin> coins)
        {
            using (var webClient = new WebClient())
            {
                foreach (var coin in coins.Where(c => c.Urls is CoinUrls))
                {
                    await webClient.DownloadFileTaskAsync(new Uri(coin.Urls.ImageUrlFull), GetImageNameForDisk(coin.Urls.ImageFileName)).ConfigureAwait(false);
                }
            }
        }
        
        public string GetImageNameForDisk(string coinImageFileName)
            => _fileIOService.GetPathAndFilename(ChainTickerFolder.Icons, coinImageFileName);
    }
}
