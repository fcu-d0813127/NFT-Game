using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem {
  public UnityAction<InventorySlot> OnInventorySlotChanged;
  public List<InventorySlot> InventorySlots => _inventorySlots;
  public int InventorySize => _inventorySlots.Count;
  [SerializeField] private List<InventorySlot> _inventorySlots;

  public InventorySystem(int size) {
    _inventorySlots = new List<InventorySlot>(size);
    for (int i = 0; i < size; i++) {
      _inventorySlots.Add(new InventorySlot());
    }
  }

  public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd) {
    if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) {
      // Check whether item exists in inventory.
      foreach (var slot in invSlot) {
        if (slot.RoomLeftInStack(amountToAdd)) {
          slot.AddToStack(amountToAdd);
          OnInventorySlotChanged?.Invoke(slot);
          return true;
        }
      }
    }
    if (HasFreeSlot(out InventorySlot freeSlot)) {
      // Get the first available slot.
      freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
      OnInventorySlotChanged?.Invoke(freeSlot);
      return true;
    }
    return false;
  }

  public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot) {
    invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
    return invSlot == null ? false : true;
  }

  public bool HasFreeSlot(out InventorySlot freeSlot) {
    freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
    return freeSlot == null ? false : true;
  }
}
