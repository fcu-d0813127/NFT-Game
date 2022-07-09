using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiControllor : MonoBehaviour {


    [SerializeField] float _deathDelayTime;  //怪物血量歸零後至怪物被清除的時間
    [SerializeField] float _searchRadius; //怪物會發現玩家的距離
    [SerializeField] float _attackRadius ; //怪物會開始攻擊玩家的距離
    [SerializeField] enum Status{idle, run, attack, takeHit, dead};
    [SerializeField] Status _enemyStatus = Status.idle; //公開方便在外觀察狀態

    private GameObject player;
    private Vector2 playerPos;
    private Vector2 _lastPos;
    private UnityEngine.AI.NavMeshAgent agent;

    // 怪物數據參考
    // 副本1哥布林  
    // _hp = 100;
    // _searchRadius = 3.5f;
    // _attackRadius = 0.8f;
    // _deathDelayTime = 1.0f;
  
    void Start() {
        //_hpMax = 100;
        //_hp = _hpMax;
        player = GameObject.FindWithTag("Player");
        playerPos = player.transform.position;
        _lastPos = transform.position;
       
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    void Update() {
        EnemyAI();
    }


    void EnemyAI(){
        // 依據條件轉換狀態

        if(GetComponent<HpControllor>().getHp() <= 0){ //怪物死亡
            _enemyStatus = Status.dead;
        }else if(GetComponent<Animator>().GetBool("isTakeHit")){
            _enemyStatus = Status.takeHit;
        }else if(isPlayerInThisCricle(_attackRadius)){ //怪物發現玩家
            _enemyStatus = Status.attack;
        } else if(isPlayerInThisCricle(_searchRadius)){ //怪物即將攻擊玩家
            _enemyStatus = Status.run;
        } else { //怪物未發現玩家
            _enemyStatus = Status.idle;  
        }
           

        //using the status to controll
        switch(_enemyStatus){
            case Status.idle:
                enemyIdle();
                break;
            case Status.run:
                enemyRun();
                break;
            case Status.attack:
                enemyAttack();
                break;
            case Status.dead:
                enemyDead();
                break;
            case Status.takeHit:
                enemyTakeHit();
                break;
            default:
                Debug.Log("此怪物目前無狀態，請注意程式碼");
                break;
        }

        playerPos = player.transform.position;

        // 在每個狀態該做什麼事
        void enemyIdle() {
            GetComponent<Animator>().SetBool("isAttack", false);
            GetComponent<Animator>().SetBool("isRun", false);
            agent.speed = 0;
        }

        void enemyRun(){
            GetComponent<Animator>().SetBool("isAttack", false);
            GetComponent<Animator>().SetBool("isRun", true);
            
            //轉向
            if(playerPos.x >= transform.position.x){
                GetComponent<SpriteRenderer>().flipX = false;
            } else {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            
            agent.SetDestination(playerPos);
            agent.speed = 1;
        }
    
        void enemyAttack(){
            GetComponent<Animator>().SetBool("isAttack", true);
            GetComponent<Animator>().SetBool("isRun", false);
           // GetComponent<Animator>().SetBool("isRun", true);
            agent.speed = 0;
            
        }

        void enemyDead(){
            
            GetComponent<Animator>().SetInteger("isDeath",  GetComponent<Animator>().GetInteger("isDeath") + 1);
            this.Invoke("DestroyThis", _deathDelayTime);
            agent.speed = 0;
        }

        void enemyTakeHit(){ 
            //受傷害的狀態，結束時機相較其他狀態較為不同
            //使用anim event判定結束，搭配func為EndTakeHit()
            agent.speed = 0;
            GetComponent<Animator>().SetBool("isTakeHit", true);
        
        }
    }

  
    //受到傷害即將結束了，動畫事件會呼叫這個func
    void EndTakeHit(){
        agent.speed = 1;
        GetComponent<Animator>().SetBool("isTakeHit", false);
    }


    //判定玩家是否在此物件所圍之園內
    bool isPlayerInThisCricle(float radius){ 
        Collider2D[] nearObject = Physics2D.OverlapCircleAll(transform.position, radius);
        if (nearObject.Length > 0)
            for (int i = 0; i < nearObject.Length; i++)
                if (nearObject[i].tag.Equals("Player"))
                    return true;
        return false;
    }

    //invoke需呼叫function，故建立此
    void DestroyThis(){
        Destroy(this.gameObject);
    }
}

