using System;
using System.IO;
using Labloader.Core.API.Features;
using Labloader.Core.API.Interfaces;
using Newtonsoft.Json;

namespace Labloader.Core.Plugins
{
    internal static class ConfigManager
    {
        internal static object AddConfig(string name, object configType)
        {
            if (!Directory.Exists(Paths.Config))
            {
                Directory.CreateDirectory(Paths.Config);
            }
            
            object config;

            var path = Path.Combine(Paths.Config, name.Replace(".", "").Replace("/", "").Replace("\\", "")+".json");
            if (!File.Exists(path))
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(configType, Formatting.Indented));
            }
            
            try
            {
                config = JsonConvert.DeserializeObject(File.ReadAllText(path), configType.GetType());
            }
            catch (NullReferenceException)
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(configType, Formatting.Indented));
                config = configType;
            }
            catch (JsonException)
            {
                File.Move(path, Path.Combine(Paths.Config, "INVALID-" + name.Replace(".", "").Replace("/", "").Replace("\\", "")+".json"));
                File.WriteAllText(path, JsonConvert.SerializeObject(configType, Formatting.Indented));
                config = configType;
            }

            return config;
        }
    }
}