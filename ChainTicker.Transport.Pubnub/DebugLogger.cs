using System.Diagnostics;

namespace ChainTicker.Transport.Pubnub
{
    public class DebugLogger : IPubnubLogger
    {
        public void WriteToLog(string logText) => Debug.WriteLine(logText);

    }
}