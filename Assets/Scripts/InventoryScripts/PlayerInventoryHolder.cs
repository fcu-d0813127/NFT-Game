using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder {
  public InventorySystem EquipmentBackpackInventorySystem => _equipmentBackpackInventorySystem;
  public InventorySystem MaterialBackpackInventorySystem => _materialBackpackInventorySystem;
  [SerializeField] protected int _backpackInventorySize;
  [SerializeField] protected InventorySystem _equipmentBackpackInventorySystem;
  [SerializeField] protected InventorySystem _materialBackpackInventorySystem;
  [SerializeField] private MouseItemData _mouseInventoryItem;
  [SerializeField] private Button _equipment;
  [SerializeField] private Button _material;
  [SerializeField] private GameObject _backpackParent;
  [SerializeField] private GameObject _detail;
  [SerializeField] private DynamicInventoryDisplay _equipmentBackpackPanel;
  [SerializeField] private DynamicInventoryDisplay _materialBackpackPanel;

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
    EquipmentItemData SimpleSword = new EquipmentItemData {
      Id = 0,
      DisplayName = "Simple Sword",
      MaxStackSize = 5,
      Icon = Resources.Load<Sprite>("Equipments/Sprites/SimpleSword"),
      Rarity = "common",
      AbilityNum = 0,
      SkillNum = 0,
      AbilityRange = 0,
      Ability = ability
    };
    EquipmentItems.Add(SimpleSword);
    var t = EquipmentItems.Equipments[0];
    var t1 = EquipmentItems.Equipments[0];
    _equipmentBackpackInventorySystem = new InventorySystem(_backpackInventorySize);
    _materialBackpackInventorySystem = new InventorySystem(_backpackInventorySize);
    OnDynamicInventoryDisplayRequested?.Invoke(_equipmentBackpackInventorySystem, false);
    OnDynamicInventoryDisplayRequested?.Invoke(_materialBackpackInventorySystem, true);
    _equipmentBackpackInventorySystem.AddToInventory(t, 5);
    _equipmentBackpackInventorySystem.AddToInventory(t1, 1);
    _backpackParent.SetActive(false);
    _equipment?.onClick.AddListener(ChangeBackpackToEquipment);
    _material?.onClick.AddListener(ChangeBackpackToDrop);
  }

  private void Update() {
    if (Keyboard.current.bKey.wasPressedThisFrame &&
        PopUpWindowController.IsPlayerStatusOpen == false) {
      PopUpWindowController.IsBackpackOpen = true;
      ChangeBackpackToEquipment();
      _detail.SetActive(false);
    }
  }

  private void ChangeBackpackToDrop() {
    if (_mouseInventoryItem.AssignedInventorySlot.ItemData != null) {
      return;
    }
    _equipmentBackpackPanel.gameObject.SetActive(false);
    _materialBackpackPanel.gameObject.SetActive(true);
    OnDynamicInventoryDisplayRequested?.Invoke(_materialBackpackInventorySystem, true);
  }

  private void ChangeBackpackToEquipment() {
    if (_mouseInventoryItem.AssignedInventorySlot.ItemData != null) {
      return;
    }
    _equipmentBackpackPanel.gameObject.SetActive(true);
    _materialBackpackPanel.gameObject.SetActive(false);
    OnDynamicInventoryDisplayRequested?.Invoke(_equipmentBackpackInventorySystem, false);
  }
}
