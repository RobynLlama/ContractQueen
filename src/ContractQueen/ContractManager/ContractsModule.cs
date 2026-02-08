using System.Collections.Generic;
using Unity.VisualScripting;
using YAPYAP;

namespace ContractQueen.ContractManager;

public sealed class ContractsModule
{
  private readonly string GUID;
  private readonly SortedSet<ContractBundle> _contracts;

  public ContractsModule(string guid)
  {
    GUID = guid;
    _contracts = ContractsRegistry.EnsureGUID(GUID);
  }

  public bool RegisterContract(ContractBundle contract) => _contracts.Add(contract);
  public bool RegisterContract(string name, GameplayTaskSO contract) => _contracts.Add(new(name, contract));
  public void RegisterMany(IEnumerable<ContractBundle> items) => _contracts.AddRange(items);
}
