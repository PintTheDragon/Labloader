using System;
using Labloader.Events;

namespace Labloader.Plugins
{
    public class EventAttribute : Attribute
    {
        internal EventType EventType;

        public EventAttribute(EventType eventType)
        {
            EventType = eventType;
        }
    }
}