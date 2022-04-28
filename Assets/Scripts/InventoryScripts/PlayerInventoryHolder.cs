using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder {
  public Button OpenButton;
  public InventorySystem BackpackInventorySystem => _backpackInventorySystem;
  [SerializeField] protected int _backpackInventorySize;
  [SerializeField] protected InventorySystem _backpackInventorySystem;

  public bool AddToInventory(InventoryItemData data, int amount) {
    if (_hotBarInventorySystem.AddToInventory(data, amount)) {
      return true;
    } else if (_backpackInventorySystem.AddToInventory(data, amount)) {
      return true;
    }
    return false;
  }

  protected override void Awake() {
    base.Awake();
    _backpackInventorySystem = new InventorySystem(_backpackInventorySize);
    OpenButton?.onClick.AddListener(() =>
        OnDynamicInventoryDisplayRequested?.Invoke(_backpackInventorySystem));
  }

  private void Update() {
    if (Keyboard.current.bKey.wasPressedThisFrame) {
      OnDynamicInventoryDisplayRequested?.Invoke(_backpackInventorySystem);
    }
  }
}
