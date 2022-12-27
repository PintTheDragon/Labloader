using System;
using Labloader.Core.Events;

namespace Labloader.Core.Plugins
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