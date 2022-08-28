using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonButtonController : MonoBehaviour
{
    // Start is called before the first frame update
  public void onClick() {
    GameObject.Find("FireTradeNPC").GetComponent<NpcController>().LeaveDungeonScene();
    Debug.Log("Be clicked!");
  }
}
