using System.Threading.Tasks;
using ChainTicker.Transport.Rest;
using ChainTicker.Core.IO;
using NUnit.Framework;

namespace ChainTicker.DataSource.Coins.Tests
{
    [TestFixture]
    public class CoinLogoServiceTests
    {
        [Test]
        public async Task GetAllAvailableImagesAsync_Works()
        {

            var fileIOService = new DiskIOService(new FolderService());
            var diskCache = new DiskCache(fileIOService);
            var chaintickerfileService = new ChainTickerFileService(diskCache, fileIOService, new ChainTickerJsonSerializer());
            var allCoins = new CoinInfoService(new RestService(new RandomUserAgentService(), new ChainTickerJsonSerializer()), chaintickerfileService);
            
            var cls = new CoinLogoService(new ImageDownloader(new DiskIOService(new FolderService())), new DiskIOService(new FolderService()));

            await allCoins.GetAvailableCoinsAsync();

            var coins = allCoins.GetAllCoins();

           await cls.GetAllAvailableImagesAsync(coins);
        }

    }
}
