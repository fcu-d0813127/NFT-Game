using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SaveEquip : MonoBehaviour {
  [DllImport("__Internal")]
  private static extern void Equip(
      int helmet,
      int chestplate,
      int leggings,
      int boots,
      int weapon);
  private Button _saveButton;

  private void Awake() {
    _saveButton = GetComponent<Button>();
    _saveButton.onClick.AddListener(Save);
  }

  private void Save() {
    var equipPanel = GameObject.FindObjectOfType<StaticInventoryDisplay>();
    var equipEquipment = equipPanel.GetComponentsInChildren<InventorySlotUI>();
    int[] equips = new int[5];
    for (int i = 0; i < 5; i++) {
      if (equipEquipment[i].AssignedInventorySlot.ItemData == null) {
        equips[i] = 0;
      } else {
        equips[i] = equipEquipment[i].AssignedInventorySlot.ItemData.Id;
      }
    }
    #if UNITY_WEBGL && !UNITY_EDITOR
      Equip(equips[1], equips[2], equips[3], equips[4], equips[0]);
    #endif
  }
}
