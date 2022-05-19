using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetailDisplay : MonoBehaviour {
  public TMP_Text Name;
  public TMP_Text Rarity;
  public TMP_Text Atk;
  public TMP_Text Matk;
  public TMP_Text Def;
  public TMP_Text Mdef;
  public TMP_Text Cri;
  public TMP_Text CriDmgRatio;
  public void OnHover(InventoryItemData onHoverItem) {
    var equipment = EquipmentItems.Find(onHoverItem);
    if (equipment == null) {
      return;
    }
    Name.text = $"Name: {equipment.DisplayName}";
    Rarity.text = $"Rarity: {equipment.Rarity}";
    Atk.text = $"+Atk: {equipment.Ability[0]}";
    Matk.text = $"+Matk: {equipment.Ability[1]}";
    Def.text = $"+Def: {equipment.Ability[2]}";
    Mdef.text = $"+Mdef: {equipment.Ability[3]}";
    Cri.text = $"+Cri: {equipment.Ability[4]}";
    CriDmgRatio.text = $"+CriDmgRatio: {equipment.Ability[5]}";
  }
}
