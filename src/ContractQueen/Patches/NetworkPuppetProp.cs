using ContractQueen.Events;
using HarmonyLib;
using YAPYAP;

namespace ContractQueen.Patches;

internal static class NetworkPuppetPropPatches
{
  [HarmonyPatch(typeof(NetworkPuppetProp), "OnPickedUp"), HarmonyPostfix]
  internal static void ServerSetInInventoryPatch(NetworkPuppetProp __instance) =>
    EventManager.ItemPickedUp(__instance);

}
