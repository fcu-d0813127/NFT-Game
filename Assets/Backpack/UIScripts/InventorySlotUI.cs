using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
  public InventorySlot AssignedInventorySlot => _assignedInventorySlot;
  public InventoryDisplay ParentDisplay {get; private set;}
  private DetailDisplay _detailDisplay;
  private GameObject _detail;
  private Button _button;
  [SerializeField] private Image _itemSprite;
  [SerializeField] private TextMeshProUGUI _itemCount;
  [SerializeField] private InventorySlot _assignedInventorySlot;

  private void Awake() {
    ClearSlot();
    _detail = FindInActiveObjectByName("Detail");
    _detailDisplay = _detail?.GetComponent<DetailDisplay>();
    _button = GetComponent<Button>();
    _button?.onClick.AddListener(OnUISlotClick);
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

  public void OnPointerEnter(PointerEventData eventData) {
    if (_assignedInventorySlot.ItemData == null) {
      return;
    }
    _detailDisplay.OnHover(_assignedInventorySlot.ItemData);
    _detail?.SetActive(true);
  }

  public void OnPointerExit(PointerEventData eventData) {
    _detail?.SetActive(false);
  }

  private void Update() {
    if (_assignedInventorySlot.ItemData != null) {
      Vector3 mousePosition = Mouse.current.position.ReadValue();
      if (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).y < 4) {
        mousePosition += new Vector3(0, 600, 0);
      }
      _detail.transform.position = mousePosition;
    }
  }

  private GameObject FindInActiveObjectByName(string name) {
    Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
    for (int i = 0; i < objs.Length; i++) {
      if (objs[i].hideFlags == HideFlags.None) {
        if (objs[i].name == name)  {
          return objs[i].gameObject;
        }
      }
    }
    return null;
  }
}
