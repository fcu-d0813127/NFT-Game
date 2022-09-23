using UnityEngine;
using UnityEngine.UI;

public class TempButtonController : MonoBehaviour {
  private Button _myselfButton;

  private void Awake() {
    _myselfButton = GetComponent<Button>();
    _myselfButton.onClick.AddListener(Change);
  }

  private void Change() {
    string a = "{\"equipments\":[{\"tokenId\":\"1\",\"equipmentStatus\":{\"rarity\":\"1\",\"part\":\"4\",\"level\":\"1\",\"attribute\":[\"5000\",\"100\",\"1000\",\"100\",\"500\",\"6\"],\"skills\":[\"0\",\"0\",\"0\"]}}]}";
    PlayerInfo.PlayerEquipment = PlayerEquipment.CreateEquipment(a);
  }
}
