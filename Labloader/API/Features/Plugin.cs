using System;
using System.Reflection;
using Labloader.API.Enums;
using Labloader.API.Interfaces;

namespace Labloader.API.Features
{
    /// <summary>
    /// The plugin class.
    /// </summary>
    /// <typeparam name="T">The plugin's config.</typeparam>
    public abstract class Plugin<T> : IComparable<Plugin<Config>> where T : Config, new()
    {
        /// <summary>
        /// The plugin's assembly.
        /// </summary>
        public Assembly Assembly { get; internal set; }
        
        /// <summary>
        /// The plugin's file name and extension.
        /// </summary>
        public string File { get; internal set; }
        
        /// <summary>
        /// The plugin's version.
        /// </summary>
        public abstract Version Version { get; internal set; }
        
        /// <summary>
        /// The plugin's author.
        /// </summary>
        public abstract string Author { get; internal set; }
        
        /// <summary>
        /// The plugin's name.
        /// </summary>
        public abstract string Name { get; internal set; }

        /// <summary>
        /// Determines when the plugin will load (and when its events will run) compared to other plugins.
        /// </summary>
        public PluginPriority Priority { get; internal set; } = PluginPriority.Normal;
        
        /// <summary>
        /// The plugin's config.
        /// </summary>
        public T Config { get; internal set; }

        /// <summary>
        /// Called when the plugin is enabled.
        /// </summary>
        public void OnEnabled()
        {
            Log.Info(Name+" (version "+Version+") by "+Author+" has been enabled!");
        }

        /// <summary>
        /// Called when the plugin is disabled.
        /// </summary>
        public void OnDisabled()
        {
            Log.Info(Name+" (version "+Version+") by "+Author+" has been disabled!");
        }
        
        public int CompareTo(Plugin<Config> other) => -Priority.CompareTo(other.Priority);
    }


}