using UnityEngine;
using TMPro;

public class BackpackAttribute : MonoBehaviour {
  private Attribute _tmpAttribute = new Attribute();
  [SerializeField] private TMP_Text[] _attribute;

  public void UpdateAttribute(Attribute attribute, bool isEquip) {
    if (isEquip) {
      _tmpAttribute.Atk += attribute.Atk;
      _tmpAttribute.Matk += attribute.Matk;
      _tmpAttribute.Def += attribute.Def;
      _tmpAttribute.Mdef += attribute.Mdef;
      _tmpAttribute.Cri += attribute.Cri;
      _tmpAttribute.CriDmgRatio += attribute.CriDmgRatio;
    } else {
      _tmpAttribute.Atk -= attribute.Atk;
      _tmpAttribute.Matk -= attribute.Matk;
      _tmpAttribute.Def -= attribute.Def;
      _tmpAttribute.Mdef -= attribute.Mdef;
      _tmpAttribute.Cri -= attribute.Cri;
      _tmpAttribute.CriDmgRatio -= attribute.CriDmgRatio;
    }
    LoadAttribute();
  }

  public void LoadAttribute() {
    Attribute totalAttribute = new Attribute();
    PlayerAttribute playerAttribute = PlayerInfo.PlayerAttribute;
    totalAttribute.Atk += _tmpAttribute.Atk + playerAttribute.Atk;
    totalAttribute.Matk += _tmpAttribute.Matk + playerAttribute.Matk;
    totalAttribute.Def += _tmpAttribute.Def + playerAttribute.Def;
    totalAttribute.Mdef += _tmpAttribute.Mdef + playerAttribute.Mdef;
    totalAttribute.Cri += _tmpAttribute.Cri + playerAttribute.Cri;
    totalAttribute.CriDmgRatio += _tmpAttribute.CriDmgRatio + playerAttribute.CriDmgRatio;
    DisplayAttribute(totalAttribute);
  }

  public void Save() {
    PlayerInfo.EquipAttribute = Attribute.CopyAttribute(_tmpAttribute);
  }

  public void ResetTmpAttribute() {
    _tmpAttribute = new Attribute();
    LoadAttribute();
  }

  private void OnEnable() {
    LoadAttribute();
  }

  private void DisplayAttribute(Attribute totalAttribute) {
    _attribute[0].text = totalAttribute.Atk.ToString();
    _attribute[1].text = totalAttribute.Matk.ToString();
    _attribute[2].text = totalAttribute.Def.ToString();
    _attribute[3].text = totalAttribute.Mdef.ToString();
    _attribute[4].text = $"{Mathf.FloorToInt(totalAttribute.Cri * 100).ToString("0")}%";
    _attribute[5].text = $"{Mathf.FloorToInt(totalAttribute.CriDmgRatio * 100).ToString("0")}%";
  }
}
