using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffectController : MonoBehaviour
{
    [SerializeField] GameObject _hitEffect;
    [SerializeField] int _bulletDamage; //傷害 <-Jimmy (約第61行附近)
    [SerializeField] float flyDistance = 5;
    [SerializeField] float bulletSpeed = 0.2f;

    private bool _shootDirection; //子彈方向(左/右)
    private float _timer; //銷毀物件所需時間
    private Vector3 _originPos; //物件創建時之位置，用以計算飛行距離之用


    void Start() {   
        _bulletDamage = 300;
        _timer = 3; 
        _originPos = this.gameObject.transform.position;

        // 依據玩家方向判斷子彈飛行方向
        if(playerDirection()){
            _shootDirection = true;
            GetComponent<SpriteRenderer>().flipX = true;
        } else{
            _shootDirection = false;
           GetComponent<SpriteRenderer>().flipX = false;
        }
          
    }

    void Update() {     
        // move the bullet
        if(_shootDirection){ //角色向左時
            this.gameObject.transform.position += new Vector3(-1 * bulletSpeed * Time.deltaTime * 60, 0 , 0 );
            
            //到一定距離銷毀
            if(_originPos.x - this.gameObject.transform.position.x > flyDistance)
                Destroy(this.gameObject);
        } else{
            this.gameObject.transform.position += new Vector3(bulletSpeed * Time.deltaTime * 60, 0 , 0 );
            
            //到一定距離銷毀
            if(_originPos.x - this.gameObject.transform.position.x < -1 * flyDistance)
                Destroy(this.gameObject);
        }
           
        // 理論上不會執行到，但若物件停留太久會銷毀
        takeBackObject();
         
    }

    //子彈集中
    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player"){
            return;
        }
        
        if(col.gameObject.tag == "Enemy"){
            Attribute enemyAttribute = col.gameObject.GetComponent<EnemyStatus>().Attribute;
            _bulletDamage = DamageController.RealDamage(PlayerInfo.PlayerAttribute, enemyAttribute, AttackType.Matk, true);
            col.gameObject.GetComponent<HpController>().sufferDamage(_bulletDamage);
            Vector3 generatePos = col.gameObject.transform.position;

            //生成碰撞到的特效
            Instantiate(_hitEffect, generatePos, new Quaternion(0, 0, 0, 1)); 
        }

        Destroy(this.gameObject);
    }

    //判斷玩家所在方向 true右方, left左方
    bool playerDirection(){
        return GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX;
    }
    
    //子彈在場景中存在過久會銷毀
    void takeBackObject(){
        _timer -= Time.deltaTime;
        if(_timer <= 0)
            Destroy(this.gameObject);
    }
}
