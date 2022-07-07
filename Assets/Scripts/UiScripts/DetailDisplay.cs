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
    _rarity.text = equipment.Rarity.ToString();
    _atk.text = equipment.Attribute.Atk.ToString();
    _matk.text = equipment.Attribute.Matk.ToString();
    _def.text = equipment.Attribute.Def.ToString();
    _mdef.text = equipment.Attribute.Mdef.ToString();
    _cri.text = $"{equipment.Attribute.Cri * 100}%";
    _criDmgRatio.text = $"{equipment.Attribute.CriDmgRatio * 100}%";
  }
}
