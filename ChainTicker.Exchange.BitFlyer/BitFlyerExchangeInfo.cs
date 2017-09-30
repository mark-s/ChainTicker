using ChanTicker.Core.Interfaces;

namespace ChainTicker.Exchange.BitFlyer
{
    internal class BitFlyerExchangeInfo : IExchangeInfo
    {
        public string Name => "BitFlyer";
        public string Uri => "THE URL";
        public string Description => "BitFlyer";
        public bool IsEnabled => true;
    }
}
