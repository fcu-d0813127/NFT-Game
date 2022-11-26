using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDungeonController : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject npc;
    [SerializeField] float _timeRecord;
    [SerializeField] float _continuedTime = 1.5f;

    [SerializeField] float _smoothTime = 0.2f;
    private Vector3 _offset = new Vector3(0f, 0f, -10f);
    private Vector3 _velocity = Vector3.zero;

    // Update is called once per frame
    void Update() {
        if(GameObject.FindWithTag("Enemy") != null){
            return;
        }

        //---怪物全部死亡---

        //開始計時
        _timeRecord += Time.deltaTime;

        //鏡頭調轉結束
        if(_timeRecord >= _continuedTime){
            mainCamera.GetComponent<CameraFollow>().enabled = true;
            return;
        }
        
        //鏡頭調轉中
         mainCamera.GetComponent<CameraFollow>().enabled = false;
        
        //在切換時可能導致z軸亂掉而無法顯示，故強制改為999;
        Vector3 targetPos = npc.transform.position;
        targetPos.z = -9.999f;
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetPos , ref _velocity,
                                            _smoothTime);
    }
}
