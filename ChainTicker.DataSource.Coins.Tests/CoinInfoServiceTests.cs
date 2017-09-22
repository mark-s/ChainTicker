using System.Linq;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins.Rest;
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

            var coinInfoService = new CoinInfoService(new RestService());

            var result = await coinInfoService.GetAllCoinsAsync();

            result.GetCoinInfo("BTC");


            result.GetAllCoinCodes().Count().ShouldBe(100);

        }


    }
}