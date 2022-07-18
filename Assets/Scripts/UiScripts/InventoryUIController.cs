using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUIController : MonoBehaviour {
  [SerializeField] private GameObject _backpackParent;
  [SerializeField] private GameObject _attribute;
  [SerializeField] private GameObject _hotBar;
  [SerializeField] private GameObject _hotBarName;
  [SerializeField] private DynamicInventoryDisplay _equipmentBackpackPanel;
  [SerializeField] private DynamicInventoryDisplay _materialBackpackPanel;

  private void OnEnable() {
    InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
  }

  private void OnDisable() {
    InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
  }

  private void Update() {
    if (_backpackParent.activeInHierarchy &&
        Keyboard.current.qKey.wasPressedThisFrame) {
      PopUpWindowController.IsBackpackOpen = false;
      CloseBackpackUI();
    }
  }

  private void CloseBackpackUI() {
    _backpackParent.SetActive(false);
    _attribute.SetActive(false);
    _hotBar.SetActive(false);
    _hotBarName.SetActive(false);
  }

  private void OpenBackpackUI() {
    _backpackParent.SetActive(true);
    _attribute.SetActive(true);
    _hotBar.SetActive(true);
    _hotBarName.SetActive(true);
  }

  private void DisplayInventory(InventorySystem invToDisplay, bool isDrop) {
    OpenBackpackUI();
    if (isDrop) {
      _materialBackpackPanel.RefreshDyncmicInventory(invToDisplay);
    } else {
      _equipmentBackpackPanel.RefreshDyncmicInventory(invToDisplay);
    }
  }
}
