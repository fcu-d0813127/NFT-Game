using UnityEngine;

[System.Serializable]
public class PlayerEquipment {
  public int Helmet;
  public int Chestplate;
  public int Leggings;
  public int Boots;
  public int Weapon;

  public static PlayerEquipment CreateEquipment(string equipmentJson) {
    return JsonUtility.FromJson<PlayerEquipment>(equipmentJson);
  }
}
