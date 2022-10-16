using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DynamicInventoryDisplay : InventoryDisplay {
  [SerializeField] protected InventorySlotUI slotPrefab;

  public void RefreshDyncmicInventory(InventorySystem invToDisplay) {
    ClearSlots();
    _inventorySystem = invToDisplay;
    if (_inventorySystem != null) {
      _inventorySystem.OnInventorySlotChanged += UpdateSlot;
    }
    AssignSlot(_inventorySystem);
  }

  public override void AssignSlot(InventorySystem invToDisplay) {
    ClearSlots();
    _slotDictionary = new Dictionary<InventorySlotUI, InventorySlot>();
    if (invToDisplay == null) {
      return;
    }
    for (var i = 0; i < invToDisplay.InventorySize; i++) {
      var uiSlot = Instantiate(slotPrefab, transform);
      _slotDictionary.Add(uiSlot, invToDisplay.InventorySlots[i]);
      uiSlot.Init(invToDisplay.InventorySlots[i]);
      uiSlot.UpdateUISlot();
    }
  }

  private void ClearSlots() {
    foreach (var item in transform.Cast<Transform>()) {
      Destroy(item.gameObject);
    }
    if (_slotDictionary != null) {
      _slotDictionary.Clear();
    }
  }

  private void OnDisable() {
    if (_inventorySystem != null) {
      _inventorySystem.OnInventorySlotChanged -= UpdateSlot;
    }
  }
}
