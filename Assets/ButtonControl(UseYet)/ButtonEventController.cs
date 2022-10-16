using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventController : MonoBehaviour
{
    // Start is called before the first frame update
  internal static void EveryButtonEvent(string buttonName) {//用Button名稱判斷不同事件觸發
    if (buttonName == "ExitDungeonButton") {
      GameObject.FindWithTag("Trader").GetComponent<NpcController>().LeaveDungeonScene();
    }  
  }
}
