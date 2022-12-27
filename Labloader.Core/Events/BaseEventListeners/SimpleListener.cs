using UnityEngine.Events;

namespace Labloader.Core.Events.BaseEventListeners
{
    public abstract class SimpleListener<T> : IBaseEventListener
    {
        public abstract UnityEvent<T> Event { get; }
        public abstract void Run(T value);

        public void Register()
        {
            Event.AddListener(Run);
        }

        public void Unregister()
        {
            Event.RemoveListener(Run);
        }
    }
}