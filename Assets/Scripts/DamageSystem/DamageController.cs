using UnityEngine;

public class DamageController {
  public static int RealDamage(EnemyStatus enemyStatus, AttackType attackType) {
    PlayerAttribute playerPanel = PlayerInfo.PlayerAttribute;
    Attribute equipAttribute = PlayerInfo.EquipAttribute;
    float atk = playerPanel.Atk + equipAttribute.Atk;
    float matk = playerPanel.Matk + equipAttribute.Matk;
    float baseRatio = 1f;  // 基礎倍率
    float skillRatio = 1.0f;
    float abs;  // 傷害減免
    bool baseCri = Random.Range(0, 100) < playerPanel.Cri * 100 ? true : false;  // 是否暴擊
    float criResistRatio = enemyStatus.CriResistRatio;  // 暴擊抗性倍率
    float finalDamage;
    if (attackType == AttackType.Matk) {
      abs = matk / (enemyStatus.Mdef + matk * 0.1f) * baseRatio;
      finalDamage = matk;
    } else {
      abs = atk / (enemyStatus.Def + atk * 0.1f) * baseRatio;
      finalDamage = atk;
    }
    finalDamage = finalDamage * (baseRatio * abs + skillRatio);
    return baseCri ? (int)(finalDamage * (playerPanel.CriDmgRatio - criResistRatio)) :
                     (int)finalDamage;
  }
}
