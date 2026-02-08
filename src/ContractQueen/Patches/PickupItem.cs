using HarmonyLib;
using YAPYAP;

namespace ContractQueen.Patches;

internal static class PawnInventoryPatches
{
  [HarmonyPatch(typeof(PawnInventory), nameof(PawnInventory.ServerTryPickup)), HarmonyPostfix]
  internal static void ServerTryPickupPatch(PawnInventory __instance, NetworkPuppetProp prop, int preferredSlotIndex, ref bool __result)
  {
    var leftProp = __instance.propInteractions.CurrentLeftHandNetworkProp;

    if (leftProp == null)
      return;

    if (leftProp.netId == prop.netId)
      ContractQueenPlugin.Log.LogMessage($"Somebody just picked up {leftProp.DisplayName}({leftProp.GetInstanceID()}) into their left hand");
  }

  [HarmonyPatch(typeof(PawnInventory), nameof(PawnInventory.ServerAddItemToSlot)), HarmonyPostfix]
  internal static void ServerSetInInventoryPatch(PawnInventory __instance, int slotIndex, InventoryItem item, ref bool __result)
  {
    //only care if the item was actually added to the inventory
    if (!__result)
      return;

    var prop = item.PropInstance;
    ContractQueenPlugin.Log.LogInfo($"Somebody just picked up {prop.DisplayName}({prop.GetInstanceID()})");
  }

  [HarmonyPatch(typeof(PawnInventory), nameof(PawnInventory.Awake)), HarmonyPostfix]
  internal static void AwakePatch(PawnInventory __instance)
  {
    var items = 0;

    foreach (var item in __instance.inventoryItems)
      if (!item.IsEmpty)
        items++;

    ContractQueenPlugin.Log.LogInfo($"Someone just spawned in with {items} items in their inventory");
  }
}
