using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllor : MonoBehaviour {
    public int hp;

    [SerializeField] int _hpMax; //最大血量
    [SerializeField] GameObject hpBar;
    [SerializeField] float _deathDelayTime;  //怪物血量歸零後至怪物被清除的時間
    [SerializeField] float _searchRadius; //怪物會發現玩家的距離
    [SerializeField] float _attackRadius ; //怪物會開始攻擊玩家得距離
    [SerializeField] enum Status{idle, run, attack, dead};
    [SerializeField] Status _enemyStatus = Status.idle;

    private GameObject player;
    private Vector2 playerPos;
    private UnityEngine.AI.NavMeshAgent agent;
    private Vector3 _lastPos;
    
    void Start() {
        _hpMax = 100;
        hp = _hpMax;
        player = GameObject.FindWithTag("Player");
        playerPos = player.transform.position;
        _lastPos = transform.position;
        _searchRadius = 3.5f;
        _attackRadius = 0.8f;
        _deathDelayTime = 1.0f;

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    void Update() {
        
        HpBarControllor();
        EnemyAI();
        //Debug.Log(_enemyStatus);
    }

    void HpBarControllor(){
        if (hp <= 0){ //敵人死亡
            //血條長度設定(不能讓血條變負的)
            hpBar.transform.localScale = new Vector3(0, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        }else { //敵人未死亡
            float _percent = ((float)hp / (float)_hpMax);
            hpBar.transform.localScale = new Vector3(_percent, hpBar.transform.localScale.y, hpBar.transform.localScale.z); //縮放hpBar
        }
    }

    void EnemyAI(){
        // 依據條件轉換狀態
        if(hp <= 0){ //怪物死亡
            _enemyStatus = Status.dead;
        }else if(isPlayerInThisCricle(_attackRadius)){ //怪物發現玩家
            _enemyStatus = Status.attack;
        } else if(isPlayerInThisCricle(_searchRadius)){ //怪物即將攻擊玩家
            _enemyStatus = Status.run;
        }  else { //怪物未發現玩家
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
           // GetComponent<Animator>().SetBool("isRun", true);
            agent.speed = 0;
            
        }

        void enemyDead(){
            
            GetComponent<Animator>().SetInteger("isDeath",  GetComponent<Animator>().GetInteger("isDeath") + 1);
            this.Invoke("DestroyThis", _deathDelayTime);
            agent.speed = 0;
        }

    }

    void DestroyThis(){
        Destroy(this.gameObject);
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

}

