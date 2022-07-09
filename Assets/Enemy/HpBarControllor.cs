using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarControllor : MonoBehaviour
{
    [SerializeField] GameObject obj;
    private int _tempHp;
    private int _tempHpMax;

    void Start() {
        _tempHp = obj.GetComponent<HpControllor>().getHp();
        _tempHpMax = obj.GetComponent<HpControllor>().getHpMax();
    }

    // Update is called once per frame
    void Update() {
        _tempHp = obj.GetComponent<HpControllor>().getHp();
        _tempHpMax = obj.GetComponent<HpControllor>().getHpMax();
        if (_tempHp <= 0){ //敵人死亡
            //血條長度設定(不能讓血條變負的)
            transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
            
        }else { //敵人未死亡
            float _percent = ((float)_tempHp / (float)_tempHpMax);
            transform.localScale = new Vector3(_percent, transform.localScale.y, transform.localScale.z); //縮放hpBar
        }
    }
}
