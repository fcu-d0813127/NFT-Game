using UnityEngine.EventSystems;
using UnityEngine;

public class cursorControllor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
  public Texture2D CursorNormal;
  public Texture2D cursorPointer;
  public Vector2 hotSpot = new Vector2(4,0);

  public void OnPointerEnter(PointerEventData eventData) {
    Cursor.SetCursor(cursorPointer, hotSpot, CursorMode.ForceSoftware);
  }

  public void OnPointerExit(PointerEventData eventData) {
    Cursor.SetCursor(CursorNormal, Vector2.zero, CursorMode.ForceSoftware);
  }

  void OnDestroy() {
    Cursor.SetCursor(CursorNormal, Vector2.zero, CursorMode.ForceSoftware);
  }
}