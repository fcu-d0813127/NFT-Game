using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   
    public string normakAttackKey = "k";
    public string skillAttack1 = "x";

    // 攻擊傷害
    [SerializeField] int _normalDemage;
    
    // 怪物layer
    public LayerMask enemyLayers;

    // 普通攻擊範圍判定點 以一個圓心+半徑畫出來
    public Transform attackPointLeft;
    public Transform attackPointRight;
    private Transform _attackPoint;
    public float attackRange; 

    // 特殊技能
    public GameObject skillEffect;
    [SerializeField] float _skillOffsetX; //微調技能射出位置
    [SerializeField] float _skillOffsetY;

    // Start is called before the first frame update
    void Start() {
        _normalDemage = 20;
        attackRange = 0.6f;
        _skillOffsetX = 1.0f;
        _skillOffsetY = 0.5f;
    }

    // Update is called once per frame
    void Update() {
        
        //依據按鍵判定攻擊類型
        if(Input.GetKeyDown(normakAttackKey)) {
            float skill = 1.5f;
            Debug.Log(DamageController.RealDamage(new int[]{100, 100, 1}, false, skill));
            normalAttackControllor();
        } else if(Input.GetKeyDown(skillAttack1)){
            specialAttackControllor();
        } else {
            GetComponent<Animator>().SetBool("isAttack", false);
        }
    }

    void normalAttackControllor(){
        if(playerDirection()){
            _attackPoint = attackPointLeft;
        }else {
            _attackPoint = attackPointRight;
        }

        GetComponent<Animator>().SetBool("isAttack", true); //利用isAttack這個bool去判定玩家是否在攻擊而播出動畫!
        Collider2D[] hitEnmies = Physics2D.OverlapCircleAll(_attackPoint.position,attackRange,enemyLayers);

        foreach(Collider2D enemy in hitEnmies){
            enemy.GetComponent<EnemyControllor>().sufferDemage(DamageController.RealDamage(new int[]{100, 100, 1}, false, 1.5f));      
        }
    }
    
    void specialAttackControllor(){
        GetComponent<Animator>().SetBool("isAttack", true);
        Vector3 genaratePos = this.gameObject.transform.position;
        genaratePos.y += _skillOffsetY;
        if(playerDirection()){
            genaratePos.x -= _skillOffsetX;
        } else {
            genaratePos.x += _skillOffsetX;
        }
        Instantiate(skillEffect, genaratePos, new Quaternion(0, 0, 0, 1));
    }

    //顯示偵測範圍 debug用
    void OnDrawGizmosSelected(){
        if(_attackPoint == null)
            return;
        Gizmos.DrawWireSphere(_attackPoint.position, attackRange);
    }

    // true -> 面向左 , false -> 面相右
    bool playerDirection(){
        return GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX;
    }
}
