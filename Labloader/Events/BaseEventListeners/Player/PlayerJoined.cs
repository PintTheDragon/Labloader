using Labloader.Events.EventArgs;
using UnityEngine.Events;

namespace Labloader.Events.BaseEventListeners.Player
{
    public class PlayerJoined : SimpleListener<ushort>
    {
        public override UnityEvent<ushort> Event => NetworkManager.instance.serverClientJoined;

        public override void Run(ushort id)
        {
            if (!NetworkManager.instance.connectedPlayers.TryGetValue(id, out var player)) return;

            var plyObj = player.playerObject.gameObject;
            var apiPlayer = plyObj.AddComponent<API.Features.Player>();

            Events.OnPlayerJoined(new PlayerJoinedEventArgs(apiPlayer));
        }
    }
}