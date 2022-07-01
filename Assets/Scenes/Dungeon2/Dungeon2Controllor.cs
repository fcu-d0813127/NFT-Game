using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon2Controllor : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 spawnPoint = new Vector3(7.77f, 6.68f, 0);
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player");
        player.transform.position = spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
