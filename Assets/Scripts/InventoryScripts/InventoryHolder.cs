using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour {
  public static UnityAction<InventorySystem, bool> OnDynamicInventoryDisplayRequested;
  public InventorySystem HotBarInventorySystem => _hotBarInventorySystem;
  [SerializeField] protected InventorySystem _hotBarInventorySystem;
  [SerializeField] private int _inventorySize;

  protected virtual void Awake() {
    _hotBarInventorySystem = new InventorySystem(_inventorySize);
  }
}
