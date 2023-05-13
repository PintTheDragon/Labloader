using Labloader.Core.Events.EventArgs;
using UnityEngine.Events;

namespace Labloader.Core.Events.BaseEventListeners.Player
{
    public class PlayerJoined : SimpleListener<ushort>
    {
        public override UnityEvent<ushort> Event => NetworkManager.instance.serverClientJoined;

        public override void Run(ushort id)
        {
            var apiPlayer = API.Features.Player.Get(id);
            if (!apiPlayer.IsValid) return;
            Core.Events.Events.OnPlayerJoined(new PlayerJoinedEventArgs(apiPlayer));
        }
    }
}