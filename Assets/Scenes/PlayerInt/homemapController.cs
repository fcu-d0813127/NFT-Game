using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homemapController : MonoBehaviour
{
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.FindWithTag("Player");
        player.transform.position = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
