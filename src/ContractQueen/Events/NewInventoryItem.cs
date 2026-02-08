using System;
using System.Collections.Generic;
using YAPYAP;

namespace ContractQueen.Events;

public static partial class EventManager
{

  internal static HashSet<int> SeenItemIDs = [];
  internal static void ItemPickedUp(NetworkPuppetProp item)
  {
    var id = item.GetInstanceID();
    if (SeenItemIDs.Contains(id))
      return;

    SeenItemIDs.Add(id);
    OnNewItemPickup?.Invoke(item);
  }

  /// <summary>
  /// This event will be fired when one of the following happens:<br />
  /// An item is newly picked up into an inventory slot<br />
  /// An item is newly shifted into an inventory slot from the left hand<br />
  /// An item is newly picked up into the left hand from the ground<br />
  /// </summary>
  public static event Action<NetworkPuppetProp>? OnNewItemPickup;
}
