using System;
using Labloader.Core.API.Features;
using Labloader.Core.Events.EventArgs;
using UnityEngine.Events;

namespace Labloader.Core.Events.BaseEventListeners.Player
{
    public class PlayerJoined : SimpleListener<ushort>
    {
        public override UnityEvent<ushort> Event => NetworkManager.instance.serverClientJoined;

        public override void Run(ushort id)
        {
            if (!NetworkManager.instance.connectedPlayers.TryGetValue(id, out var player)) return;

            var plyObj = player.player != null ? player.player.gameObject : player.playerObject.gameObject;
            var apiPlayer = plyObj.AddComponent<API.Features.Player>();

            Core.Events.Events.OnPlayerJoined(new PlayerJoinedEventArgs(apiPlayer));
        }
    }
}