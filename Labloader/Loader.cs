using Labloader.Events.Patches;
using Labloader.Plugins;

namespace Labloader
{
    public static class Loader
    {
        public static void Main()
        {
            PluginLoader.LoadPlugins();
            Patcher.Patch();
        }
    }
}