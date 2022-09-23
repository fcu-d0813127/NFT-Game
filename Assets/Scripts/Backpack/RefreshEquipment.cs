using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class RefreshEquipment : MonoBehaviour {
  [DllImport("__Internal")]
  private static extern void LoadEquipment(string playerAccount, int isInit);
  private Button _refreshEquipmentButton;

  private void Awake() {
    _refreshEquipmentButton = GetComponent<Button>();
    _refreshEquipmentButton.onClick.AddListener(Refresh);
  }

  private void Refresh() {
    #if UNITY_WEBGL && !UNITY_EDITOR
      LoadEquipment(PlayerInfo.AccountAddress, 0);
    #endif
    #if UNITY_EDITOR
      RefreshBackpack();
    #endif
  }

  private void SetEquipment(string equipment) {
    PlayerInfo.PlayerEquipment = PlayerEquipment.CreateEquipment(equipment);
    Debug.Log("Ready Refresh Equipment");
    RefreshBackpack();
  }

  private void RefreshBackpack() {
    EquipmentItems.Clear();
    PlayerInventoryHolder.Instance.ResetEquipmentBackpack();
  }
}
