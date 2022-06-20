using UnityEngine;

public class DamageController {
  public static int RealDamage(int[] enemyStatus, bool attackType, float skillRatio) {
    float baseRatio = 1f;
    float abs;
    float criRatio = 0.1f;
    bool baseCri = Random.Range(0, 100) < criRatio * 100 ? true : false;;
    float criResistRatio = enemyStatus[2];
    float finalDamage;
    PlayerPanel playerPanel = PlayerInfo.PlayerPanel;
    // attackType, false: 物理, true: 魔法
    // enemyStatus, {物理防禦, 魔法防禦, 爆擊抵抗}
    if (attackType) {
      abs = playerPanel.Matk / (enemyStatus[1] + playerPanel.Matk * 0.1f) * baseRatio;
      finalDamage = playerPanel.Matk;
    } else {
      abs = playerPanel.Atk / (enemyStatus[0] + playerPanel.Atk * 0.1f) * baseRatio;
      finalDamage = playerPanel.Atk;
    }
    finalDamage = finalDamage * (baseRatio * abs + skillRatio);
    return baseCri ? (int)(finalDamage * (skillRatio * criResistRatio)) : (int)finalDamage;
  }
}
