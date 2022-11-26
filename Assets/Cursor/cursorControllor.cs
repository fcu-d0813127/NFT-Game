using UnityEngine.EventSystems;
using UnityEngine;

public class cursorControllor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
  [SerializeField] private Vector2 _hotSpot = new Vector2(4,0);
  private Texture2D _cursorDefault;
  private Texture2D _cursorPointer;

  public void OnPointerEnter(PointerEventData eventData) {
    Cursor.SetCursor(_cursorPointer, _hotSpot, CursorMode.ForceSoftware);
  }

  public void OnPointerExit(PointerEventData eventData) {
    Cursor.SetCursor(_cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
  }

  private void Awake() {
    _cursorDefault = CursorTexture.CursorDefault;
    _cursorPointer = CursorTexture.CursorPointer;
  }

  private void OnDisable() {
    Cursor.SetCursor(_cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
  }
}