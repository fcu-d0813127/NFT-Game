using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder {
  public static PlayerInventoryHolder Instance {get; private set;}
  public InventorySystem EquipmentBackpackInventorySystem => _equipmentBackpackInventorySystem;
  [SerializeField] protected int _backpackInventorySize;
  [SerializeField] protected InventorySystem _equipmentBackpackInventorySystem;
  [SerializeField] private MouseItemData _mouseInventoryItem;
  [SerializeField] private Button _equipment;
  [SerializeField] private Button _material;
  [SerializeField] private GameObject _backpackParent;
  [SerializeField] private GameObject _attribute;
  [SerializeField] private GameObject _hotBar;
  [SerializeField] private GameObject _hotBarName;
  [SerializeField] private GameObject _detail;
  [SerializeField] private GameObject _materialBackpackAttribute;
  [SerializeField] private GameObject _destroyField;
  [SerializeField] private GameObject _saveEquip;
  [SerializeField] private GameObject _refreshButton;
  [SerializeField] private DynamicInventoryDisplay _equipmentBackpackAttribute;
  [SerializeField] private BackpackAttribute _backpackAttribute;
  private EquipmentItemData[] _equips;

  public bool AddToInventory(InventoryItemData data, int amount) {
    if (_hotBarInventorySystem.AddToInventory(data, amount)) {
      return true;
    } else if (_equipmentBackpackInventorySystem.AddToInventory(data, amount)) {
      return true;
    }
    return false;
  }

  public void ResetEquipmentBackpack() {
    _equipmentBackpackInventorySystem = new InventorySystem(_backpackInventorySize);
    OnDynamicInventoryDisplayRequested?.Invoke(_equipmentBackpackInventorySystem, false);
    AddItem();
  }

  protected override void Awake() {
    base.Awake();
    Instance = this;
    _hotBar.GetComponent<StaticInventoryDisplay>().Init();
    _equipmentBackpackInventorySystem = new InventorySystem(_backpackInventorySize);
    OnDynamicInventoryDisplayRequested?.Invoke(_equipmentBackpackInventorySystem, false);
    _backpackParent.SetActive(false);
    _attribute.SetActive(false);
    _hotBar.SetActive(false);
    _hotBarName.SetActive(false);
    _destroyField.SetActive(false);
    _saveEquip.SetActive(false);
    _refreshButton.SetActive(false);
    _equipment?.onClick.AddListener(ChangeBackpackToEquipment);
    _material?.onClick.AddListener(ChangeBackpackToMaterial);
    AddItem();
  }

  private void Update() {
    if (Keyboard.current.bKey.wasPressedThisFrame &&
        PopUpWindowController.IsPlayerStatusOpen == false) {
      PopUpWindowController.IsBackpackOpen = true;
      BackpackAttribute backpackAbility = _attribute.GetComponent<BackpackAttribute>();
      ChangeBackpackToEquipment();
      UpdateEquipBar();
      _detail.SetActive(false);
    }
  }

  private void ChangeBackpackToEquipment() {
    if (_mouseInventoryItem.AssignedInventorySlot.ItemData != null) {
      return;
    }
    _equipmentBackpackAttribute.gameObject.SetActive(true);
    _materialBackpackAttribute.gameObject.SetActive(false);
    OnDynamicInventoryDisplayRequested?.Invoke(_equipmentBackpackInventorySystem, false);
  }

  private void ChangeBackpackToMaterial() {
    if (_mouseInventoryItem.AssignedInventorySlot.ItemData != null) {
      return;
    }
    _equipmentBackpackAttribute.gameObject.SetActive(false);
    _materialBackpackAttribute.SetActive(true);
  }

  private void AddItem() {
    foreach (var i in PlayerInfo.PlayerEquipment.equipments) {
      Attribute attribute = new Attribute {
        Atk = i.equipmentStatus.attribute[0],
        Matk = i.equipmentStatus.attribute[1],
        Def = i.equipmentStatus.attribute[2],
        Mdef = i.equipmentStatus.attribute[3],
        Cri = (float)i.equipmentStatus.attribute[4] / 10000,
        CriDmgRatio = (float)i.equipmentStatus.attribute[5] / 100
      };
      EquipmentItemData equipment = new EquipmentItemData {
        Id = i.tokenId,
        DisplayName = $"Id: {i.tokenId}",
        MaxStackSize = 1,
        Rarity = i.equipmentStatus.rarity,
        Part = i.equipmentStatus.part,
        Level = i.equipmentStatus.level,
        Attribute = attribute,
        Skills = i.equipmentStatus.skills
      };
      EquipmentItems.Add(equipment);
    }
    _equips = new EquipmentItemData[5];
    foreach (var i in EquipmentItems.Equipments) {
      bool isEquip = false;
      for (int j = 0; j < 5; j++) {
        if (i.Id == PlayerInfo.EquipEquipments[j]) {
          isEquip = true;
          _equips[j] = i;
        }
      }
      if (isEquip == false) {
        _equipmentBackpackInventorySystem.AddToInventory(i, 1);
      }
    }
  }

  private void UpdateEquipBar() {
    var equipPanel = NormalUseLibrary.FindInActiveObjectByName("PlayerHotBar");
    var equipEquipment = equipPanel.GetComponentsInChildren<InventorySlotUI>();
    for (int i = 0; i < 5; i++) {
      equipEquipment[i].ClearSlot();
      if (_equips[i] != null) {
        equipEquipment[i].AssignedInventorySlot.UpdateInventorySlot(_equips[i], 1);
        equipEquipment[i].UpdateUISlot();
        _backpackAttribute.UpdateAttribute(_equips[i].Attribute, true);
        _backpackAttribute.Save();
      }
    }
  }
}
