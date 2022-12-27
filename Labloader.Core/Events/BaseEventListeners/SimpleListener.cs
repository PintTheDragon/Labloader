using System;
using Labloader.Core.API.Features;
using UnityEngine.Events;

namespace Labloader.Core.Events.BaseEventListeners
{
    public abstract class SimpleListener<T> : IBaseEventListener
    {
        public abstract UnityEvent<T> Event { get; }
        public abstract void Run(T value);

        public void Register()
        {
            Event.AddListener(InternalRun);
        }

        public void Unregister()
        {
            Event.RemoveListener(InternalRun);
        }

        private void InternalRun(T arg)
        {
            try
            {
                Run(arg);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}