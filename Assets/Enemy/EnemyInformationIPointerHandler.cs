using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EnemyInformationIPointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  GameObject enemyList;
  void Start() {
    enemyList = GameObject.Find("EnemyList");
    enemyList.SetActive(false);
  }
  public void OnPointerEnter(PointerEventData pointerEventData){
    enemyList.SetActive(true);
  }

    //Detect when Cursor leaves the GameObject
  public void OnPointerExit(PointerEventData pointerEventData){
    enemyList.SetActive(false);
  }
}
