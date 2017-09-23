using System;
using System.Diagnostics;
using ChainTicker.DataSource.Coins.DTO;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins.Domain
{

    [DebuggerDisplay("{" + nameof(Description) + "}")]
    public class Coin : ICoin
    {

        public string Code { get; }

        public string Name { get; }

        public string Description { get; }


        public string InfoUrl { get; }

        public string ImageUrl { get; }


        public string Algorithm { get; }

        public string ProofType { get; }

        public bool IsFullyPremined { get; }

        public string TotalCoinSupply { get; }

        public string PreMinedValue { get; }

        public string TotalCoinsFreeFloat { get; }


        public Coin(CoinInfo coinInfo, string baseImageUrl, string baseLinkUrl)
        {

            Code = coinInfo.Name;
            Name = coinInfo.CoinName;
            Description = coinInfo.FullName;

            InfoUrl = baseLinkUrl + coinInfo.Url;
            ImageUrl = baseImageUrl + coinInfo.ImageUrl;

            Algorithm = coinInfo.Algorithm;
            ProofType = coinInfo.ProofType;
            IsFullyPremined = Convert.ToBoolean(Convert.ToInt32( coinInfo.FullyPremined));

            TotalCoinSupply = coinInfo.TotalCoinSupply;
            PreMinedValue = coinInfo.PreMinedValue;
            TotalCoinsFreeFloat = coinInfo.TotalCoinsFreeFloat;
        }


    }
}
