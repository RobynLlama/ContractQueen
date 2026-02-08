using System;
using ContractQueen.Behaviors;
using ContractQueen.ContractEvents;
using UnityEngine;
using YAPYAP;

namespace ContractQueen.Contracts;

public class RescueFrogsTask : GameplayTaskSO
{

  [SerializeField]
  private int frogCount = 5;

  public static RescueFrogsTask Factory()
  {
    var frog = new RescueFrogsTask()
    {
      nameLocalisationKey = ContractQueenPlugin.contractName,
      descriptionLocalisationKey = ContractQueenPlugin.contractDesc,
      pointValue = 200
    };

    return frog;
  }

  public override bool CanBeCreated()
  {
    return true;
  }

  protected override int CalculateTargetProgress()
  {
    return frogCount;
  }

  public override void SubscribeToProgressEvents(GameplayTask runtimeTask)
  {
    var del = (FrogContractBehavior frog) =>
    {
      ContractQueenPlugin.Log.LogMessage("Counted a frog for a quest");
      runtimeTask.AdvanceProgress();
    };

    runtimeTask.SetProgressHandler(del);
    Events.FrogCountedEvent += del;
  }

  public override void UnsubscribeFromProgressEvents(GameplayTask runtimeTask)
  {
    if (runtimeTask.GetProgressHandler() is not Action<FrogContractBehavior> value)
      return;

    Events.FrogCountedEvent -= value;
    runtimeTask.SetProgressHandler(null);
  }
}
