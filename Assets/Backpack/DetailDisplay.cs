using UnityEngine;
using TMPro;

public class DetailDisplay : MonoBehaviour {
  [SerializeField] private TMP_Text _name;
  [SerializeField] private TMP_Text _rarity;
  [SerializeField] private TMP_Text _part;
  [SerializeField] private TMP_Text _atk;
  [SerializeField] private TMP_Text _matk;
  [SerializeField] private TMP_Text _def;
  [SerializeField] private TMP_Text _mdef;
  [SerializeField] private TMP_Text _cri;
  [SerializeField] private TMP_Text _criDmgRatio;
  private enum EquipPart {
    Weapon, Breastplate, Pants, Helmet, Shoes
  }

  public void OnHover(InventoryItemData onHoverItem) {
    var equipment = EquipmentItems.Find(onHoverItem);
    if (equipment == null) {
      return;
    }
    if (equipment.Part == (int)EquipPart.Weapon) {
      _part.text = "武器";
    } else if (equipment.Part == (int)EquipPart.Helmet) {
      _part.text = "頭盔";
    } else if (equipment.Part == (int)EquipPart.Breastplate) {
      _part.text = "胸甲";
    } else if (equipment.Part == (int)EquipPart.Pants) {
      _part.text = "護腿";
    } else if (equipment.Part == (int)EquipPart.Shoes) {
      _part.text = "靴子";
    }
    if (equipment.Rarity == "common") {
      _rarity.text = "常見";
    } else if (equipment.Rarity == "uncommon") {
      _rarity.text = "不常見";
    } else if (equipment.Rarity == "rare") {
      _rarity.text = "稀有";
    } else if (equipment.Rarity == "legendary") {
      _rarity.text = "傳說";
    }
    _name.text = equipment.DisplayName;
    _atk.text = equipment.Attribute.Atk.ToString();
    _matk.text = equipment.Attribute.Matk.ToString();
    _def.text = equipment.Attribute.Def.ToString();
    _mdef.text = equipment.Attribute.Mdef.ToString();
    _cri.text = $"{equipment.Attribute.Cri * 100}%";
    _criDmgRatio.text = $"{equipment.Attribute.CriDmgRatio * 100}%";
  }
}
