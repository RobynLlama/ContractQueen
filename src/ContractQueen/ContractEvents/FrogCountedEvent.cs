using System;
using ContractQueen.Behaviors;
using YAPYAP;

namespace ContractQueen.ContractEvents;

/// <summary>
/// These events fire to handle progression for
/// the built in contracts
/// </summary>
public static partial class Events
{

  /// <summary>
  /// Fired when a frog should be counted for the rescue frog contract
  /// </summary>
  public static event Action<FrogContractBehavior>? FrogCountedEvent;

  internal static void CountFrog(FrogContractBehavior frog)
  {
    //only count frogs dropped off at home
    if (GameManager.Instance.CurrentGameState == GameManager.GameState.Lobby)
      FrogCountedEvent?.Invoke(frog);

    frog.Count();
  }
}
