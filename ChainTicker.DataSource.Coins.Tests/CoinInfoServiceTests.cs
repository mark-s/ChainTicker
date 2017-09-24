using System.Linq;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins.Rest;
using ChanTicker.Core.IO;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace ChainTicker.DataSource.Coins.Tests
{

    [TestFixture]
    public class CoinInfoServiceTests
    {
        private IRestService _restService;
        private ICoinInfoCacheService _cacheService;

        [SetUp]
        public virtual void SetUp()
        {
            _restService = A.Fake<IRestService>();
            _cacheService = A.Fake<ICoinInfoCacheService>();
        }


        [Test]
        public async Task GetAllAvailableCoins_StaleCache_WebserviceCall_ReturnsAllCoins()
        {

            A.CallTo(() => _cacheService.IsStale(A<CoinInfoServiceConfig>.Ignored)).Returns(true);

            var fileIo = new FileIOService(new JsonSerializer());
            var coinInfoService = new CoinInfoService(new RestService(), new CoinInfoServiceConfig(), new CoinInfoCacheService(fileIo));

            var result = await coinInfoService.GetAllCoinsAsync();

            var btcInfo = result.GetCoinInfo("BTC");

            var allCoinCodes =  result.GetAllCoinCodes().OrderBy(c => c).ToList();


            result.GetAllCoinCodes().Count().ShouldBe(100);

        }


    }
}