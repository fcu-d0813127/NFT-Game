using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   
    //[SerializeField] char normalAttackKey = 'k';
    [SerializeField] int _demage;
    public Transform attackPointLeft;
    public Transform attackPointRight;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    // Start is called before the first frame update
    void Start()
    {
        _demage = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<SpriteRenderer>().flipX){
            attackPoint = attackPointLeft;
        }else {
            attackPoint = attackPointRight;
        }


        if (Input.GetKeyDown(KeyCode.K)) {
            GetComponent<Animator>().SetBool("isAttack", true); //利用isAttack這個bool去判定玩家是否在攻擊而播出動畫!
            Collider2D[] hitEnmies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);
        
            foreach(Collider2D enemy in hitEnmies){
                Debug.Log("hit " + enemy.name);

                enemy.GetComponent<EnemyControllor>().hp = enemy.GetComponent<EnemyControllor>().hp - _demage;

                
            }
        
        } else {
            GetComponent<Animator>().SetBool("isAttack", false);
        }
    }

    //顯示偵測範圍 debug用
    void OnDrawGizmosSelected(){
        if(attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
