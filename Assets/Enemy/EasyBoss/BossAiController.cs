using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAiController : MonoBehaviour {

    //基本怪物AI參數
    [SerializeField] float _deathDelayTime; //怪物血量歸零後至怪物被清除的時間
    [SerializeField] float _searchRadius; //怪物會發現玩家的距離
    [SerializeField] float _attackRadius ; //怪物會開始攻擊玩家的距離

    // 管理敵人現在要做什麼
    [SerializeField] enum Status{idle, run, attack, remoteAttack, takeHit, dead};
    [SerializeField] Status _enemyStatus = Status.idle; //公開方便在外觀察狀態

    // 用於傷害計算 (for Jimmy)
    public enum EnemyAbilityValue {dungeon1Goblin, dungeon2, dungeon3Boss};
    [SerializeField] EnemyAbilityValue _thisAbilityValue;
    
    // 遠程攻擊
    private float  _remoteAttackTime; //上一次發動遠程攻擊之時間
    [SerializeField] float  _remoteAttackFrequency; //希望攻擊能夠幾秒(例如5秒到8秒)發動一次

    // 普通攻擊
    [SerializeField] float _attackTimeRecord; // 距離上次普攻已過多久
    [SerializeField] float _attackFrequency; // 每次技能頻率 (由attackInterval[0]和[1]取隨機)
    [SerializeField] float[] _attackInterval = new float[] {1.0f, 3.0f};
  
    // 開發用
    [SerializeField] GameObject remoteEffect;
    [SerializeField] GameObject bigHpBar;
    private GameObject player;
    private Vector2 playerPos;
    private Vector2 _lastPos;
    private UnityEngine.AI.NavMeshAgent agent;

    void Start() {

        player = GameObject.FindWithTag("Player");
        playerPos = player.transform.position;
        _lastPos = transform.position;
        _remoteAttackTime = Time.time;
        _remoteAttackFrequency = Random.Range(5.0f, 10.0f); 
        _attackTimeRecord += Time.deltaTime;

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    void Update() {
        EnemyAI();
    }


    void EnemyAI(){
        playerPos = player.transform.position;
        //計算時間與重置隨機
        _attackTimeRecord += Time.deltaTime;
        _remoteAttackFrequency = Random.Range(5.0f, 10.0f); 
        
        // 依據條件轉換狀態
        if(GetComponent<HpController>().getHp() <= 0){ //怪物死亡
            _enemyStatus = Status.dead;
        }else if(GetComponent<Animator>().GetBool("isTakeHit")){
            _enemyStatus = Status.takeHit;
        }else if(isPlayerInThisCircle(_attackRadius)){    
           //怪物即將攻擊玩家
            _enemyStatus = Status.attack; 
        } else if(isPlayerInThisCircle(_searchRadius)){ 
            //怪物發現玩家
            if(!GetComponent<Animator>().GetBool("isRemoteAttack")){
                //在隨機區間內發動遠程攻擊
                if(Time.time - _remoteAttackTime > _remoteAttackFrequency){
                    _enemyStatus = Status.remoteAttack;
                    _remoteAttackTime = Time.time;
                    _remoteAttackFrequency = Random.Range(5.0f, 10.0f); 
                } else{
                _enemyStatus = Status.run;
                }
            }
        } else { //怪物未發現玩家
            _enemyStatus = Status.idle;  
        }
           

        //using the status to control
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
            case Status.remoteAttack:
                enemyRemoteAttack();
                break;
            default:
                Debug.Log("此怪物目前無狀態，請注意程式碼");
                break;
        }


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
                //面朝右
                GetComponent<SpriteRenderer>().flipX = true;
            } else {
                //面朝左
                GetComponent<SpriteRenderer>().flipX = false;
            }
            
            agent.SetDestination(playerPos);
            agent.speed = 1;
        }
    
        void enemyAttack(){
            agent.speed = 0;

            //如果正在執行此次攻擊，則return
            if(GetComponent<Animator>().GetBool("isAttack")){
                return;
            }

            if(_attackTimeRecord >= _attackFrequency){
                GetComponent<Animator>().SetBool("isAttack", true);
                GetComponent<Animator>().SetBool("isRun", false);   

                this.Invoke("callAttack", 0.6f); //動畫播出與打出傷害之間的些微延遲
                
                _attackTimeRecord = 0;
                _attackFrequency = Random.Range(_attackInterval[0], _attackInterval[1]); //下一次發動技能之間隔
            }else{
                GetComponent<Animator>().SetBool("isAttack", false);
                GetComponent<Animator>().SetBool("isRun", false);   
            }
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

        void enemyRemoteAttack(){
            GetComponent<Animator>().SetBool("isRemoteAttack", true);
            agent.speed = 0;
           
        }
    }

 
    //受到傷害即將結束了，動畫事件會呼叫這個func
    void EndTakeHit(){
        agent.speed = 1;
        GetComponent<Animator>().SetBool("isTakeHit", false);
        GetComponent<Animator>().SetBool("isTakeHit", false);
    }

    //一次攻擊完成
    void EndAttack(){
        GetComponent<Animator>().SetBool("isTakeHit", false);
        GetComponent<Animator>().SetBool("isAttack", false);
       
    }

    //結束施法動作，並生成遠程攻擊物物件
    void EndRemoteAttack(){
        agent.speed = 1;
        Vector2 generatePos = playerPos; 
        generatePos.y += 1.5f;
        Instantiate(remoteEffect, generatePos, new Quaternion(0, 0, 0, 1));
        GetComponent<Animator>().SetBool("isRemoteAttack", false);
    }

    //判定玩家是否在此物件所圍之園內
    bool isPlayerInThisCircle(float radius){ 
        Collider2D[] nearObject = Physics2D.OverlapCircleAll(transform.position, radius);
        if (nearObject.Length > 0)
            for (int i = 0; i < nearObject.Length; i++)
                if (nearObject[i].tag.Equals("Player"))
                    return true;
        return false;
    }

    //invoke需呼叫function，故建立此
    void DestroyThis(){
        Destroy(bigHpBar);
        Destroy(this.gameObject);
    }

    void callAttack(){
        GetComponent<NormalAttackController>().normalAttack(!GetComponent<SpriteRenderer>().flipX);
    }
}

