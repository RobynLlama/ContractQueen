using System;
using ContractQueen.Behaviors;
using ContractQueen.ContractEvents;
using UnityEngine;
using YAPYAP;

namespace ContractQueen.Contracts;

/// <summary>
/// A simple contract to retrieve X frogs from the tower and drop them in the Hub
/// </summary>
public class RescueFrogsTask : GameplayTaskSO
{

  [SerializeField]
  private int frogCount = 3;

  /// <summary>
  /// A factory for creating new instances of this contract
  /// </summary>
  public static RescueFrogsTask Create()
  {
    var frog = CreateInstance<RescueFrogsTask>();

    frog.nameLocalisationKey = ContractQueenPlugin.contractName;
    frog.descriptionLocalisationKey = ContractQueenPlugin.contractDesc;
    frog.pointValue = 300;

    return frog;
  }

  /// <inheritdoc/>
  public override bool CanBeCreated()
  {
    return true;
  }

  /// <inheritdoc/>
  protected override int CalculateTargetProgress()
  {
    return frogCount;
  }

  /// <inheritdoc/>
  public override void SubscribeToProgressEvents(GameplayTask runtimeTask)
  {
    var del = (FrogContractBehavior frog) =>
    {
      ContractQueenPlugin.Log.LogDebug("Counted a frog for a quest");
      runtimeTask.AdvanceProgress();
    };

    runtimeTask.SetProgressHandler(del);
    Events.FrogCountedEvent += del;
  }

  /// <inheritdoc/>
  public override void UnsubscribeFromProgressEvents(GameplayTask runtimeTask)
  {
    if (runtimeTask.GetProgressHandler() is not Action<FrogContractBehavior> value)
      return;

    Events.FrogCountedEvent -= value;
    runtimeTask.SetProgressHandler(null);
  }
}
