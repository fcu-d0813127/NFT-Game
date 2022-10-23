using UnityEngine;

public class CursorTexture {
  public static Texture2D CursorDefault;
  public static Texture2D CursorPointer;

  public static void Init() {
    CursorDefault = Resources.Load<Texture2D>("Cursor/cursor-default");
    CursorPointer = Resources.Load<Texture2D>("Cursor/up-sign");
  }
}
