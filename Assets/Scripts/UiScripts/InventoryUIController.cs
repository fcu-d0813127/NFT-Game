using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryUIController : MonoBehaviour {
  public DynamicInventoryDisplay PlayerBackpackPanel;
  public Button CloseButton;

  private void OnEnable() {
    InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
  }

  private void OnDisable() {
    InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
  }

  private void Awake() {
    PlayerBackpackPanel.gameObject.SetActive(false);
    CloseButton?.onClick.AddListener(CloseBackpackUI);
  }

  private void Update() {
    if (PlayerBackpackPanel.gameObject.activeInHierarchy &&
        Keyboard.current.escapeKey.wasPressedThisFrame) {
      CloseBackpackUI();
    }
  }

  private void CloseBackpackUI() {
    PlayerBackpackPanel.gameObject.SetActive(false);
  }

  private void OpenBackpackUI() {
    PlayerBackpackPanel.gameObject.SetActive(true);
  }

  private void DisplayInventory(InventorySystem invToDisplay) {
    OpenBackpackUI();
    PlayerBackpackPanel.RefreshDyncmicInventory(invToDisplay);
  }
}
