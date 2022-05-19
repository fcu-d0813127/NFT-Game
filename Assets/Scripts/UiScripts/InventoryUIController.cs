using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryUIController : MonoBehaviour {
  public GameObject BackpackParent;
  public DynamicInventoryDisplay EquipmentBackpackPanel;
  public DynamicInventoryDisplay DropBackpackPanel;
  public Button CloseButton;

  private void OnEnable() {
    InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
  }

  private void OnDisable() {
    InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
  }

  private void Awake() {
    CloseButton?.onClick.AddListener(CloseBackpackUI);
  }

  private void Update() {
    if (BackpackParent.activeInHierarchy &&
        Keyboard.current.escapeKey.wasPressedThisFrame) {
      CloseBackpackUI();
    }
  }

  private void CloseBackpackUI() {
    BackpackParent.SetActive(false);
  }

  private void OpenBackpackUI() {
    BackpackParent.SetActive(true);
  }

  private void DisplayInventory(InventorySystem invToDisplay, bool isDrop) {
    OpenBackpackUI();
    if (isDrop) {
      DropBackpackPanel.RefreshDyncmicInventory(invToDisplay);
    } else {
      EquipmentBackpackPanel.RefreshDyncmicInventory(invToDisplay);
    }
  }
}
