using HarmonyLib;
using Labloader.Events.EventArgs;

namespace Labloader.Events.Patches.Player
{
    [HarmonyPatch(typeof(NetworkManager), nameof(NetworkManager.CreateNewPlayer))]
    internal class PlayerJoined
    {
        internal static void Postfix(NetworkManager __instance, ushort id)
        {
            if (__instance.connectedPlayers.TryGetValue(id, out var player))
            {
                var plyObj = player.playerObject.gameObject;
                var apiPlayer = plyObj.AddComponent<API.Features.Player>();

                Events.OnPlayerJoined(new PlayerJoinedEventArgs(apiPlayer));
            }
        }
    }
}