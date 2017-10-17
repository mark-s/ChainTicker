using System.Reflection;

namespace ChainTicker.Shell.Helpers
{
    public class ReflectionHelpers
    {
        public static string GetEntryAssemblyVersion()
        {
            var entryAssembly = Assembly.GetEntryAssembly();

            return entryAssembly?.GetName().Version.ToString();
        }
    }
}
