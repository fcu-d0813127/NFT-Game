using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPointer : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    public GameObject followEnemy;
    private float maxX; 
    private float maxY;
    private float minX;
    private float minY;
    [SerializeField] float showLimit = 1f; //螢幕外多少要顯示箭頭
    [SerializeField] float spaceBetweenScreen = 0.15f; //箭頭與螢幕的間距
    [SerializeField] float startTime = 0.0f; //自進入場景後開始顯示箭頭之時間
    [SerializeField] float arrowSize = 160.0f; //箭頭大小，數值越大箭頭越小

    void Start()
    {   

        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;
        transform.localScale = new Vector3(width / arrowSize , width / arrowSize ,0);  

        maxX = mainCamera.transform.position.x + width / 2;
        maxY = mainCamera.transform.position.y + height / 2;
        minX = mainCamera.transform.position.x - width / 2;
        minY = mainCamera.transform.position.y - height / 2;

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < startTime) return;
        if(followEnemy == null) return;
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;
        Debug.Log(width);

        maxX = mainCamera.transform.position.x + width / 2;
        maxX = maxX - spaceBetweenScreen;

        maxY = mainCamera.transform.position.y + height / 2;
        maxY = maxY - spaceBetweenScreen;

        minX = mainCamera.transform.position.x - width / 2;
        minX = minX + spaceBetweenScreen;

        minY = mainCamera.transform.position.y - height / 2;
        minY = minY + spaceBetweenScreen;

        Vector2 safePos = Vector2.zero;
        safePos.x = Mathf.Clamp(followEnemy.transform.position.x, minX, maxX);
        safePos.y = Mathf.Clamp(followEnemy.transform.position.y, minY, maxY);

        if( ((followEnemy.transform.position.x <= maxX + showLimit) && 
            (followEnemy.transform.position.x >= minX - showLimit) &&
            (followEnemy.transform.position.y <= maxY + showLimit) &&
            (followEnemy.transform.position.y >= minY - showLimit)) ||
            (Time.time < startTime)
        ) {
            GetComponent<SpriteRenderer>().sortingOrder = -1;
            GetComponent<SpriteRenderer>().sortingLayerName = "default";
            
        } else {
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            GetComponent<SpriteRenderer>().sortingLayerName = "ObjectUI";
        }

        transform.position = safePos;

        Vector2 dir = followEnemy.transform.position - GameObject.Find("Player").transform.position;

        //角度硬算出來的，不要太認真看
        float angles = Mathf.Atan2( dir.y, dir.x )  * Mathf.Rad2Deg;
        if(angles <= -90 && angles >= -180){
            angles = (angles * -1) - 90;
        } else if (angles >= 0 && angles <= 180 ) {
            angles = 270 - angles;
        } else if(angles>= -90 && angles <= 0) {
            angles = 270 + (-1 * angles);
        } else {
            angles = 0;
        }

        angles = angles * -1 + 180;
        transform.rotation = Quaternion.Euler(0,0, angles);
    }
}
