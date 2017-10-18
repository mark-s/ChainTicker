using ChainTicker.Shell.Models;

namespace ChainTicker.Shell.Helpers
{
    public static class PriceDirectionCalculator
    {
        public static PriceDirection GetPriceDirection(decimal? previousPrice, decimal? currentPrice, PriceDirection previousPriceDirection)
        {
            var previous = previousPrice.GetValueOrDefault();
            var current = currentPrice.GetValueOrDefault();

            var currentDirection = current < previous ? PriceDirection.Down : PriceDirection.Up;

            if (previousPriceDirection != currentDirection)
                return currentDirection;
            else
                return previousPriceDirection;
        }

    }
}