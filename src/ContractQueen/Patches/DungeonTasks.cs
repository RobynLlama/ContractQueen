using ContractQueen.ContractManager;
using HarmonyLib;
using YAPYAP;

namespace ContractQueen.Patches;

internal class DungeonTasksPatches
{

  [HarmonyPatch(typeof(DungeonTasks), nameof(DungeonTasks.PreAwake)), HarmonyPostfix]
  internal static void AddContractsToRandomPool()
  {
    ContractsRegistry.Lock();

    var customContracts = ContractsRegistry.LockedList;
    var constants = ContractsRegistry.FrozenConstants;
    var rng = ContractsRegistry.FrozenRandoms;
    var collectibles = ContractsRegistry.FrozenCollectibles;

    int startID = constants.Length + rng.Length + collectibles.Length;
    int i = 0;

    foreach (var contract in customContracts)
    {
      contract.TaskId = startID + i;
      i++;
    }

    DungeonTasks.Instance.randomTasks = [.. rng, .. customContracts];
    DungeonTasks.Instance.allTasks = [.. constants, .. rng, .. collectibles, .. customContracts];

    ContractQueenPlugin.Log.LogMessage($"Added {i} contract(s) to pool starting from ID {startID} to {startID + i - 1}");
  }
}
