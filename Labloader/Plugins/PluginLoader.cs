using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Labloader.API.Features;
using Labloader.API.Interfaces;

namespace Labloader.Plugins
{
    public static class PluginLoader
    {
        public static List<string> loadedDependencies = new List<string>();
        public static Dictionary<string, Plugin<Config>> loadedPlugins = new Dictionary<string, Plugin<Config>>();
        
        private static List<Plugin<Config>> tempPlugins = new List<Plugin<Config>>();
        
        public static void LoadPlugins()
        {
            if (!Directory.Exists(Paths.Dependencies))
            {
                Directory.CreateDirectory(Paths.Dependencies);
            }
            
            var deps = Directory.GetFiles(Paths.Dependencies);
            Log.Info("Loading " + deps.Length + " dependencies.");
            
            for (var i = 0; i < deps.Length; i++)
            {
                LoadDependency(i, deps.Length, deps[i]);
            }
            
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

        private static void LoadDependency(int index, int count, string path)
        {
            if (!path.EndsWith(".dll")) return;
            
            var fileName = Path.GetFileName(path);
            if (!loadedDependencies.Contains(fileName))
            {
                loadedDependencies.Add(fileName);
                Log.Info(string.Concat(new object[]
                {
                    "Dependency ",
                    fileName,
                    " loaded. (",
                    (index+1),
                    "/",
                    count,
                    ")"
                }));
                Assembly.Load(File.ReadAllBytes(path));
                return;
            }
            
            Log.Warn(string.Concat(new object[]
            {
                "Dependency ",
                fileName,
                " is already loaded. (",
                (index+1),
                "/",
                count,
                ")"
            }));
        }

        private static void LoadPlugin(int index, int count, string path)
        {
            if (!path.EndsWith(".dll"))
            {
                return;
            }

            var fileName = Path.GetFileName(path);
            if (!loadedPlugins.ContainsKey(fileName))
            {
                var assembly = Assembly.Load(File.ReadAllBytes(path));

                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (!type.IsSubclassOf(typeof(Plugin<>)) || type == typeof(Plugin<>)) continue;
                        
                        var plugin = (Plugin<Config>) Activator.CreateInstance(type);
                            
                        plugin.Config = ConfigManager.AddConfig(plugin.Name, Activator.CreateInstance(plugin.Config.GetType()));
                        plugin.File = fileName;
                        plugin.Assembly = assembly;
                            
                        tempPlugins.Add(plugin);
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
            tempPlugins.Sort();
            
            for (var i = 0; i < tempPlugins.Count; i++)
            {
                var plugin = tempPlugins[i];
                
                loadedPlugins.Add(plugin.File, plugin);
                
                plugin.Enable();

                Log.Info(string.Concat(new object[]
                {
                    "Plugin ",
                    plugin.File,
                    " loaded. (",
                    i+1,
                    "/",
                    tempPlugins.Count,
                    ")"
                }));
            }

            tempPlugins.Clear();
        }

        public static void Enable(this Plugin<Config> plugin)
        {
            foreach (var type in plugin.Assembly.GetTypes())
            {
                foreach (var methodInfo in type.GetMethods())
                {
                    var customAttribute = methodInfo.GetCustomAttribute<EventAttribute>();
                    if (customAttribute == null) continue;
                    
                    var eventInfo = Events.Events.GetEvent(customAttribute.EventType);
                    eventInfo.AddEventHandler(null, Delegate.CreateDelegate(eventInfo.EventHandlerType, methodInfo));
                }
            }

            plugin.OnEnabled();
        }

        public static void Disable(this Plugin<Config> plugin)
        {
            foreach (var type in plugin.Assembly.GetTypes())
            {
                foreach (var methodInfo in type.GetMethods())
                {
                    var customAttribute = methodInfo.GetCustomAttribute<EventAttribute>();
                    if (customAttribute == null) continue;
                    
                    var eventInfo = Events.Events.GetEvent(customAttribute.EventType);
                    eventInfo.RemoveEventHandler(null, Delegate.CreateDelegate(eventInfo.EventHandlerType, methodInfo));
                }
            }

            plugin.OnDisabled();
        }
    }
}