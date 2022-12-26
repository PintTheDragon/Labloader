using System;
using Labloader.API.Features;
using Labloader.Events.BaseEventListeners;
using Labloader.Events.Patches;
using Labloader.Plugins;

namespace Labloader
{
    public static class Loader
    {
        public static void Main()
        {
            try
            {
                PluginLoader.LoadPlugins();
                Patcher.Patch();
                Listeners.Register();
            }
            catch (Exception e)
            {
                Log.Error(e);
                Log.Error("Failed to start Labloader!");
            }
        }
    }
}