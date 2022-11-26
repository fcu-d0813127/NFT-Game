using UnityEngine;

public class DamageController {
  public static int RealDamage(Attribute attacker, Attribute defender, AttackType attackType, bool hasEquipment) {
    Attribute equipAttribute = new Attribute();
    if (hasEquipment == true) {
      equipAttribute = PlayerInfo.EquipAttribute;
    }
    float atk = attacker.Atk + equipAttribute.Atk;
    float matk = attacker.Matk + equipAttribute.Matk;
    float baseRatio = 1f;  // 基礎倍率
    float skillRatio = 1.0f;
    float abs;  // 傷害減免
    bool baseCri = Random.Range(0, 100) < attacker.Cri * 100 ? true : false;  // 是否暴擊
    float criResistRatio = 0.05f;  // 暴擊抗性倍率
    float finalDamage;
    if (attackType == AttackType.Matk) {
      abs = matk / (defender.Mdef + matk * 0.1f) * baseRatio;
      finalDamage = matk;
    } else {
      abs = atk / (defender.Def + atk * 0.1f) * baseRatio;
      finalDamage = atk;
    }
    finalDamage = finalDamage * (baseRatio * abs + skillRatio);
    return baseCri ? (int)(finalDamage * (attacker.CriDmgRatio - criResistRatio)) :
                     (int)finalDamage;
  }
}
