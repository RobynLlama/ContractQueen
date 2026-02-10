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
    ContractQueenPlugin.Log.LogDebug($"OnDropped: {__instance.gameObject.name} | {__instance.DisplayName}({__instance.GetInstanceID()}), isFrog: {frog != null}");

    if (frog == null || frog.HasBeenCounted)
      return;

    ContractQueenPlugin.Log.LogDebug($"New frog: {frog.HasBeenCounted == false}");

    Events.CountFrog(frog);
  }

}
