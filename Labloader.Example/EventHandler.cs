using Labloader.Core.API.Features;
using Labloader.Core.Events;
using Labloader.Core.Events.EventArgs;
using Labloader.Core.Plugins;

namespace Labloader.Example
{
    public static class EventHandler
    {
        [EventHandler(EventType.PlayerJoined)]
        public static void OnJoin(PlayerJoinedEventArgs ev)
        {
            Log.Info(Plugin.Singleton.Config.PlayerJoinMessage);
        }
    }
}