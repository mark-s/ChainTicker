using System;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.Transport.Rest;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace ChainTicker.DataSource.Coins.Tests
{

    [TestFixture]
    public sealed class CoinInfoServiceTests
    {
        private IDiskCache _cache;
        private RestService _restService;
        private IChainTickerFileService _fileService;
        private IFileIOService _fileIoService;


        [SetUp]
        public void SetUp()
        {

            _cache = A.Fake<IDiskCache>();
            _restService = new RestService(new RandomUserAgentService(), new ChainTickerJsonSerializer());
            _fileIoService = A.Fake<IFileIOService>();
            
            _fileService = new ChainTickerFileService(_cache, _fileIoService, new ChainTickerJsonSerializer());
        }


        [Test]
        public async Task GetAllAvailableCoins_StaleCache_WebserviceCall_ReturnsAllCoins()
        {
            A.CallTo(() => _cache.IsStale(A<ChainTickerFolder>.Ignored, A<string>.Ignored, A<TimeSpan>.Ignored)).Returns(false);

            A.CallTo(() => _fileIoService.LoadTextAsync(A<ChainTickerFolder>.Ignored, A<string>.Ignored)
                            ).Returns(GetCoinsJson());


            var coinInfoService = new CoinInfoService(_restService, _fileService);

            await coinInfoService.GetAvailableCoinsAsync();

           
            var result = coinInfoService.GetAllCoins();

            //var btcInfo = result.GetCoinInfo("BTC");

            //var allCoinCodes = result.GetAllCoinCodes().OrderBy(c => c).ToList();

            result.Count().ShouldBe(100);

        }



        private string GetCoinsJson()
        {
            return "{\"Response\":\"Success\",\"Message\":\"Coin list succesfully returned!\",\"BaseImageUrl\":\"https://www.someurl.com\",\"BaseLinkUrl\":\"https://www.someurl.com\",\"Data\":{\"42\":{\"ProductCode\":\"4321\",\"Url\":\"/coins/42/overview\",\"ImageUrl\":\"/media/19984/42.png\",\"Name\":\"42\",\"CoinName\":\"42 Coin\",\"FullName\":\"42 Coin (42)\",\"Algorithm\":\"Scrypt\",\"ProofType\":\"PoW\",\"FullyPremined\":\"0\",\"TotalCoinSupply\":\"42\",\"PreMinedValue\":\"N/A\",\"TotalCoinsFreeFloat\":\"N/A\",\"SortOrder\":\"34\",\"Sponsored\":false},\"365\":{\"ProductCode\":\"33639\",\"Url\":\"/coins/365/overview\",\"ImageUrl\":\"/media/352070/365.png\",\"Name\":\"365\",\"CoinName\":\"365Coin\",\"FullName\":\"365Coin (365)\",\"Algorithm\":\"X11\",\"ProofType\":\"PoW/PoS\",\"FullyPremined\":\"0\",\"TotalCoinSupply\":\"2300000000\",\"PreMinedValue\":\"N/A\",\"TotalCoinsFreeFloat\":\"N/A\",\"SortOrder\":\"916\",\"Sponsored\":false}},\"Type\":100}";
        }



    }
}