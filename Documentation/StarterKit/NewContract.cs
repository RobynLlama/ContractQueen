using System;
using MyMod.Events;
using MyMod.Locales;
using UnityEngine;
using YAPYAP;

namespace MyMod.Contracts;

public class MyNewContract : GameplayTaskSO
{

  [SerializeField]
  private int MaxProgress = 10;

  public static MyNewContract CreateNew()
  {
    var contract = CreateInstance<MyNewContract>();

    contract.nameLocalisationKey = Locale.MyContractName;
    contract.descriptionLocalisationKey = Locale.MyContractDesc;
    contract.pointValue = 100;

    return contract;
  }

  public override bool CanBeCreated()
  {
    return true;
  }

  protected override int CalculateTargetProgress()
  {
    return MaxProgress;
  }

  public override void SubscribeToProgressEvents(GameplayTask runtimeTask)
  {
    var del = () =>
    {
      ContractEvents.FireContractEvent();
    };

    ContractEvents.MyContractEvent += del;
    runtimeTask.SetProgressHandler(del);
  }

  public override void UnsubscribeFromProgressEvents(GameplayTask runtimeTask)
  {
    if (runtimeTask.GetProgressHandler() is not Action del)
      return;

    ContractEvents.MyContractEvent -= del;
    runtimeTask.SetProgressHandler(null);
  }

}
