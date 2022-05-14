using UnityEngine;

[System.Serializable]
public class PlayerStatus {
  public string Name;
  public int Level;
  public int Experience;
  public int DistributableAbility;
  public int SiteOfDungeon;
  public int Timestamp;

  public static PlayerStatus CreateStatus(string statusJson) {
    return JsonUtility.FromJson<PlayerStatus>(statusJson);
  }

  public PlayerStatus(
      string name,
      int level,
      int experience,
      int distributableAbility,
      int siteOfDungeon,
      int timestamp) {
    this.Name = name;
    this.Level = level;
    this.Experience = experience;
    this.DistributableAbility = distributableAbility;
    this.SiteOfDungeon = siteOfDungeon;
    this.Timestamp = timestamp;
  }
}
