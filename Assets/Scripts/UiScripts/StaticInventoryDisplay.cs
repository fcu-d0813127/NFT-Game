using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay {
  [SerializeField] private InventoryHolder _inventoryHolder;
  [SerializeField] private InventorySlotUI[] _slots;

  public override void AssignSlot(InventorySystem invToDisplay) {
    _slotDictionary = new Dictionary<InventorySlotUI, InventorySlot>();
    if (_slots.Length != _inventorySystem.InventorySize) {
      Debug.Log($"Inventory slots out of sync on {this.gameObject}.");
    }
    for (int i = 0; i < _inventorySystem.InventorySize; i++) {
      _slotDictionary.Add(_slots[i], _inventorySystem.InventorySlots[i]);
      _slots[i].Init(_inventorySystem.InventorySlots[i]);
    }
  }

  protected override void Start() {
    if (_inventoryHolder != null) {
      _inventorySystem = _inventoryHolder.HotBarInventorySystem;
      _inventorySystem.OnInventorySlotChanged += UpdateSlot;
    } else {
      Debug.Log($"No inventory assigned to {this.gameObject}.");
    }
    AssignSlot(_inventorySystem);
  }
}
