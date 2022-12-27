namespace Labloader.Core.Events.BaseEventListeners
{
    public interface IBaseEventListener
    {
        void Register();
        void Unregister();
    }
}