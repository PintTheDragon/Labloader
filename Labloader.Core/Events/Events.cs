using System;
using System.Reflection;
using Labloader.Core.Events.EventArgs;

namespace Labloader.Core.Events
{
    internal static class Events
    {
        internal static EventInfo GetEvent(EventType eventType)
        {
            return typeof(Events).GetEvent(eventType.ToString());
        }

        internal static event EventHandler<PlayerJoinedEventArgs> PlayerJoined;
        internal static void OnPlayerJoined(PlayerJoinedEventArgs ev) => PlayerJoined?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerLeftEventArgs> PlayerLeft;
        internal static void OnPlayerLeft(PlayerLeftEventArgs ev) => PlayerLeft?.Invoke(null, ev);
    }
}