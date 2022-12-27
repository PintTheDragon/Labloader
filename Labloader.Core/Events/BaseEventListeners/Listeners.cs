using System;
using System.Linq;
using System.Reflection;
using Labloader.Core.API.Features;

namespace Labloader.Core.Events.BaseEventListeners
{
    public static class Listeners
    {
        private static IBaseEventListener[] _registeredListeners = null;
        
        public static void Register()
        {
            var listeners = typeof(Listeners).Assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && typeof(IBaseEventListener).IsAssignableFrom(type))
                .Select(type => (IBaseEventListener) Activator.CreateInstance(type))
                .ToArray();

            foreach (var listener in listeners)
            {
                listener.Register();
            }

            _registeredListeners = listeners;
        }

        public static void Unregister()
        {
            // TODO: Use this.
            if (_registeredListeners == null) return;

            foreach (var listener in _registeredListeners)
            {
                listener.Unregister();
            }
        }
    }
}