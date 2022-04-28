using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour {
  public InventorySlot AssignedInventorySlot => _assignedInventorySlot;
  public InventoryDisplay ParentDisplay {get; private set;}
  private Button button;
  [SerializeField] private Image _itemSprite;
  [SerializeField] private TextMeshProUGUI _itemCount;
  [SerializeField] private InventorySlot _assignedInventorySlot;

  private void Awake() {
    ClearSlot();
    button = GetComponent<Button>();
    button?.onClick.AddListener(OnUISlotClick);
    ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
  }

  public void Init(InventorySlot slot) {
    _assignedInventorySlot = slot;
    UpdateUISlot(slot);
  }

  public void UpdateUISlot(InventorySlot slot) {
    if (slot.ItemData != null) {
      _itemSprite.sprite = slot.ItemData.Icon;
      _itemSprite.color = Color.white;
      if (slot.StackSize > 1) {
        _itemCount.text = slot.StackSize.ToString();
      } else {
        _itemCount.text = "";
      }
    } else {
      ClearSlot();
    }
  }

  public void UpdateUISlot() {
    if (_assignedInventorySlot != null) {
      UpdateUISlot(_assignedInventorySlot);
    }
  }

  public void ClearSlot() {
    _assignedInventorySlot?.ClearSlot();
    _itemSprite.sprite = null;
    _itemSprite.color = Color.clear;
    _itemCount.text = "";
  }

  public void OnUISlotClick() {
    // Access display class function.
    ParentDisplay?.SlotClicked(this);
  }
}
