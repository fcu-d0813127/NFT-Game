using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectController : MonoBehaviour {
    // 若沒成功銷毀將回收物件
    // 由動畫事件控制此物件的摧毀
    // 但仍設置了一個計時器摧毀做備選方案
    private float _timer;
    
    void Start() {
        _timer = 1.5f;
    }

    void Update() {  
        takeBackObject();
    }

    void destroyThis(){
        Destroy(this.gameObject);
    }

    void takeBackObject(){
        _timer -= Time.deltaTime;
        if(_timer <= 0)
            Destroy(this.gameObject);
    }
}
