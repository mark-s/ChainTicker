using System.Diagnostics;
using ChainTicker.DataSource.Coins.DTO;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using EnsureThat;

namespace ChainTicker.DataSource.Coins.Domain
{

    [DebuggerDisplay("{" + nameof(Description) + "}")]
    internal sealed class Coin : CoinBase, ICoin
    {
        public Coin(CoinInfo coinInfo, string baseImageUrl, string baseLinkUrl)
        {
            EnsureArg.IsNotNull(coinInfo, nameof(coinInfo));
            EnsureArg.IsNotNullOrEmpty(baseImageUrl, nameof(baseImageUrl));
            EnsureArg.IsNotNullOrEmpty(baseLinkUrl, nameof(baseLinkUrl));

            IsValid = true;

            Code = coinInfo.Name;
            Name = coinInfo.CoinName;
            Description = coinInfo.FullName;

            Urls = new CoinUrls(coinInfo.ImageUrl, 
                                                   coinInfo.Url, 
                                                   baseImageUrl + coinInfo.ImageUrl, 
                                                   baseLinkUrl + coinInfo.Url, 
                                                   coinInfo.ImageUrl?.Replace("/", ""));
        }

        
    }
}
