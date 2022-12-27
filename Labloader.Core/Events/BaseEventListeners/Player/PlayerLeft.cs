using Labloader.Core.Events.EventArgs;
using UnityEngine.Events;

namespace Labloader.Core.Events.BaseEventListeners.Player
{
    public class PlayerLeft : SimpleListener<ushort>
    {
        public override UnityEvent<ushort> Event => NetworkManager.instance.serverClientLeft;

        public override void Run(ushort id)
        {
            if (!NetworkManager.instance.connectedPlayers.TryGetValue(id, out var player)) return;

            var plyObj = player.playerObject.gameObject;
            var apiPlayer = plyObj.GetComponent<API.Features.Player>();

            Core.Events.Events.OnPlayerLeft(new PlayerLeftEventArgs(apiPlayer));
        }
    }
}