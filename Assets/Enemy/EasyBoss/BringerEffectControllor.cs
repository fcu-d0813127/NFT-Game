using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerEffectControllor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //結束攻擊特效的Event
    void EndBringerEffect(){
        
        Destroy(this.gameObject);
    }
}
