using HarmonyLib;

namespace Labloader.Core.Events.Patches
{
    internal static class Patcher
    {
        internal static void Patch()
        {
            var harmony = new Harmony("Labloader");
            harmony.PatchAll();
            
            // TODO: Maybe unpatch or something else?
        }
    }
}