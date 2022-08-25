using UnityEngine;
using UnityEngine.UI;

public class DestroyButtonController : MonoBehaviour {
  [SerializeField] private MouseItemData _mouseItemData;
  private Button _myselfButton;

  private void Awake() {
    _myselfButton = GetComponent<Button>();
    _myselfButton.onClick.AddListener(DestroyEquipment);
  }

  private void DestroyEquipment() {
    if (_mouseItemData.AssignedInventorySlot.ItemData != null) {
      int tokenId = _mouseItemData.AssignedInventorySlot.ItemData.Id;
      Debug.Log(tokenId);
      // 消除裝備
      _mouseItemData.ClearSlot();
    }
  }
}
