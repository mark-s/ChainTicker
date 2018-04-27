using ChainTicker.Shell.Models;

namespace ChainTicker.Shell.Helpers
{
    public static class PriceDirectionCalculator
    {
        public static PriceDirection GetPriceDirection(decimal? previousPrice, decimal? currentPrice, PriceDirection previousPriceDirection)
        {
            var previous = previousPrice.GetValueOrDefault();
            var current = currentPrice.GetValueOrDefault();

            if (current == previous)
                return previousPriceDirection;

            var currentDirection = current < previous ? PriceDirection.Down : PriceDirection.Up;

            if (previousPriceDirection == currentDirection)
                return previousPriceDirection;
            else
                return currentDirection;
        }

    }
}