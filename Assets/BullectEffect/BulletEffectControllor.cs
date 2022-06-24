using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffectControllor : MonoBehaviour
{
    public GameObject hitEffect;
    private bool _shootDirection;
    private float _timer; //銷毀物件所需時間
    private Vector3 _originDistance;
    [SerializeField] int _bulletDemage;
    [SerializeField] float flyDistance = 5;
    [SerializeField] float bulletSpeed = 0.2f;
    // Start is called before the first frame update
    void Start() {   
        _bulletDemage = 50;
        _timer = 3; 
        _originDistance = this.gameObject.transform.position;

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
            if(_originDistance.x - this.gameObject.transform.position.x > flyDistance)
                Destroy(this.gameObject);
        } else{
            this.gameObject.transform.position += new Vector3(bulletSpeed * Time.deltaTime * 60, 0 , 0 );
            
            //到一定距離銷毀
            if(_originDistance.x - this.gameObject.transform.position.x < -1 * flyDistance)
                Destroy(this.gameObject);
        }
           
        // 理論上不會執行到，但若物件停留太久會銷毀
        takeBackObject();
         
    }
    void OnCollisionEnter2D(Collision2D col) {
        //Debug.Log(col.gameObject.name);
        if(col.gameObject.tag == "Player"){
            return;
        }
        
        if(col.gameObject.tag == "Enemy"){
            //Debug.Log("子彈集中怪物惹");
            //減少Hp
            col.gameObject.GetComponent<HpControllor>().sufferDemage(_bulletDemage);
            Vector3 genaratePos = col.gameObject.transform.position;

            //生成碰撞到的特效
            Instantiate(hitEffect, genaratePos, new Quaternion(0, 0, 0, 1)); 
        }

        Destroy(this.gameObject);
    }

    //判斷玩家所在方向 true右方, left左方
    bool playerDirection(){
        return GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX;
    }
    
    void takeBackObject(){
        _timer -= Time.deltaTime;
        if(_timer <= 0)
            Destroy(this.gameObject);
    }
}
