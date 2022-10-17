using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon1Controller : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject _DeathWindow;
    [SerializeField] float passTime;
    [SerializeField] float timerStart;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player");
        player.transform.position = transform.position; 
    }

    // Update is called once per frame
    void Update() {
    }

    
}
