using System;

namespace ChainTicker.Core.Interfaces
{
    public interface ITimeService
    {
        DateTime Now { get; }
    }
}
