using ContractQueen.Behaviors;
using HarmonyLib;
using Unity.VisualScripting;
using YAPYAP.Npc.Frog;

namespace ContractQueen.Patches;

internal static class FrogStateMachinePatches
{
  [HarmonyPatch(typeof(FrogStateMachine), "Awake"), HarmonyPostfix]
  internal static void AwakePatch(FrogStateMachine __instance) =>
    __instance.AddComponent<FrogContractBehavior>();
}
