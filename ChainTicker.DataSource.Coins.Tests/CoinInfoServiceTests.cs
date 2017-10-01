using System.Threading.Tasks;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.IO;
using FakeItEasy;
using NUnit.Framework;

namespace ChainTicker.DataSource.Coins.Tests
{

    [TestFixture]
    public class CoinInfoServiceTests
    {
        private IDiskCache _cache;
        private RestService _restService;


        [SetUp]
        public virtual void SetUp()
        {

            _cache = A.Fake<IDiskCache>();
            _restService = new RestService();
        }


        [Test]
        public async Task GetAllAvailableCoins_StaleCache_WebserviceCall_ReturnsAllCoins()
        {

            //A.CallTo(() => _cache.IsStale(A<CoinInfoServiceConfig>.Ignored)).Returns(true);

            //var fileIo = new FileIOService(new ChainTickerJsonSerializer());
            //var coinInfoService = new CoinInfoService(new RestService(new ChainTickerJsonSerializer()), new CoinInfoServiceConfig(), new CachedFileIO(fileIo));

            //var result = await coinInfoService.GetAllCoinsAsync();

            //var btcInfo = result.GetCoinInfo("BTC");

            //var allCoinCodes =  result.GetAllCoinCodes().OrderBy(c => c).ToList();


            //result.GetAllCoinCodes().Count().ShouldBe(100);

        }


    }
}