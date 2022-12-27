using System;
using System.Reflection;
using Labloader.Core.API.Enums;
using Labloader.Core.API.Interfaces;

namespace Labloader.Core.API.Features
{
    /// <summary>
    /// The plugin class.
    /// </summary>
    /// <typeparam name="T">The plugin's config.</typeparam>
    public abstract class Plugin<T> : IPlugin<T>
        where T : IConfig, new()
    {
        /// <inheritdoc/>
        public Assembly Assembly { get; set; }
        
        /// <inheritdoc/>
        public string File { get; set; }
        
        /// <inheritdoc/>
        public abstract Version Version { get; }
        
        /// <inheritdoc/>
        public abstract string Author { get; }
        
        /// <inheritdoc/>
        public abstract string Name { get; }

        /// <inheritdoc/>
        public PluginPriority Priority { get; } = PluginPriority.Normal;
        
        /// <inheritdoc/>
        public T Config { get; set; }

        /// <inheritdoc/>
        public virtual void OnEnabled()
        {
            Log.Info(Name+" (version "+Version+") by "+Author+" has been enabled!");
        }

        /// <inheritdoc/>
        public virtual void OnDisabled()
        {
            Log.Info(Name+" (version "+Version+") by "+Author+" has been disabled!");
        }
        
        public int CompareTo(IPlugin<IConfig> other) => -Priority.CompareTo(other.Priority);
    }
}