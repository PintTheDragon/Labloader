using Labloader.Core.Events.EventArgs;
using UnityEngine.Events;

namespace Labloader.Core.Events.BaseEventListeners.Player
{
    public class PlayerLeft : SimpleListener<ushort>
    {
        public override UnityEvent<ushort> Event => NetworkManager.instance.serverClientLeft;

        public override void Run(ushort id)
        {
            if (id == 0) return;
            
            var player = API.Features.Player.Get(id);
            
            if(player)
                Core.Events.Events.OnPlayerLeft(new PlayerLeftEventArgs(player));
        }
    }
}