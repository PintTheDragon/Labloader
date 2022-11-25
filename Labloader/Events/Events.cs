using System;
using System.Reflection;
using Labloader.Events.EventArgs;

namespace Labloader.Events
{
    internal static class Events
    {
        internal static EventInfo GetEvent(EventType eventType)
        {
            return typeof(Events).GetEvent(eventType.ToString());
        }

        internal static event EventHandler<PlayerJoinedEventArgs> PlayerJoinedEvent;
    }
}