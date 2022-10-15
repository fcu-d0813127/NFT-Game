using UnityEngine;

[System.Serializable]
public class PlayerEquipment {
  public string description;
  public string image;
  public string name;
  public IpfsAttribute[] attributes;
  [System.Serializable]
  public class IpfsAttribute {
    public string trait_type;
    public string value;
  }
  public static PlayerEquipment CreateEquipment(string equipmentJson) {
    return JsonUtility.FromJson<PlayerEquipment>(equipmentJson);
  }
}
