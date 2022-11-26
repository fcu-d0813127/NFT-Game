using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPopUP : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject _DeathWindow;
    [SerializeField] float passTime;
    [SerializeField] float timerStart;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player");
        player.transform.position = transform.position;
        timerStart = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if(player == null)
            return;

        passTime = Time.time - timerStart;

        if(passTime >= 0.1f)
            if(player.GetComponent<HpController>().getHp() <= 0){ 
                _DeathWindow.SetActive(true); 
            }
    }
}
