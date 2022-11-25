using System;
using System.IO;
using Labloader.API.Features;
using Newtonsoft.Json;
using Labloader.API.Interfaces;
using UnityEngine;

namespace Labloader.Plugins
{
    internal static class ConfigManager
    {
        internal static Config AddConfig(string name, object configType)
        {
            Config config;
            
            var path = Path.Combine(Paths.Config, name.Replace(".", "").Replace("/", "").Replace("\\", "")+".json");
            if (!File.Exists(path))
            {
                if (!Directory.Exists(Path.GetDirectoryName(path))) Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, JsonConvert.SerializeObject(configType, Formatting.Indented));
            }
            try
            {
                config = (Config) JsonConvert.DeserializeObject(File.ReadAllText(path));
            }
            catch (NullReferenceException)
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(configType, Formatting.Indented));
                config = (Config) configType;
            }
            catch (JsonException)
            {
                File.Move(path, Path.Combine(Paths.Config, "INVALID-" + name.Replace(".", "").Replace("/", "").Replace("\\", "")+".json"));
                File.WriteAllText(path, JsonConvert.SerializeObject(configType, Formatting.Indented));
                config = (Config) configType;
            }

            return config;
        }
    }
}