using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   

    public string normakAttackKey = "k";
    public string skillAttack1 = "x"; //波動拳


    // 特殊技能
    public GameObject skillEffect;
    [SerializeField] float _skillOffsetX; //微調技能射出位置
    [SerializeField] float _skillOffsetY;

    // Start is called before the first frame update
    void Start() {
        _skillOffsetX = 1.0f;
        _skillOffsetY = 0.5f;
    }

    // Update is called once per frame
    void Update() {
        //依據按鍵判定攻擊類型
        if(Input.GetKeyDown(normakAttackKey)) {
            normalAttackControllor();
        } else if(Input.GetKeyDown(skillAttack1)){
            specialAttackControllor();
        } else {
            GetComponent<Animator>().SetBool("isAttack", false);
        }
    }

    void normalAttackControllor(){
        GetComponent<Animator>().SetBool("isAttack", true); 
        //發出動畫過0.1秒再做出傷害
        this.Invoke("callAttack", 0.1f);
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
    
    //invoke需呼叫function，故建立此區
    void callAttack(){
        GetComponent<NormalAttackControllor>().normalAttack(playerDirection());
    }

    // true -> 面向左 , false -> 面相右
    bool playerDirection(){
        return GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX;
    }
}
