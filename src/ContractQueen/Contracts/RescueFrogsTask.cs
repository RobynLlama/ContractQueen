#if DEBUG

using System;
using ContractQueen.Events;
using UnityEngine;
using YAPYAP;

namespace ContractQueen.Contracts;

public class RescueFrogsTask : GameplayTaskSO
{

  [SerializeField]
  private int frogCount = 5;

  public static RescueFrogsTask Factory()
  {
    return new()
    {
      nameLocalisationKey = ContractQueenPlugin.contractName,
      descriptionLocalisationKey = ContractQueenPlugin.contractDesc,
      pointValue = 30000
    };
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
    var del = () => runtimeTask.AdvanceProgress();
    runtimeTask.SetProgressHandler(del);
    EventManager.TestEvent += del;
  }

  public override void UnsubscribeFromProgressEvents(GameplayTask runtimeTask)
  {
    if (runtimeTask.GetProgressHandler() is not Action value)
      return;

    EventManager.TestEvent -= value;
    runtimeTask.SetProgressHandler(null);
  }
}

#endif
