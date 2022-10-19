using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarController : MonoBehaviour
{
    [SerializeField] GameObject obj; //此obj可以是任一個需要血條的物件
    private int _tempHp;
    private int _tempHpMax;

    void Start() {
        if (obj == null) {
            obj = GameObject.Find("Player");
        }
        _tempHp = obj.GetComponent<HpController>().getHp();
        _tempHpMax = obj.GetComponent<HpController>().getHpMax();
    }

    // Update is called once per frame
    void Update() {
        if(obj == null)
            return;

        _tempHp = obj.GetComponent<HpController>().getHp();
        _tempHpMax = obj.GetComponent<HpController>().getHpMax();
        
        if (_tempHp <= 0){ //敵人死亡
            //血條長度設定(不能讓血條變負的)
            transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
            
        }else { //敵人未死亡
            float _percent = ((float)_tempHp / (float)_tempHpMax);
            transform.localScale = new Vector3(_percent, transform.localScale.y, transform.localScale.z); //縮放hpBar
        }
    }
}
