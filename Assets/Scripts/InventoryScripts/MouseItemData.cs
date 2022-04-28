using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class MouseItemData : MonoBehaviour {
  public Image ItemSprite;
  public TextMeshProUGUI ItemCount;
  public InventorySlot AssignedInventorySlot;

  public void UpdateMouseSlot(InventorySlot invSlot) {
    AssignedInventorySlot.AssignItem(invSlot);
    ItemSprite.sprite = invSlot.ItemData.Icon;
    ItemCount.text = invSlot.StackSize.ToString();
    ItemSprite.color = Color.white;
  }

  public void ClearSlot() {
    AssignedInventorySlot.ClearSlot();
    ItemCount.text = "";
    ItemSprite.color = Color.clear;
    ItemSprite.sprite = null;
  }

  private void Awake() {
    ItemSprite.color = Color.clear;
    ItemCount.text = "";
  }

  private void Update() {
    if (AssignedInventorySlot.ItemData != null) {
      transform.position = Mouse.current.position.ReadValue();
    }
  }
}
