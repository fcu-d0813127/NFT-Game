using UnityEngine;

[System.Serializable]
public class PlayerStatus {
  public string name;
  public int carrer;
  public int siteOfDungeon;
  public int timestamp;

  public static PlayerStatus CreateStatus(string statusJson) {
    return JsonUtility.FromJson<PlayerStatus>(statusJson);
  }
}
