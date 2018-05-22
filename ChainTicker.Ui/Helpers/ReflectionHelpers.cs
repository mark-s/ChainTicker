using System.Reflection;

namespace ChainTicker.Ui.Helpers
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
