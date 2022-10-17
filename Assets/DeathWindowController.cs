using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWindowController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("working");
        // if(GameObject.Find("Player") == null){
        //     Debug.Log("dead");
        //     this.gameObject.SetActive(true);
        // }
    }

    public void callDeath() {
        Debug.Log("call");
        this.gameObject.SetActive(true);
    }
}
