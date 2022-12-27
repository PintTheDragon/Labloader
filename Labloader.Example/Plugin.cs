using System;
using Labloader.Core.API.Features;

namespace Labloader.Example
{
    public class Plugin : Plugin<PluginConfig>
    {
        public override Version Version { get; } = new Version(1, 0, 0);
        public override string Author { get; } = "PintTheDragon";
        public override string Name { get; } = "Example Plugin";

        public static Plugin Singleton;

        public override void OnEnabled()
        {
            Singleton = this;

            Log.Info("I exist!");
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Log.Info("I don't exist :(");

            Singleton = null;
            
            base.OnDisabled();
        }
    }
}