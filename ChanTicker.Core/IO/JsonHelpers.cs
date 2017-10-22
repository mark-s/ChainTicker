using System;
using System.Text.RegularExpressions;

namespace ChanTicker.Core.IO
{
    public static class JsonHelpers
    {
        // Might make sense to pass this in...
        private static readonly Regex _typematcher = new Regex("\"type\":\"([^\"]*)\"",
                                                        RegexOptions.Compiled | RegexOptions.CultureInvariant,
                                                        TimeSpan.FromSeconds(1));

        public static string GetType(string jsonText)
        {
            var match = _typematcher.Match(jsonText);
            return match.Success == false ? null : match.Groups[1].Value;
        }
    }
}