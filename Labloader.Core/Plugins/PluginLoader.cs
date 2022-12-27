using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Labloader.Core.API.Features;
using Labloader.Core.API.Interfaces;

namespace Labloader.Core.Plugins
{
    public static class PluginLoader
    {
        private static readonly Dictionary<string, IPlugin<IConfig>> LoadedPlugins = new Dictionary<string, IPlugin<IConfig>>();
        
        private static readonly List<IPlugin<IConfig>> TempPlugins = new List<IPlugin<IConfig>>();

        public static void LoadPlugins()
        {
            // This check shouldn't be needed, as the dependencies check
            // should include it.
            if (!Directory.Exists(Paths.Plugins))
            {
                Directory.CreateDirectory(Paths.Plugins);
            }

            var plugins = Directory.GetFiles(Paths.Plugins);
            Log.Info("Loading " + plugins.Length + " plugins.");

            for (var i = 0; i < plugins.Length; i++)
            {
                LoadPlugin(i+1, plugins.Length, plugins[i]);
            }

            EnablePlugins();
        }

        private static void LoadPlugin(int index, int count, string path)
        {
            if (!path.EndsWith(".dll"))
            {
                return;
            }

            var fileName = Path.GetFileName(path);
            if (!LoadedPlugins.ContainsKey(fileName))
            {
                var assembly = Assembly.Load(File.ReadAllBytes(path));

                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (!type.IsSubclassOf(typeof(Plugin<>)) || type == typeof(Plugin<>)) continue;
                        
                        var plugin = (IPlugin<IConfig>) Activator.CreateInstance(type);
                            
                        plugin.Config = ConfigManager.AddConfig(plugin.Name, Activator.CreateInstance(plugin.Config.GetType()));
                        plugin.File = fileName;
                        plugin.Assembly = assembly;
                            
                        TempPlugins.Add(plugin);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(string.Concat(new object[]
                    {
                        "Loading plugin ",
                        fileName,
                        " failed. (",
                        index,
                        "/",
                        count,
                        ")",
                        Environment.NewLine,
                        ex.ToString()
                    }));
                }
            }
            else
            {
                Log.Warn(string.Concat(new object[]
                {
                    "Plugin ",
                    fileName,
                    " is already loaded. (",
                    index,
                    "/",
                    count,
                    ")"
                }));
            }
        }

        private static void EnablePlugins()
        {
            TempPlugins.Sort();
            
            for (var i = 0; i < TempPlugins.Count; i++)
            {
                var plugin = TempPlugins[i];
                
                LoadedPlugins.Add(plugin.File, plugin);
                
                plugin.Enable();

                Log.Info(string.Concat(new object[]
                {
                    "Plugin ",
                    plugin.File,
                    " loaded. (",
                    i+1,
                    "/",
                    TempPlugins.Count,
                    ")"
                }));
            }

            TempPlugins.Clear();
        }

        private static void Enable(this IPlugin<IConfig> plugin)
        {
            foreach (var type in plugin.Assembly.GetTypes())
            {
                foreach (var methodInfo in type.GetMethods())
                {
                    var customAttribute = methodInfo.GetCustomAttribute<EventHandlerAttribute>();
                    if (customAttribute == null) continue;
                    
                    if (!methodInfo.IsStatic)
                    {
                        Log.Warn("[" + plugin.Name + "] Could not register " + type.FullName + "." + methodInfo.Name + " as an event handler as it is not static!");
                        continue;
                    }
                    
                    // TODO: Add an error for incorrect arguments.
                    
                    var eventInfo = Events.Events.GetEvent(customAttribute.EventType);
                    eventInfo.AddEventHandler(null, Delegate.CreateDelegate(eventInfo.EventHandlerType, methodInfo));
                }
            }

            plugin.OnEnabled();
        }

        private static void Disable(this IPlugin<IConfig> plugin)
        {
            foreach (var type in plugin.Assembly.GetTypes())
            {
                foreach (var methodInfo in type.GetMethods())
                {
                    var customAttribute = methodInfo.GetCustomAttribute<EventHandlerAttribute>();
                    if (customAttribute == null) continue;
                    
                    var eventInfo = Events.Events.GetEvent(customAttribute.EventType);
                    eventInfo.RemoveEventHandler(null, Delegate.CreateDelegate(eventInfo.EventHandlerType, methodInfo));
                }
            }

            plugin.OnDisabled();
        }
    }
}