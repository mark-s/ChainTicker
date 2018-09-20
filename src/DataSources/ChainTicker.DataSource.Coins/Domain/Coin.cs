using System.Diagnostics;
using ChainTicker.DataSource.Coins.DTO;
using ChainTicker.Core.Interfaces;
using EnsureThat;

namespace ChainTicker.DataSource.Coins.Domain
{

    [DebuggerDisplay("{" + nameof(Description) + "}")]
    public sealed class Coin : CoinBase, ICoin
    {
        internal Coin(CoinInfo coinInfo, string baseImageUrl, string baseLinkUrl)
        {
            EnsureArg.IsNotNull(coinInfo, nameof(coinInfo));
            EnsureArg.IsNotNullOrEmpty(baseImageUrl, nameof(baseImageUrl));
            EnsureArg.IsNotNullOrEmpty(baseLinkUrl, nameof(baseLinkUrl));

            IsValid = true;

            Code = coinInfo.Name;
            Name = coinInfo.CoinName;
            Description = coinInfo.FullName;
        }

        
    }
}
