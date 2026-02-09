using System;

namespace MyMod.Events;

public static partial class ContractEvents
{
  public static event Action? MyContractEvent;
  internal static void FireContractEvent() => MyContractEvent?.Invoke();
}
