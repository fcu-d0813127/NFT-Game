using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;// Required when using Event data.
using UnityEngine;

public class cursorControllor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D cursorPointer;
    public Vector2 hotSpot = new Vector2(4,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Cursor.SetCursor(cursorPointer, hotSpot, CursorMode.ForceSoftware);
    }

    public void OnPointerExit(PointerEventData eventData) {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }

    void OnDestroy() {
         Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }
}
