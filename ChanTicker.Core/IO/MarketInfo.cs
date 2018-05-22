using System;

namespace ChainTicker.Core.IO
{
    public class MarketInfo : IEquatable<MarketInfo>
    {

        public string ExchangeName { get; }
        public string MarketDescription { get; }


        public MarketInfo(string exchangeName, string marketDescription)
        {
            ExchangeName = exchangeName;
            MarketDescription = marketDescription;
        }


        public bool Equals(MarketInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(ExchangeName, other.ExchangeName) && string.Equals(MarketDescription, other.MarketDescription);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MarketInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ExchangeName.GetHashCode() * 397) ^ MarketDescription.GetHashCode();
            }
        }
    }
}