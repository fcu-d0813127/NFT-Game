using UnityEngine;

[System.Serializable]
public class PlayerStatus {
  public string name;
  public int level;
  public int experience;
  public int distributableAbility;

  public static PlayerStatus CreateStatus(string statusJson) {
    return JsonUtility.FromJson<PlayerStatus>(statusJson);
  }
}
