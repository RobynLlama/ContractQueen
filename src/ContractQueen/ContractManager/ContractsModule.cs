using System.Collections.Generic;
using Unity.VisualScripting;
using YAPYAP;

namespace ContractQueen.ContractManager;

/// <summary>
/// Used to register contracts with the registry
/// </summary>
public sealed class ContractsModule
{
  private readonly string GUID;
  private readonly SortedSet<ContractBundle> _contracts;

  /// <summary>
  /// Creates a new instance of ContractsModule
  /// </summary>
  /// <param name="guid">A unique identifier used for deterministically sorting contracts</param>
  public ContractsModule(string guid)
  {
    GUID = guid;
    _contracts = ContractsRegistry.EnsureGUID(GUID);
  }

  /// <summary>
  /// Registers a contract with the registry
  /// </summary>
  /// <remarks>All registered contracts are added to the Random pool, other pools are not useful or WIP right now</remarks>
  /// <param name="contract">A contract bundle to register</param>
  /// <returns></returns>
  public bool RegisterContract(ContractBundle contract) => _contracts.Add(contract);

  /// <summary>
  /// Registers a contract with the registry by name and scriptable object
  /// </summary>
  /// <remarks>All registered contracts are added to the Random pool, other pools are not useful or WIP right now</remarks>
  /// <param name="name">A friendly name for the contract</param>
  /// <param name="contract">The scriptable object responsible for handling contract logic</param>
  /// <returns></returns>
  public bool RegisterContract(string name, GameplayTaskSO contract) => _contracts.Add(new(name, contract));

  /// <summary>
  /// Registers many contract bundles at once
  /// </summary>
  /// <remarks>All registered contracts are added to the Random pool, other pools are not useful or WIP right now</remarks>
  /// <param name="items">A enumerable list, array, etc of contract bundles</param>
  public void RegisterMany(IEnumerable<ContractBundle> items) => _contracts.AddRange(items);
}
