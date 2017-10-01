using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;

namespace ChainTicker.DataSource.Coins
{
    public class IconDownloader
    {
        private readonly IFileIOService _fileIOService;

        public IconDownloader(IFileIOService fileIOService)
        {
            _fileIOService = fileIOService;
        }
        public async Task DownloadImageAsync(ICoin coin)
        {
            if(string.IsNullOrEmpty(coin.ImageFileName))
                return;
            
            using (var webClient = new WebClient())
            {
                await webClient.DownloadFileTaskAsync(new Uri(coin.ImageUrlFull), GetImageNameForDisk(coin)).ConfigureAwait(false);
            }
        }



        public async Task DownloadImagesAsync(IEnumerable<ICoin> coins)
        {
            using (var webClient = new WebClient())
            {
                foreach (var coin in coins.Where(c => string.IsNullOrEmpty(c.ImageFileName) == false))
                {
                    await webClient.DownloadFileTaskAsync(new Uri(coin.ImageUrlFull), GetImageNameForDisk(coin)).ConfigureAwait(false);
                }
            }
        }



        public string GetImageNameForDisk(ICoin coin)
            => _fileIOService.GetPathAndFilename(ChainTickerFolder.Icons, coin.ImageFileName);
    }
}
