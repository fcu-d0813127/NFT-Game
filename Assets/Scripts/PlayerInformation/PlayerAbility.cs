using UnityEngine;

[System.Serializable]
public class PlayerAbility {
  public int Str;
  public int Intllegence;
  public int Dex;
  public int Vit;
  public int Luk;

  public static PlayerAbility CreateAbility(string abilityJson) {
    return JsonUtility.FromJson<PlayerAbility>(abilityJson);
  }

  public PlayerAbility(int str, int intllegence, int dex, int vit, int luk) {
    this.Str = str;
    this.Intllegence = intllegence;
    this.Dex = dex;
    this.Vit = vit;
    this.Luk = luk;
  }
}
