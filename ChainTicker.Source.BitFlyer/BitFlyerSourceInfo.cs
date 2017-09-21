using ChanTicker.Core.Interfaces;

namespace ChainTicker.Source.BitFlyer
{
    internal class BitFlyerSourceInfo : ISourceInfo
    {
        public string Name => "BitFlyer";
        public string Uri => "THE URL";
        public string Description => "BitFlyer";
        public bool IsEnabled => true;
    }
}
