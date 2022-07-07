using UnityEngine;

public class DamageController {
  public static int RealDamage(EnemyStatus enemyStatus, AttackType attackType, float skillRatio) {
    PlayerAttribute playerPanel = PlayerInfo.PlayerAttribute;
    float baseRatio = 1f;  // 基礎倍率
    float abs;  // 傷害減免
    bool baseCri = Random.Range(0, 100) < playerPanel.Cri * 100 ? true : false;  // 是否暴擊
    float criResistRatio = enemyStatus.CriResistRatio;  // 暴擊抗性倍率
    float finalDamage;
    if (attackType == AttackType.Matk) {
      abs = playerPanel.Matk / (enemyStatus.Mdef + playerPanel.Matk * 0.1f) * baseRatio;
      finalDamage = playerPanel.Matk;
    } else {
      abs = playerPanel.Atk / (enemyStatus.Def + playerPanel.Atk * 0.1f) * baseRatio;
      finalDamage = playerPanel.Atk;
    }
    finalDamage = finalDamage * (baseRatio * abs + skillRatio);
    return baseCri ? (int)(finalDamage * (playerPanel.CriDmgRatio - criResistRatio)) :
                     (int)finalDamage;
  }
}
