namespace ChainTicker.DataSource.Coins.Domain
{
    public abstract class CoinBase 
    {
        public bool IsValid { get; protected set; }
        
        public string Code { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }
        
    }
}