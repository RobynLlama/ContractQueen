#if DEBUG

using System;

namespace ContractQueen.Events;

public static partial class EventManager
{
  public static event Action? TestEvent;

  public static void FireTestEvent() => TestEvent?.Invoke();
}

#endif
