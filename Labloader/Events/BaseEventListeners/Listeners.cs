using System;
using System.Linq;

namespace Labloader.Events.BaseEventListeners
{
    public static class Listeners
    {
        private static IBaseEventListener[] _registeredListeners = null;
        
        public static void Register()
        {
            var listeners = (IBaseEventListener[]) typeof(Listeners).Assembly.GetTypes()
                .Where(type => type.IsAssignableFrom(typeof(IBaseEventListener))).Select(Activator.CreateInstance).ToArray();

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