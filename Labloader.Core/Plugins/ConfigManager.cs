using System;
using System.IO;
using Labloader.Core.API.Features;
using Labloader.Core.API.Interfaces;
using Newtonsoft.Json;

namespace Labloader.Core.Plugins
{
    internal static class ConfigManager
    {
        internal static IConfig AddConfig(string name, object configType)
        {
            if (!Directory.Exists(Paths.Config))
            {
                Directory.CreateDirectory(Paths.Config);
            }
            
            IConfig config;

            var path = Path.Combine(Paths.Config, name.Replace(".", "").Replace("/", "").Replace("\\", "")+".json");
            if (!File.Exists(path))
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(configType, Formatting.Indented));
            }
            
            try
            {
                config = (IConfig) JsonConvert.DeserializeObject(File.ReadAllText(path));
            }
            catch (NullReferenceException)
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(configType, Formatting.Indented));
                config = (IConfig) configType;
            }
            catch (JsonException)
            {
                File.Move(path, Path.Combine(Paths.Config, "INVALID-" + name.Replace(".", "").Replace("/", "").Replace("\\", "")+".json"));
                File.WriteAllText(path, JsonConvert.SerializeObject(configType, Formatting.Indented));
                config = (IConfig) configType;
            }

            return config;
        }
    }
}