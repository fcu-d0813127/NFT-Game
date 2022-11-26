using UnityEngine;

[System.Serializable]
public class InventorySlot {
  public InventoryItemData ItemData => _itemData;
  public int StackSize => _stackSize;
  private InventoryItemData _itemData;
  [SerializeField] private int _stackSize;

  public InventorySlot(InventoryItemData source, int amount) {
    _itemData = source;
    _stackSize = amount;
  }

  public InventorySlot() {
    ClearSlot();
  }

  public void ClearSlot() {
    _itemData = null;
    _stackSize = -1;
  }

  public void AssignItem(InventorySlot invSlot) {
    if (_itemData == invSlot.ItemData) {
      AddToStack(invSlot.StackSize);
    } else {
      _itemData = invSlot.ItemData;
      _stackSize = 0;
      AddToStack(invSlot.StackSize);
    }
  }

  public void UpdateInventorySlot(InventoryItemData data, int amount) {
    _itemData = data;
    _stackSize = amount;
  }

  public void AddToStack(int amount) {
    _stackSize += amount;
  }

  public void RemoveFromStack(int amount) {
    _stackSize -= amount;
  }

  public bool RoomLeftInStack(int amountToAdd, out int amountRemaining) {
    amountRemaining = _itemData.MaxStackSize - _stackSize;
    return RoomLeftInStack(amountToAdd);
  }

  public bool RoomLeftInStack(int amountToAdd) {
    return _stackSize + amountToAdd <= _itemData.MaxStackSize;
  }

  public bool SplitStack(out InventorySlot splitStack) {
    if (_stackSize <= 1) {
      splitStack = null;
      return false;
    }
    int halfStack = Mathf.RoundToInt(_stackSize / 2);
    RemoveFromStack(halfStack);
    splitStack = new InventorySlot(_itemData, halfStack);
    return true;
  }
}
