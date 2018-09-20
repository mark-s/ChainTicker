using System.Collections.Generic;
using System.Linq;
using ChainTicker.Core.Interfaces;
using EnsureThat;

namespace ChainTicker.DataSource.Coins.Domain
{
    public sealed class CoinsCollection
    {
        private readonly Dictionary<string, ICoin> _coins = new Dictionary<string, ICoin>(3080); // There's approx this many coins available, let's pre-size the dictionary

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

