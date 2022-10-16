using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempButtonController : MonoBehaviour {
  private Button _myselfButton;
  private List<PlayerEquipment> _playerEquipments = new List<PlayerEquipment>();

  private void Awake() {
    _myselfButton = GetComponent<Button>();
    _myselfButton.onClick.AddListener(Change);
  }

  private void Change() {
    string b = "{\"description\":\"一把樸實無華的武器\",\"image\":\"ipfs://QmcRyo6LNNRRgAXSrdCuFqzZP4HXVhnsGe52uvFFB64oZE\",\"name\":\"\u57fa\u790e\u6b66\u5668\",\"attributes\":[{\"trait_type\":\"rarity\",\"value\":\"common\"},{\"trait_type\":\"atk\",\"value\":100},{\"trait_type\":\"def\",\"value\":100},{\"trait_type\":\"matk\",\"value\":0},{\"trait_type\":\"mdef\",\"value\":0},{\"trait_type\":\"cri\",\"value\":0},{\"trait_type\":\"criDmgRatio\",\"value\":0}]}";
    PlayerEquipment a = PlayerEquipment.CreateEquipment(b);
    _playerEquipments.Add(a);
    PlayerInfo.PlayerEquipment = _playerEquipments;
    PlayerInfo.EquipmentTokenIds = new string[]{"1"};
  }
}
