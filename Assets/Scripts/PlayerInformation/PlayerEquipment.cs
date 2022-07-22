using UnityEngine;

[System.Serializable]
public class PlayerEquipment {
  public Equipment[] equipments;

  [System.Serializable]
  public class Equipment {
    public int tokenId;
    public EquipmentStatus equipmentStatus;
  }
  public static PlayerEquipment CreateEquipment(string equipmentJson) {
    return JsonUtility.FromJson<PlayerEquipment>(equipmentJson);
  }
}
