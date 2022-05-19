using UnityEngine;

public class InventoryItemData {
  public int Id;
  public string DisplayName;
  [TextArea(4, 4)]
  public string Description;
  public Sprite Icon;
  public int MaxStackSize;
}
