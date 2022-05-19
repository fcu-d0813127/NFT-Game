using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder {
  public Button Equipment;
  public Button Material;
  public GameObject BackpackParent;
  public GameObject Detail;
  public InventorySystem EquipmentBackpackInventorySystem => _equipmentBackpackInventorySystem;
  public InventorySystem MaterialBackpackInventorySystem => _materialBackpackInventorySystem;
  public DynamicInventoryDisplay EquipmentBackpackPanel;
  public DynamicInventoryDisplay MaterialBackpackPanel;
  [SerializeField] protected int _backpackInventorySize;
  [SerializeField] protected InventorySystem _equipmentBackpackInventorySystem;
  [SerializeField] protected InventorySystem _materialBackpackInventorySystem;
  [SerializeField] private MouseItemData _mouseInventoryItem;

  public bool AddToInventory(InventoryItemData data, int amount) {
    if (_hotBarInventorySystem.AddToInventory(data, amount)) {
      return true;
    } else if (_equipmentBackpackInventorySystem.AddToInventory(data, amount)) {
      return true;
    } else if (_materialBackpackInventorySystem.AddToInventory(data, amount)) {
      return true;
    }
    return false;
  }

  protected override void Awake() {
    base.Awake();
    int[] ability = {1, 2, 3, 4, 5, 6};
    EquipmentItemData SimpleSword = new EquipmentItemData(0, "Simple Sword", 5, Resources.Load<Sprite>("Equipments/Sprites/SimpleSword"), "common", 0, 0, 0, ability);
    EquipmentItems.Add(SimpleSword);
    var t = EquipmentItems.Equipments[0];
    var t1 = EquipmentItems.Equipments[0];
    _equipmentBackpackInventorySystem = new InventorySystem(_backpackInventorySize);
    _materialBackpackInventorySystem = new InventorySystem(_backpackInventorySize);
    OnDynamicInventoryDisplayRequested?.Invoke(_equipmentBackpackInventorySystem, false);
    OnDynamicInventoryDisplayRequested?.Invoke(_materialBackpackInventorySystem, true);
    _equipmentBackpackInventorySystem.AddToInventory(t, 5);
    _equipmentBackpackInventorySystem.AddToInventory(t1, 1);
    BackpackParent.SetActive(false);
    Equipment?.onClick.AddListener(ChangeBackpackToEquipment);
    Material?.onClick.AddListener(ChangeBackpackToDrop);
  }

  private void Update() {
    if (Keyboard.current.bKey.wasPressedThisFrame) {
      ChangeBackpackToEquipment();
      Detail.SetActive(false);
    }
  }

  private void ChangeBackpackToDrop() {
    if (_mouseInventoryItem.AssignedInventorySlot.ItemData != null) {
      return;
    }
    EquipmentBackpackPanel.gameObject.SetActive(false);
    MaterialBackpackPanel.gameObject.SetActive(true);
      OnDynamicInventoryDisplayRequested?.Invoke(_materialBackpackInventorySystem, true);
  }

  private void ChangeBackpackToEquipment() {
    if (_mouseInventoryItem.AssignedInventorySlot.ItemData != null) {
      return;
    }
    EquipmentBackpackPanel.gameObject.SetActive(true);
    MaterialBackpackPanel.gameObject.SetActive(false);
    OnDynamicInventoryDisplayRequested?.Invoke(_equipmentBackpackInventorySystem, false);
  }
}
