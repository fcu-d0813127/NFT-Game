using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class DestroyButtonController : MonoBehaviour {
  [DllImport("__Internal")]
  private static extern void DestroyEquipmentSmartContract(int tokenId);
  [SerializeField] private MouseItemData _mouseItemData;
  private Button _myselfButton;

  private void Awake() {
    _myselfButton = GetComponent<Button>();
    _myselfButton.onClick.AddListener(DestroyEquipment);
  }

  private void DestroyEquipment() {
    if (_mouseItemData.AssignedInventorySlot.ItemData != null) {
      int tokenId = _mouseItemData.AssignedInventorySlot.ItemData.Id;
      #if UNITY_WEBGL && !UNITY_EDITOR
        DestroyEquipmentSmartContract(tokenId);
      #endif
      // 消除裝備
      #if UNITY_EDITOR
        ClearMouse();
      #endif
    }
  }

  private void ClearMouse() {
    _mouseItemData.ClearSlot();
  }
}
