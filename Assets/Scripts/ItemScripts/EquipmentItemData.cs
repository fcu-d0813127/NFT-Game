using UnityEngine;

public class EquipmentItemData : InventoryItemData {
  public string Rarity;
  public int AbilityNum;
  public int SkillNum;
  public int AbilityRange;
  public int[] Ability;

  public EquipmentItemData(int id, string displayName, int maxStackSize, Sprite icon, string rarity, int abilityNum, int skillNum, int abilityRange, int[] ability) {
    this.Id = id;
    this.DisplayName = displayName;
    this.MaxStackSize = maxStackSize;
    this.Icon = icon;
    this.Rarity = rarity;
    this.AbilityNum = abilityNum;
    this.SkillNum = skillNum;
    this.AbilityRange = abilityRange;
    this.Ability = ability;
  }
}
