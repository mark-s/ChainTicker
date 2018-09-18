using System;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Core.Services
{
    public class TimeService : ITimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
