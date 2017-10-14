using ChainTicker.Shell.Models;

namespace ChainTicker.Shell.Helpers
{
    public static class PriceDirectionCalculator
    {

        public static PriceDirection GetPriceDirection(decimal? previousPrice, decimal? currentPrice)
        {
            var previous = previousPrice.GetValueOrDefault();
            var current = currentPrice.GetValueOrDefault();

            if (current == previous)
                return PriceDirection.Level;
            else
                return current < previous ? PriceDirection.Down : PriceDirection.Up;
        }

    }
}