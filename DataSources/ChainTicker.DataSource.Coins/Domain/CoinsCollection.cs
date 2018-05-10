using System.Collections.Generic;
using System.Linq;
using ChainTicker.Core.Interfaces;
using EnsureThat;

namespace ChainTicker.DataSource.Coins.Domain
{
    internal sealed class CoinsCollection
    {
        private readonly Dictionary<string, ICoin> _coins = new Dictionary<string, ICoin>(2000);

        internal IEnumerable<string> GetAllCoinCodes()
            => _coins.Values.Select(c => c.Code);

        internal IEnumerable<ICoin> GetAllCoins()
            => _coins.Values;
        
        internal ICoin GetCoin(string coinCode) 
            => _coins.TryGetValue(coinCode, out var coin) ? coin : new UnknownCoin(coinCode);

        internal void AddCoin(string coinCode, ICoin coin)
        {
            EnsureArg.IsNotNullOrEmpty(coinCode, nameof(coinCode));
            EnsureArg.IsNotNull(coin, nameof(coin));

            _coins[coinCode] = coin;
        }
    }
}

