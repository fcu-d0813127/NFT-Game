using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   
    public bool AttackAble = true;
    public string normalAttackKey = "k";
    public string skillAttack1 = "x"; //波動拳

    [SerializeField] float _attackTimeRecord; // 距離上次普攻已過多久
    [SerializeField] float _attackFrequency = 0.4f; // 每次技能間隔

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
        if (AttackAble == false) {
            return;
        }
        _attackTimeRecord += Time.deltaTime;
        
        if(_attackTimeRecord >= _attackFrequency){
            if(Input.GetKeyDown(normalAttackKey)) {
                normalAttackController();
                _attackTimeRecord = 0;
            } else if(Input.GetKeyDown(skillAttack1)){
                specialAttackController();
                _attackTimeRecord = 0;
            }
            return;
        }

         GetComponent<Animator>().SetBool("isAttack", false);
      
    }

    void normalAttackController(){
        //若目前正在揮擊則不得揮擊
        GetComponent<Animator>().SetBool("isAttack", true); 
        //發出動畫過0.1秒再做出傷害
        this.Invoke("callAttack", 0.1f);       
    }
    
    void specialAttackController(){
        GetComponent<Animator>().SetBool("isAttack", true);
        Vector3 generatePos = this.gameObject.transform.position;
        generatePos.y += _skillOffsetY;
        if(playerDirection()){
            generatePos.x -= _skillOffsetX;
        } else {
            generatePos.x += _skillOffsetX;
        }
        Instantiate(skillEffect, generatePos, new Quaternion(0, 0, 0, 1));
    }
    
    //invoke需呼叫function，故建立此區
    void callAttack(){
        GetComponent<NormalAttackController>().normalAttack(playerDirection());
    }

    // true -> 面向左 , false -> 面相右
    bool playerDirection(){
        return GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX;
    }
}
