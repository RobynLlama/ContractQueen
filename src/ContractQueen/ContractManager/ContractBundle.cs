using YAPYAP;

namespace ContractQueen.ContractManager;

public class ContractBundle(string name, GameplayTaskSO contract)
{
  public readonly string Name = name;
  public readonly GameplayTaskSO Contract = contract;
}
