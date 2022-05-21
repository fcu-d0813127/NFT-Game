using UnityEngine;
using TMPro;

public class DetailDisplay : MonoBehaviour {
  [SerializeField] private TMP_Text _name;
  [SerializeField] private TMP_Text _rarity;
  [SerializeField] private TMP_Text _atk;
  [SerializeField] private TMP_Text _matk;
  [SerializeField] private TMP_Text _def;
  [SerializeField] private TMP_Text _mdef;
  [SerializeField] private TMP_Text _cri;
  [SerializeField] private TMP_Text _criDmgRatio;
  public void OnHover(InventoryItemData onHoverItem) {
    var equipment = EquipmentItems.Find(onHoverItem);
    if (equipment == null) {
      return;
    }
    _name.text = $"Name: {equipment.DisplayName}";
    _rarity.text = $"Rarity: {equipment.Rarity}";
    _atk.text = $"+Atk: {equipment.Ability[0]}";
    _matk.text = $"+Matk: {equipment.Ability[1]}";
    _def.text = $"+Def: {equipment.Ability[2]}";
    _mdef.text = $"+Mdef: {equipment.Ability[3]}";
    _cri.text = $"+Cri: {equipment.Ability[4]}";
    _criDmgRatio.text = $"+CriDmgRatio: {equipment.Ability[5]}";
  }
}
