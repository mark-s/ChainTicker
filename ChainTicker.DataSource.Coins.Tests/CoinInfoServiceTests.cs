using System.Linq;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins.Rest;
using ChanTicker.Core.IO;
using NUnit.Framework;
using Shouldly;

namespace ChainTicker.DataSource.Coins.Tests
{

    [TestFixture]
    public class CoinInfoServiceTests
    {

        [Test]
        public async Task GetAllAvailableCoins_ReturnsAllCoinsAsync()
        {

            var coinInfoService = new CoinInfoService(new RestService(), new FileIOService());

            var result = await coinInfoService.GetAllCoinsAsync();

            var btcInfo = result.GetCoinInfo("BTC");

            var allCoinCodes =  result.GetAllCoinCodes().OrderBy(c => c).ToList();


            result.GetAllCoinCodes().Count().ShouldBe(100);

        }


    }
}