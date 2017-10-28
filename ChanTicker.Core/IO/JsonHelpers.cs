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


        public static T GetType<T>(string jsonText) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Not an enum");

            if (Enum.TryParse(GetType(jsonText), true, out T toReturn))
                return toReturn;
            else
                return default(T);
        }

    }
}