using ContractQueen.Behaviors;
using ContractQueen.ContractEvents;
using HarmonyLib;
using YAPYAP;

namespace ContractQueen.Patches;

internal static class NetworkPuppetPropPatches
{
  [HarmonyPatch(typeof(NetworkPuppetProp), "OnDropped"), HarmonyPostfix]
  internal static void ServerSetInInventoryPatch(NetworkPuppetProp __instance)
  {
    var frog = __instance.GetComponent<FrogContractBehavior>();

    if (frog == null || frog.HasBeenCounted)
      return;

    Events.CountFrog(frog);
  }

}
