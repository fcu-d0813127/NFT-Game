using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllor : MonoBehaviour {
    [SerializeField] int _hp;
    [SerializeField] int _hpMax;
    [SerializeField] GameObject hpBar;
    [SerializeField] float _deathDelayTime; //怪物血量歸零後至怪物被清除的時間
    [SerializeField] float _searchRadius;
    [SerializeField] float _attackRadius;
    [SerializeField] enum Status{idle, run, attack};
    [SerializeField] Status _enemyStatus;
    private GameObject player;

    // Start is called before the first frame update
    void Start() {
        _hpMax = 100;
        _hp = _hpMax;
        _deathDelayTime = 1.5f; 
        _searchRadius = 3.5f;
        _attackRadius = 0.8f;
        _enemyStatus = Status.idle;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        HpControllor();
        EnemyAI();
    }

    void HpControllor(){
        if (_hp <= 0){ //敵人死亡

            //血條長度設定(不能讓血條變負的)
            hpBar.transform.localScale = new Vector3(0, hpBar.transform.localScale.y, hpBar.transform.localScale.z);

            //動畫設定
            GetComponent<Animator>().SetInteger("isDeath",  GetComponent<Animator>().GetInteger("isDeath") + 1);
            this.Invoke("DestroyThis", _deathDelayTime);
            
        }else {
             //敵人未死亡
            float _percent = ((float)_hp / (float)_hpMax);
            hpBar.transform.localScale = new Vector3(_percent, hpBar.transform.localScale.y, hpBar.transform.localScale.z); //縮放hpBar
        }

       
    }

    void EnemyAI(){
        
        if(isPlayerInThisCricle(_attackRadius)){
            _enemyStatus = Status.attack;
        } else if(isPlayerInThisCricle(_searchRadius)){
            _enemyStatus = Status.run;
        } else {
            _enemyStatus = Status.idle;  
        }
           

        //using the status to controll
        switch(_enemyStatus){
            case Status.idle:
                //Debug.Log("怪物等待中");
                enemyIdle();
                break;
  
            case Status.run:
                //Debug.Log("玩家進入了移動範圍");
                enemyRun();
                break;
            case Status.attack:
                enemyAttack();
                break;
        }

        bool isPlayerInThisCricle(float radius){
            Collider2D[] nearObject = Physics2D.OverlapCircleAll(transform.position, radius);
            if (nearObject.Length > 0)
                for (int i = 0; i < nearObject.Length; i++)
                    if (nearObject[i].tag.Equals("Player"))
                        return true;
            return false;
        }

        void enemyIdle() {
            GetComponent<Animator>().SetBool("isAttack", false);
            GetComponent<Animator>().SetBool("isRun", false);
        }
        void enemyRun(){
            // GetComponent<Animator>().SetBool("isRun", true);
            GetComponent<Animator>().SetBool("isAttack", false);
            Vector2 playerPos = player.transform.position;
            if(playerPos.x >= transform.position.x){
                GetComponent<SpriteRenderer>().flipX = false;
            } else {
                GetComponent<SpriteRenderer>().flipX = true;
            }
           
        }
    
        void enemyAttack(){
            GetComponent<Animator>().SetBool("isAttack", true);
        }
    }
    void DestroyThis(){
        Destroy(this.gameObject);
    }
    
}

