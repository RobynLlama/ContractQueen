using ContractQueen.Behaviors;
using ContractQueen.Persist;
using FrogDataLib.DataManagement;
using HarmonyLib;
using Unity.VisualScripting;
using YAPYAP.Npc.Frog;

namespace ContractQueen.Patches;

internal static class FrogStateMachinePatches
{
  [HarmonyPatch(typeof(FrogStateMachine), "Awake"), HarmonyPostfix]
  internal static void AwakePatch(FrogStateMachine __instance)
  {
    //Ignore the pre-placed frogs
    if (__instance.gameObject.GetInstanceID() > 0)
      return;

    var comp = __instance.AddComponent<FrogContractBehavior>();
    AssetMUD token = new(__instance);

    var dto = ContractQueenPlugin.Database.GetFrogData(token);
    comp.Data = dto;
  }
}
