using YAPYAP;

namespace ContractQueen.ContractManager;

/// <summary>
/// A tuple of information about a contract
/// </summary>
/// <param name="name">A friendly name for the contract</param>
/// <param name="contract">The scriptable object responsible for handling contract logic</param>
public class ContractBundle(string name, GameplayTaskSO contract)
{

  /// <summary>
  /// A friendly name for the contract
  /// </summary>
  public readonly string Name = name;

  /// <summary>
  /// The scriptable object responsible for handling contract logic
  /// </summary>
  public readonly GameplayTaskSO Contract = contract;
}
