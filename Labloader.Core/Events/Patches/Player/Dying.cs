using HarmonyLib;
using Labloader.Core.Events.EventArgs;

namespace Labloader.Core.Events.Patches.Player
{
    [HarmonyPatch(typeof(Health), nameof(Health.Kill))]
    internal class Dying
    {
        internal static bool Prefix(Health __instance, DeathMessage message, IKiller killer)
        {
            var ev = new PlayerDyingEventArgs(API.Features.Player.Get(killer?.gameObject),
                API.Features.Player.Get(__instance.gameObject));
            Events.OnPlayerDying(ev);

            return ev.IsAllowed;
        }
    }
}