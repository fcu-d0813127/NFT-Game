using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackController : MonoBehaviour
{   
    // 普通攻擊範圍判定點 以一個圓心+半徑畫出來
    [SerializeField] Transform _attackPointLeft;
    [SerializeField] Transform _attackPointRight;
    private Transform _attackPoint;

    [SerializeField] float _attackRange = 0.5f; //圓的半徑
    [SerializeField] LayerMask attackLayers; //指定可以攻擊到的Layer
    [SerializeField] int _attackDamage; //傷害 (for Jimmy)

    void Start() {
        if (_attackPointLeft == null) 
            _attackPointLeft = transform.Find("AttackPointLeft");
        
        if (_attackPointRight == null) 
            _attackPointLeft = transform.Find("AttackPointRight");
        
    }

    // true -> 面向左 , false -> 面相右
    public void normalAttack(bool direction){

        if(direction){
            _attackPoint = _attackPointLeft;
        }else {
            _attackPoint = _attackPointRight;
        }

        Collider2D[] hitObjs = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, attackLayers);
        
        //Jimmy !! 傷害判斷在這
        foreach(Collider2D hitObj in hitObjs){ 
            Debug.Log(hitObj.gameObject.name);
            if (hitObj.tag == "Enemy") {
                Attribute enemyAttribute = hitObj.gameObject.GetComponent<EnemyStatus>().Attribute;
                _attackDamage = DamageController.RealDamage(PlayerInfo.PlayerAttribute, enemyAttribute, AttackType.Atk, true);
            } else {
                Attribute enemyAttribute = GetComponent<EnemyStatus>().Attribute;
                _attackDamage = DamageController.RealDamage(enemyAttribute, PlayerInfo.PlayerAttribute, AttackType.Atk, false);
            }
            hitObj.GetComponent<HpController>().sufferDamage(_attackDamage);      
        }
    }

    //顯示偵測範圍 debug用，使攻擊範圍在scene中顯示出來
    void OnDrawGizmosSelected(){
        if(_attackPoint == null)
            return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
