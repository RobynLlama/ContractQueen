using System;
using ContractQueen.Behaviors;
using YAPYAP;

namespace ContractQueen.ContractEvents;

public static partial class Events
{
  public static event Action<FrogContractBehavior>? FrogCountedEvent;

  internal static void CountFrog(FrogContractBehavior frog)
  {
    //only count frogs dropped off at home
    if (GameManager.Instance.CurrentGameState == GameManager.GameState.Lobby)
      FrogCountedEvent?.Invoke(frog);

    frog.Count();
  }
}
