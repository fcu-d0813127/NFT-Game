using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]

public class InventoryItemData : ScriptableObject {
  public int Id;
  public string DisplayName;
  [TextArea(4, 4)]
  public string Description;
  public Sprite Icon;
  public int MaxStackSize;
}
