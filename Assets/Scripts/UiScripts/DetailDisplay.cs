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
    _name.text = equipment.DisplayName;
    _rarity.text = equipment.Rarity;
    _atk.text = equipment.Ability[0].ToString();
    _matk.text = equipment.Ability[1].ToString();
    _def.text = equipment.Ability[2].ToString();
    _mdef.text = equipment.Ability[3].ToString();
    _cri.text = equipment.Ability[4].ToString();
    _criDmgRatio.text = equipment.Ability[5].ToString();
  }
}
