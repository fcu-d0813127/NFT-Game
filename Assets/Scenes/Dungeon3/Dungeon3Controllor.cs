using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon3Controllor : MonoBehaviour {
    private float _smoothTime = 0.07f;
    private Vector3 _offset = new Vector3(0f, 0f, -10f);
    private Vector3 _velocity = Vector3.zero;

    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
    [SerializeField] float entryEdge;
    [SerializeField] GameObject camera;
    [SerializeField] GameObject bigHpBar;

    private float _entryTime;
    private bool _isStart = false;
    private bool _isEnd = false;
    //Todo:目前先用拉的，但日後仍需考慮Boss用生成的狀況

    void Start() {
        player = GameObject.FindWithTag("Player");
        entryEdge = -17f;
    }

    // Update is called once per frame
    void Update() {
        
        if((player.transform.position.x < entryEdge) && !_isEnd){


            if(!_isStart){
                _entryTime = Time.time;
                _isStart = true;
                bigHpBar.SetActive(true);
            }

            if(Time.time - _entryTime > 1){
                _isEnd = true;
                camera.GetComponent<CameraFollow>().enabled = true;
                return;
            }
            camera.GetComponent<CameraFollow>().enabled = false;
            //在切換時可能導致z軸亂掉而無法顯示，故強制改為999;
            Vector3 tempBoss = boss.transform.position;
            tempBoss.z = -9.999f;
            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, tempBoss, ref _velocity,
                                                _smoothTime);
       
            
        }
    }
}
