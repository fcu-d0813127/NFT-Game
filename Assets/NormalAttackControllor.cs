using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackControllor : MonoBehaviour
{   
    // 普通攻擊範圍判定點 以一個圓心+半徑畫出來
    public Transform attackPointLeft;
    public Transform attackPointRight;
    private Transform _attackPoint;
    public float attackRange = 0.5f; 
    public LayerMask attackLayers;
    [SerializeField] int _attackDamage;

    // true -> 面向左 , false -> 面相右
    public void normalAttack(bool direction){
        if(direction){
            _attackPoint = attackPointLeft;
        }else {
            _attackPoint = attackPointRight;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, attackRange, attackLayers);
        
        //Jimmy !! 傷害判斷在這
        foreach(Collider2D enemy in hitEnemies){ 
            //playerDemage = ...
            enemy.GetComponent<HpControllor>().sufferDemage(_attackDamage);      
        }
    }

      //顯示偵測範圍 debug用
    void OnDrawGizmosSelected(){
        if(_attackPoint == null)
            return;
        Gizmos.DrawWireSphere(_attackPoint.position, attackRange);
    }
}
