using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyAiController : MonoBehaviour {

    [SerializeField] float _deathDelayTime;  //怪物血量歸零後至怪物被清除的時間
    [SerializeField] float _searchRadius; //怪物會發現玩家的距離
    [SerializeField] float _attackRadius ; //怪物會開始攻擊玩家的距離

    //管理敵人現在要做什麼
    [SerializeField] enum Status{idle, run, attack, takeHit, dead};
    [SerializeField] Status _enemyStatus = Status.idle; //公開方便在外觀察狀態

    //用於傷害計算 (for Jimmy)
    public enum EnemyAbilityValue {dungeon1Goblin, dungeon2, dungeon3Boss};
    [SerializeField] EnemyAbilityValue _thisAbilityValue;

    // 攻擊間隔時間
    [SerializeField] float _attackTimeRecord; //該技能距離上次已過多久
    [SerializeField] float _attackFrequency; // 每次技能頻率 (由attackInterval[0]和[1]取隨機)
    [SerializeField] float[] _attackInterval = new float[] {1.0f, 3.0f};

    // 開發方便用
    private GameObject player;
    private Vector2 playerPos;
    private Vector2 _lastPos;
    private UnityEngine.AI.NavMeshAgent agent;
    private bool _isDead;//避免怪物死亡重複呼叫特定程式

    /*怪物數據參考 副本1哥布林  
    _searchRadius = 3.5f;
    _attackRadius = 0.8f;
    _deathDelayTime = 1.0f;*/

    void Start() {

        player = GameObject.FindWithTag("Player");
        playerPos = player.transform.position;
        _lastPos = transform.position;
        _attackFrequency = Random.Range(_attackInterval[0], _attackInterval[1]);
        _attackTimeRecord = _attackFrequency; //使敵人在一開始就會進行攻擊

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        _isDead = false;

    }

    void Update() {
        EnemyAI();
    }


    void EnemyAI(){
        _attackTimeRecord += Time.deltaTime;

        // 依據條件轉換狀態
        if(GetComponent<HpController>().getHp() <= 0){ //怪物死亡
            _enemyStatus = Status.dead;
        } else if(GetComponent<Animator>().GetBool("isTakeHit")){
            _enemyStatus = Status.takeHit;
        } else if(isPlayerInThisCircle(_attackRadius)){ //怪物發現玩家
            _enemyStatus = Status.attack;
        } else if(isPlayerInThisCircle(_searchRadius)){ //怪物即將攻擊玩家
            _enemyStatus = Status.run;
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
            agent.speed = 0;

            //如果正在執行此次攻擊，則return
            if(GetComponent<Animator>().GetBool("isAttack")){
                return;
            }

            if(_attackTimeRecord >= _attackFrequency){
                GetComponent<Animator>().SetBool("isAttack", true);
                GetComponent<Animator>().SetBool("isRun", false);   

                this.Invoke("callAttack", 0.4f);
                
                _attackTimeRecord = 0;
                _attackFrequency = Random.Range(_attackInterval[0], _attackInterval[1]); //下一次發動技能之間隔
            }else{
                GetComponent<Animator>().SetBool("isAttack", false);
                GetComponent<Animator>().SetBool("isRun", false);   
            }
            
        }

        void enemyDead(){
            
            GetComponent<Animator>().SetInteger("isDeath",  GetComponent<Animator>().GetInteger("isDeath") + 1);
            GetComponent<BoxCollider2D>().enabled = false;
            this.Invoke("DestroyThis", _deathDelayTime);
            agent.speed = 0;
            if (_isDead == false) {
                _isDead = true;
                var index = EnemyInformation.AddBooty(this.gameObject.name);
                string[] nameOfEnemyList = EnemyInformation.NameOfEnemyList;
                string thisEnemyName= "";
                for (int i = 0; i < EnemyInformation.NameOfEnemyList.Length; i++) {
                    if (nameOfEnemyList[i] == this.gameObject.name.Substring(0, nameOfEnemyList[i].Length)) {
                        thisEnemyName = nameOfEnemyList[i];
                        break;
                    }
                }
                if (index != -1) {
                    GameObject enemyList = GameObject.Find("EnemyList");
                    for (int i = 0; i < enemyList.transform.GetChild(0).gameObject.transform.childCount; i++) {
                        if (thisEnemyName == enemyList.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.name) {
                            enemyList.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = EnemyInformation.GetOneEnemyBooty(index).ToString();
                            break;
                        }
                    }
                }
                    
            }
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
        GetComponent<Animator>().SetBool("isAttack", false);
    }
    void EndAttack(){
        GetComponent<Animator>().SetBool("isTakeHit", false);
        GetComponent<Animator>().SetBool("isAttack", false);
       
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


    //invoke需呼叫function，故建立此區
    void callAttack(){
        GetComponent<NormalAttackController>().normalAttack(GetComponent<SpriteRenderer>().flipX);
    }

    
    void DestroyThis(){
        Destroy(this.gameObject);
    }
}

