using System.Diagnostics;
using PubnubApi;

namespace ChainTicker.Transport.Pubnub
{
    public class DebugLogger : IPubnubLog
    {
        public void WriteToLog(string logText) => Debug.WriteLine(logText);

    }
}