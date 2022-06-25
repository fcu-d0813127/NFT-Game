using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectControllor : MonoBehaviour {
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
