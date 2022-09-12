using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
  public void onClick() {
    ButtonEventController.EveryButtonEvent(this.gameObject.name);
  }
}
