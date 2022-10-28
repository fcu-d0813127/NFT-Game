using UnityEngine;

public class MaterialIcon {
  public static Sprite Ruby;
  public static Sprite Emerald;
  public static Sprite Sapphire;
  public static void Init() {
    Ruby = Resources.Load<Sprite>("Material/ruby");
    Emerald = Resources.Load<Sprite>("Material/emerald");
    Sapphire = Resources.Load<Sprite>("Material/sapphire");
  }
}
