using Labloader.Core.Events.EventArgs;
using UnityEngine.Events;

namespace Labloader.Core.Events.BaseEventListeners.Player
{
    public class PlayerLeft : SimpleListener<ushort>
    {
        public override UnityEvent<ushort> Event => NetworkManager.instance.serverClientLeft;

        public override void Run(ushort id)
        {
            var player = API.Features.Player.Get(id);
            if (id == 0) return;
            Core.Events.Events.OnPlayerLeft(new PlayerLeftEventArgs(player));
        }
    }
}