using UnityEngine;

public class PlayerEquips {
  public int helmet;
  public int chestplate;
  public int leggings;
  public int boots;
  public int weapon;

  public static int[] CreateEquips(string equipsJson) {
    PlayerEquips playerEquips = JsonUtility.FromJson<PlayerEquips>(equipsJson);
    int[] equips = new int[5];
    equips[0] = playerEquips.weapon;
    equips[1] = playerEquips.helmet;
    equips[2] = playerEquips.chestplate;
    equips[3] = playerEquips.leggings;
    equips[4] = playerEquips.boots;
    return equips;
  }
}
