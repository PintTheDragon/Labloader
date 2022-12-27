using System;
using Labloader.Core.Events;

namespace Labloader.Core.Plugins
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EventHandlerAttribute : Attribute
    {
        internal EventType EventType;

        public EventHandlerAttribute(EventType eventType)
        {
            EventType = eventType;
        }
    }
}