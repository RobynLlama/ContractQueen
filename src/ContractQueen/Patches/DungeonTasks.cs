using ContractQueen.ContractManager;
using HarmonyLib;
using YAPYAP;

namespace ContractQueen.Patches;

internal class DungeonTasksPatches
{

  [HarmonyPatch(typeof(DungeonTasks), "Awake"), HarmonyPostfix]
  internal static void AddContractsToRandomPool()
  {
    var customContracts = ContractsRegistry.LockedList;
    var constants = DungeonTasks.Instance.constantTasks;
    var rng = DungeonTasks.Instance.randomTasks;
    var collectibles = DungeonTasks.Instance.collectableTasks;

    int startID = constants.Length + rng.Length + collectibles.Length;
    int i = 0;

    foreach (var contract in customContracts)
    {
      contract.TaskId = startID + i;
      i++;
    }

    DungeonTasks.Instance.randomTasks = [.. rng, .. customContracts];
    DungeonTasks.Instance.allTasks = [.. constants, .. rng, .. collectibles];

    ContractQueenPlugin.Log.LogMessage($"Added {i} contract(s) to pool starting from ID {startID} to {startID + i - 1}");
  }
}
