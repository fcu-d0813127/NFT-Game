using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

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
    Clear();
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
        PopUpWindowController.IsPlayerStatusOpen == false &&
        PopUpWindowController.IsBackpackOpen == false) {
      PopUpWindowController.IsBackpackOpen = true;
      BackpackAttribute backpackAbility = _attribute.GetComponent<BackpackAttribute>();
      ChangeBackpackToEquipment();
      ResetEquipmentBackpack();
      _detail.SetActive(false);
    }
  }

  private void ChangeBackpackToEquipment() {
    if (_mouseInventoryItem.AssignedInventorySlot.ItemData != null) {
      return;
    }
    _equipmentBackpackAttribute.gameObject.SetActive(true);
    _refreshButton.SetActive(true);
    _materialBackpackAttribute.gameObject.SetActive(false);
  }

  private void ChangeBackpackToMaterial() {
    if (_mouseInventoryItem.AssignedInventorySlot.ItemData != null) {
      return;
    }
    _equipmentBackpackAttribute.gameObject.SetActive(false);
    _refreshButton.SetActive(false);
    _materialBackpackAttribute.SetActive(true);
  }

  private void AddItem() {
    foreach (var i in PlayerInfo.PlayerEquipment) {
      Attribute attribute = new Attribute {
        Atk = int.Parse(i.Value.attributes[1].value),
        Matk = int.Parse(i.Value.attributes[3].value),
        Def = int.Parse(i.Value.attributes[2].value),
        Mdef = int.Parse(i.Value.attributes[4].value),
        Cri = (float)int.Parse(i.Value.attributes[5].value) / 10000,
        CriDmgRatio = (float)int.Parse(i.Value.attributes[6].value) / 100
      };
      int part = -1;
      int nameLength = i.Value.name.Length;
      string partName = i.Value.name.Substring(nameLength - 2);
      if (partName == "武器") {
        part = 0;
      } else if (partName == "頭盔") {
        part = 3;
      } else if (partName == "胸甲") {
        part = 1;
      } else if (partName == "護腿") {
        part = 2;
      } else if (partName == "靴子") {
        part = 4;
      }
      string[] imageSplitArray = i.Value.image.Split('/');
      int imageSplitArrayLength = imageSplitArray.Length;
      string imageHash = imageSplitArray[imageSplitArrayLength - 1];
      string uri = "https://ipfs.io/ipfs/" + imageHash;
      EquipmentItemData equipment = new EquipmentItemData {
        Id = i.Key,
        DisplayName = i.Value.name,
        MaxStackSize = 1,
        Rarity = i.Value.attributes[0].value,
        Part = part,
        Attribute = attribute,
        Icon = null
      };
      EquipmentItemData exsistEquipment = EquipmentItems.Find(equipment);
      if (exsistEquipment == null) {
        EquipmentItems.Add(equipment);
        StartCoroutine(GetTexture(uri, equipment));
      } else if (exsistEquipment.Icon == null) {
        StartCoroutine(GetTexture(uri, equipment));
      }
    }
    _equips = new EquipmentItemData[5];
    foreach (var i in EquipmentItems.Equipments) {
      bool isExsist = false;
      foreach (var j in PlayerInfo.PlayerEquipment) {
        if (i.Id == j.Key) {
          isExsist = true;
          break;
        }
      }
      if (isExsist == false) {
        continue;
      }
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
    UpdateEquipBar();
  }

  private void UpdateEquipBar() {
    _backpackAttribute.ResetTmpAttribute();
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

  private IEnumerator GetTexture(string uri, EquipmentItemData equipment) {
    UnityWebRequest www = UnityWebRequestTexture.GetTexture(uri);
    yield return www.SendWebRequest();

    if (www.result != UnityWebRequest.Result.Success) {
      Debug.Log(www.error);
      StartCoroutine(GetTexture(uri, equipment));
    } else {
      Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
      equipment.Icon = Sprite.Create(myTexture,
                                     new Rect(0.0f, 0.0f, myTexture.width, myTexture.height),
                                     new Vector2(0.5f, 0.5f));
    }
  }
}
