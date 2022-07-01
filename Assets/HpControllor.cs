using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpControllor : MonoBehaviour {
    [SerializeField] int _hp;
    [SerializeField] int _hpMax; //最大血量

    // Update is called once per frame
    void Start() {
        _hp = _hpMax;
        
    }

    public void sufferDemage(int demage) {
        _hp -= demage;
        GetComponent<Animator>().SetBool("isTakeHit", true);
        
    }
    
    public int getHp() {
        return _hp;
    }

    public int getHpMax(){
        return _hpMax;
    }
    
}
