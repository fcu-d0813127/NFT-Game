using UnityEngine;

[System.Serializable]
public class PlayerEquipment {
  public int helmet;
  public int chestplate;
  public int leggings;
  public int boots;
  public int weapon;

  public static PlayerEquipment CreateEquipment(string equipmentJson) {
    return JsonUtility.FromJson<PlayerEquipment>(equipmentJson);
  }
}
