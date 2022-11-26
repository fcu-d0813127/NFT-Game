using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HpController : MonoBehaviour {
    [SerializeField] int _hp;
    [SerializeField] int _hpMax; //最大血量
    [SerializeField] GameObject popup; 
    // Update is called once per frame
    void Start() {
        _hp = _hpMax;
    }

    public void sufferDamage(int damage) {
        _hp -= damage;
        GetComponent<Animator>().SetBool("isTakeHit", true);

        if(this.gameObject.tag == "Enemy"){
            GameObject obj = Instantiate(popup, transform.position, Quaternion.identity);
            obj.GetComponent<TMP_Text>().text = damage.ToString();
        }
    }
    
    public int getHp() {
        return _hp;
    }

    public int getHpMax(){
        return _hpMax;
    }
    
}
