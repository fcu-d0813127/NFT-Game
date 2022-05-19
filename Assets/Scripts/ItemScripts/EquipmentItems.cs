using System.Collections.Generic;

public class EquipmentItems {
  public static List<EquipmentItemData> Equipments = new List<EquipmentItemData>();

  public static void Add(EquipmentItemData equipment) {
    var found = Find(equipment);
    if (found != null) {
      return;
    }
    Equipments.Add(equipment);
  }

  public static EquipmentItemData Find(InventoryItemData equipment) {
    foreach(var i in Equipments) {
      if (i.Id == equipment.Id) {
        return i;
      }
    }
    return null;
  }
}
