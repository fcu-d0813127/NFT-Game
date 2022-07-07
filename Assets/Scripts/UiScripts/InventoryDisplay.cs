using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour {
  public InventorySystem InventorySystem => _inventorySystem;
  public Dictionary<InventorySlotUI, InventorySlot> SlotDictionary => _slotDictionary;
  protected InventorySystem _inventorySystem;
  protected Dictionary<InventorySlotUI, InventorySlot> _slotDictionary;
  private enum EquipStatus {
    Equip, UnEquip, Switch
  }
  private enum EquipPart {
    Weapon, Breastplate, Pants, Helmet, Shoes
  }
  [SerializeField] private MouseItemData _mouseInventoryItem;
  [SerializeField] private BackpackAttribute _backpackAttribute;

  public void SlotClicked(InventorySlotUI clickedUISlot) {
    bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;
    if (clickedUISlot.AssignedInventorySlot.ItemData != null &&
        _mouseInventoryItem.AssignedInventorySlot.ItemData == null) {
      // If player is hoding the shift key? Splite the stack.
      if (isShiftPressed &&
          clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot)) {
        // Split stack.
        _mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
        clickedUISlot.UpdateUISlot();
        return;
      }
      // Clicked slot has an item & mouse doesn't have an item -> pick up that item.
      _mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
      UpdateAbility(clickedUISlot, EquipStatus.UnEquip);
      clickedUISlot.ClearSlot();
      return;
    }
    // Clicked slot doesn't have an item & mouse does have an item
    // ->place the mouse item into the empty slot.
    if (clickedUISlot.AssignedInventorySlot.ItemData == null &&
        _mouseInventoryItem.AssignedInventorySlot.ItemData != null) {
      // Check correct part
      if (clickedUISlot.transform.parent.gameObject.name == "PlayerHotBar" &&
          CheckPart(clickedUISlot) == false) {
        return;
      }
      clickedUISlot.AssignedInventorySlot.AssignItem(_mouseInventoryItem.AssignedInventorySlot);
      clickedUISlot.UpdateUISlot();
      UpdateAbility(clickedUISlot, EquipStatus.Equip);
      _mouseInventoryItem.ClearSlot();
      return;
    }
    // Both slots have an item -> decide what to do...
    //   If different items, then swap the items.
    //   Are both items the same? If so,  combine them.
    //     Is the slot stack size + mouse stack size > the slot max stack size?
    //     If so, take from mouse.
    if (clickedUISlot.AssignedInventorySlot.ItemData != null &&
        _mouseInventoryItem.AssignedInventorySlot.ItemData != null) {
      bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData ==
          _mouseInventoryItem.AssignedInventorySlot.ItemData;
      // Check correct part
      if (clickedUISlot.transform.parent.gameObject.name == "PlayerHotBar" &&
          CheckPart(clickedUISlot) == false) {
        return;
      }
      UpdateAbility(clickedUISlot, EquipStatus.Switch);
      if (!isSameItem) {
        SwapSlots(clickedUISlot);
        return;
      }
      if (isSameItem && clickedUISlot.AssignedInventorySlot.RoomLeftInStack(
          _mouseInventoryItem.AssignedInventorySlot.StackSize)) {
        clickedUISlot.AssignedInventorySlot.AssignItem(_mouseInventoryItem.AssignedInventorySlot);
        clickedUISlot.UpdateUISlot();
        _mouseInventoryItem.ClearSlot();
        return;
      }
      if (isSameItem && !clickedUISlot.AssignedInventorySlot.RoomLeftInStack(
              _mouseInventoryItem.AssignedInventorySlot.StackSize, out int leftInStack)) {
        if (leftInStack < 1) {
          // Stack is full, so swap the items.
          SwapSlots(clickedUISlot);
        } else {
          // Slot is not at max, so take what's need from the mouse inventory.
          int remainingOnMouse = _mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack;
          clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
          clickedUISlot.UpdateUISlot();
          var newItem = new InventorySlot(
            _mouseInventoryItem.AssignedInventorySlot.ItemData,
            remainingOnMouse
          );
          _mouseInventoryItem.ClearSlot();
          _mouseInventoryItem.UpdateMouseSlot(newItem);
        }
        return;
      }
    }
  }

  public abstract void AssignSlot(InventorySystem invToDisplay);

  protected virtual void Start() {
    
  }

  protected virtual void UpdateSlot(InventorySlot updatedSlot) {
    foreach (var slot in SlotDictionary) {
      if (slot.Value == updatedSlot) {
        slot.Key.UpdateUISlot(updatedSlot);
      }
    }
  }

  private void SwapSlots(InventorySlotUI clickedUISlot) {
    var clonedSlot = new InventorySlot(
      _mouseInventoryItem.AssignedInventorySlot.ItemData,
      _mouseInventoryItem.AssignedInventorySlot.StackSize
    );
    _mouseInventoryItem.ClearSlot();
    _mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
    clickedUISlot.ClearSlot();
    clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
    clickedUISlot.UpdateUISlot();
  }

  private void UpdateAbility(InventorySlotUI clickedUISlot, EquipStatus equipStatus) {
    if (clickedUISlot.transform.parent.gameObject.name == "PlayerHotBar") {
      if (equipStatus == EquipStatus.Equip) {
        InventoryItemData equipment = _mouseInventoryItem.AssignedInventorySlot.ItemData;
        Attribute equipmentAttribute = EquipmentItems.Find(equipment).Attribute;
        _backpackAttribute.UpdateAttribute(equipmentAttribute, true);
      } else if (equipStatus == EquipStatus.UnEquip) {
        InventoryItemData equipment = clickedUISlot.AssignedInventorySlot.ItemData;
        Attribute equipmentAttribute = EquipmentItems.Find(equipment).Attribute;
        _backpackAttribute.UpdateAttribute(equipmentAttribute, false);
      } else if (equipStatus == EquipStatus.Switch) {
        InventoryItemData equipment = clickedUISlot.AssignedInventorySlot.ItemData;
        Attribute equipmentAttribute = EquipmentItems.Find(equipment).Attribute;
        _backpackAttribute.UpdateAttribute(equipmentAttribute, false);
        equipment = _mouseInventoryItem.AssignedInventorySlot.ItemData;
        equipmentAttribute = EquipmentItems.Find(equipment).Attribute;
        _backpackAttribute.UpdateAttribute(equipmentAttribute, true);
      }
    }
  }

  private bool CheckPart(InventorySlotUI clickedSlot) {
    InventoryItemData itemData = _mouseInventoryItem.AssignedInventorySlot.ItemData;
    EquipmentItemData equipment = EquipmentItems.Find(itemData);
    string clickedSlotName = clickedSlot.gameObject.name;
    if (equipment.Part == (int)EquipPart.Weapon && clickedSlotName == "Weapon" ||
        equipment.Part == (int)EquipPart.Helmet && clickedSlotName == "Helmet" ||
        equipment.Part == (int)EquipPart.Breastplate && clickedSlotName == "Breastplate" ||
        equipment.Part == (int)EquipPart.Pants && clickedSlotName == "Pants" ||
        equipment.Part == (int)EquipPart.Shoes && clickedSlotName == "Shoes") {
      return true;
    }
    return false;
  }
}
