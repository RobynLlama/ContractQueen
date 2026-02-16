using System;
using System.Collections.Generic;
using System.Text;
using YAPYAP;

namespace ContractQueen.ContractManager;

internal static class ContractsRegistry
{
  private static readonly SortedDictionary<string, SortedSet<ContractBundle>> Registry = new(StringComparer.Ordinal);
  private static bool Finalized = false;
  private static IReadOnlyList<GameplayTaskSO> _cachedLockedList = [];
  internal static GameplayTaskSO[] FrozenConstants { get; private set; } = [];
  internal static GameplayTaskSO[] FrozenCollectibles { get; private set; } = [];
  internal static GameplayTaskSO[] FrozenRandoms { get; private set; } = [];
  public static IReadOnlyList<GameplayTaskSO> LockedList => _cachedLockedList;

  internal static SortedSet<ContractBundle> EnsureGUID(string GUID)
  {
    if (Finalized)
      throw new InvalidOperationException("Cannot add quests after main menu is reached");

    if (Registry.TryGetValue(GUID, out var list))
    {
      return list;
    }

    var value = new SortedSet<ContractBundle>(ContractBundleComparer.NameOrdinal);
    Registry[GUID] = value;
    return value;
  }

  internal static void Lock()
  {
    try
    {

      List<GameplayTaskSO> items = [];

      foreach (var item in Registry.Values)
        foreach (var bundle in item)
          items.Add(bundle.Contract);

      FrozenConstants = DungeonTasks.Instance.constantTasks;
      FrozenRandoms = DungeonTasks.Instance.randomTasks;
      FrozenCollectibles = DungeonTasks.Instance.collectableTasks;

      ContractQueenPlugin.Log.LogMessage(ListContracts());

      _cachedLockedList = items.AsReadOnly();
      Finalized = true;
    }
    catch (Exception ex)
    {
      ContractQueenPlugin.Log.LogError($"{ex.Message}\n\n{ex.StackTrace}");
    }
  }

  public static string ListContracts()
  {
    StringBuilder sb = new("Contracts Loaded:\n");

    foreach (var source in Registry)
    {
      sb.AppendLine($"  [{source.Key}]");
      foreach (var item in source.Value)
      {
        sb.AppendLine($"    -{item.Name}");
      }
    }

    return sb.ToString();
  }

}
