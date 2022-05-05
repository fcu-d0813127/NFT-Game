using UnityEngine;

[System.Serializable]
public class PlayerAbility {
  public int str;
  public int intllegence;
  public int dex;
  public int luk;

  public static PlayerAbility CreateAbility(string abilityJson) {
    return JsonUtility.FromJson<PlayerAbility>(abilityJson);
  }
}
