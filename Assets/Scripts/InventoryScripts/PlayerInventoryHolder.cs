using System.Collections.Generic;
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
  [SerializeField] private GameObject _attribute;
  [SerializeField] private GameObject _hotBar;
  [SerializeField] private GameObject _hotBarName;
  [SerializeField] private GameObject _detail;
  [SerializeField] private DynamicInventoryDisplay _equipmentBackpackAttribute;
  [SerializeField] private DynamicInventoryDisplay _materialBackpackAttribute;

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
    _equipmentBackpackInventorySystem = new InventorySystem(_backpackInventorySize);
    _materialBackpackInventorySystem = new InventorySystem(_backpackInventorySize);
    OnDynamicInventoryDisplayRequested?.Invoke(_equipmentBackpackInventorySystem, false);
    OnDynamicInventoryDisplayRequested?.Invoke(_materialBackpackInventorySystem, true);
    _backpackParent.SetActive(false);
    _attribute.SetActive(false);
    _hotBar.SetActive(false);
    _hotBarName.SetActive(false);
    _equipment?.onClick.AddListener(ChangeBackpackToEquipment);
    _material?.onClick.AddListener(ChangeBackpackToMaterial);
    AddItem();
  }

  private void Update() {
    if (Keyboard.current.bKey.wasPressedThisFrame &&
        PopUpWindowController.IsPlayerStatusOpen == false) {
      PopUpWindowController.IsBackpackOpen = true;
      BackpackAttribute backpackAbility = _attribute.GetComponent<BackpackAttribute>();
      backpackAbility.LoadAttribute();
      ChangeBackpackToEquipment();
      _detail.SetActive(false);
    }
  }

  private void ChangeBackpackToMaterial() {
    if (_mouseInventoryItem.AssignedInventorySlot.ItemData != null) {
      return;
    }
    _equipmentBackpackAttribute.gameObject.SetActive(false);
    _materialBackpackAttribute.gameObject.SetActive(true);
    OnDynamicInventoryDisplayRequested?.Invoke(_materialBackpackInventorySystem, true);
  }

  private void ChangeBackpackToEquipment() {
    if (_mouseInventoryItem.AssignedInventorySlot.ItemData != null) {
      return;
    }
    _equipmentBackpackAttribute.gameObject.SetActive(true);
    _materialBackpackAttribute.gameObject.SetActive(false);
    OnDynamicInventoryDisplayRequested?.Invoke(_equipmentBackpackInventorySystem, false);
  }

  private void AddItem() {
    Attribute attribute = new Attribute {
      Atk = 1,
      Matk = 1,
      Def = 1,
      Mdef = 1,
      Cri = 0.01f,
      CriDmgRatio = 0
    };
    EquipmentItemData SimpleSword = new EquipmentItemData {
      Id = 0,
      DisplayName = "Simple Sword",
      MaxStackSize = 5,
      Icon = Resources.Load<Sprite>("Equipments/Sprites/SimpleSword"),
      Rarity = 0,
      Part = 0,
      Level = 0,
      Attribute = attribute,
      Skills = new int[3]
    };
    EquipmentItemData helmet = new EquipmentItemData {
      Id = 1,
      DisplayName = "Helmet",
      MaxStackSize = 5,
      Icon = Resources.Load<Sprite>("Equipments/Sprites/Helmet"),
      Rarity = 0,
      Part = 3,
      Level = 0,
      Attribute = attribute,
      Skills = new int[3]
    };
    EquipmentItemData breastplate = new EquipmentItemData {
      Id = 2,
      DisplayName = "Breastplate",
      MaxStackSize = 5,
      Icon = Resources.Load<Sprite>("Equipments/Sprites/Breastplate"),
      Rarity = 0,
      Part = 1,
      Level = 0,
      Attribute = attribute,
      Skills = new int[3]
    };
    EquipmentItemData pants = new EquipmentItemData {
      Id = 3,
      DisplayName = "Pants",
      MaxStackSize = 5,
      Icon = Resources.Load<Sprite>("Equipments/Sprites/Pants"),
      Rarity = 0,
      Part = 2,
      Level = 0,
      Attribute = attribute,
      Skills = new int[3]
    };
    EquipmentItemData shoes = new EquipmentItemData {
      Id = 4,
      DisplayName = "Shoes",
      MaxStackSize = 5,
      Icon = Resources.Load<Sprite>("Equipments/Sprites/Shoes"),
      Rarity = 0,
      Part = 4,
      Level = 0,
      Attribute = attribute,
      Skills = new int[3]
    };
    EquipmentItems.Add(SimpleSword);
    EquipmentItems.Add(helmet);
    EquipmentItems.Add(breastplate);
    EquipmentItems.Add(pants);
    EquipmentItems.Add(shoes);
    foreach (var i in EquipmentItems.Equipments) {
      _equipmentBackpackInventorySystem.AddToInventory(i, 1);
    }
    // BackpackAttribute backpackAbility = _attribute.GetComponent<BackpackAttribute>();
    // backpackAbility.LoadAttribute();
    // backpackAbility.UpdateAttribute(t1.Attribute, true);
  }
}
