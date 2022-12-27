using System;
using System.Reflection;
using Labloader.Core.API.Enums;

namespace Labloader.Core.API.Interfaces
{
    public interface IPlugin<T> : IComparable<IPlugin<IConfig>>
        where T : IConfig
    {
        /// <summary>
        /// The plugin's assembly.
        /// </summary>
        Assembly Assembly { get; set; }
        
        /// <summary>
        /// The plugin's file name and extension.
        /// </summary>
        string File { get; set; }
        
        /// <summary>
        /// The plugin's version.
        /// </summary>
        Version Version { get; }
        
        /// <summary>
        /// The plugin's author.
        /// </summary>
        string Author { get; }
        
        /// <summary>
        /// The plugin's name.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Determines when the plugin will load (and when its events will run) compared to other plugins.
        /// </summary>
        PluginPriority Priority { get; }
        
        /// <summary>
        /// The plugin's config.
        /// </summary>
        T Config { get; set; }

        /// <summary>
        /// Called when the plugin is enabled.
        /// </summary>
        void OnEnabled();

        /// <summary>
        /// Called when the plugin is disabled.
        /// </summary>
        void OnDisabled();
    }
}