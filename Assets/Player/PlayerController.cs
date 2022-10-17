using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _deathDelayTime;  //血量歸零後至被清除的時間


    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {
        if(GetComponent<HpController>().getHp() <= 0){ //怪物死亡
            GetComponent<Animator>().SetInteger("isDeath",  GetComponent<Animator>().GetInteger("isDeath") + 1);

            this.Invoke("DestroyThis", _deathDelayTime);
        }
        
    }

    void DestroyThis(){
        Destroy(this.gameObject);
    }
}
