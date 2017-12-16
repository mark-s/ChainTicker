using System.Threading.Tasks;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.IO;
using NUnit.Framework;

namespace ChainTicker.DataSource.Coins.Tests
{
    [TestFixture]
    public class CoinLogoServiceTests
    {
        [Test]
        public async Task METHODUNDERTEST_PARTUNDERTEST_EXPECTEDRESULT()
        {

            var fileIOService = new FileIOService(new FolderService());
            var diskCache = new DiskCache(fileIOService);
            var chaintickerfileService = new ChainTickerFileService(diskCache, fileIOService, new ChainTickerJsonSerializer());
            var allCoins = new CoinInfoService(new RestService(new RandomUserAgentService()), chaintickerfileService, new ChainTickerJsonSerializer());
            
            var cls = new CoinLogoService(new ImageDownloader(new FileIOService(new FolderService())), new FileIOService(new FolderService()));

            await allCoins.PopulateAvailableCoinsAsync();

            var coins = allCoins.GetAllCoins();

           await cls.GetAllAvailableImagesAsync(coins);
        }

    }
}
