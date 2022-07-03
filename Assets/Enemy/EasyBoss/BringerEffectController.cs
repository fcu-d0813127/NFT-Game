using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerEffectController : MonoBehaviour
{   
    [SerializeField] LayerMask _playerLayermask;
    [SerializeField] bool _isHit;
    [SerializeField] GameObject _player;
    [SerializeField] int _damage;

    // Start is called before the first frame update
    void Start() {
        _isHit = false;
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() {
        //特效發出後與實際攻擊的延遲時間
        this.Invoke("startAttack", 0.55f);
    }

    void startAttack(){
        //只攻擊一次
        if(_isHit) return;

        //判定是否攻擊到
        if (Physics2D.OverlapBox(transform.position, transform.localScale / 2, 0.0f, _playerLayermask)) {
            _isHit = true;
            //_damage = ...
            _player.GetComponent<HpController>().sufferDamage(_damage);
        }
    }


    
    //結束攻擊特效的animation Event
    void EndBringerEffect(){
        
        Destroy(this.gameObject);
    }

    
}
