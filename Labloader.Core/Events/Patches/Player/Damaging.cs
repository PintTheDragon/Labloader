using HarmonyLib;
using Labloader.Core.Events.EventArgs;

namespace Labloader.Core.Events.Patches.Player
{
    [HarmonyPatch(typeof(Health), nameof(Health.Damage))]
    internal class Damaging
    {
        internal static bool Prefix(Health __instance, ref float amount, DeathMessage message, IKiller killer,
            float bloodLoss)
        {
            var ev = new PlayerDamagingEventArgs(API.Features.Player.Get(killer?.gameObject),
                API.Features.Player.Get(__instance.gameObject), amount);
            Events.OnPlayerDamaging(ev);

            amount = ev.Damage;

            return ev.IsAllowed;
        }
    }
}