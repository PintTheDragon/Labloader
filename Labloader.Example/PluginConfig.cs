using Labloader.Core.API.Interfaces;

namespace Labloader.Example
{
    public class PluginConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public string PlayerJoinMessage { get; set; } = "New player!";
    }
}