using UnityEngine;

[System.Serializable]
public class PlayerAbility {
  public int str;
  public int intllegence;
  public int dex;
  public int vit;
  public int luk;

  public static PlayerAbility CreateAbility(string abilityJson) {
    return JsonUtility.FromJson<PlayerAbility>(abilityJson);
  }

  public PlayerAbility(int str, int intllegence, int dex, int vit, int luk) {
    this.str = str;
    this.intllegence = intllegence;
    this.dex = dex;
    this.vit = vit;
    this.luk = luk;
  }
}
