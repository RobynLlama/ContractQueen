using ContractQueen.Behaviors;
using HarmonyLib;
using Unity.VisualScripting;
using YAPYAP.Npc.Frog;

namespace ContractQueen.Patches;

internal static class FrogStateMachinePatches
{
  [HarmonyPatch(typeof(FrogStateMachine), "Awake"), HarmonyPostfix]
  internal static void AwakePatch(FrogStateMachine __instance)
  {
    //ContractQueenPlugin.Log.LogMessage("Adding frog contract behavior to a frog");
    __instance.AddComponent<FrogContractBehavior>();
  }
}
