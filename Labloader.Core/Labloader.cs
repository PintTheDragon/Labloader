using System;
using Labloader.Core.API.Features;
using Labloader.Core.Events.BaseEventListeners;
using Labloader.Core.Events.Patches;
using Labloader.Core.Plugins;

namespace Labloader.Core
{
    public static class Labloader
    {
        public static void Init()
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