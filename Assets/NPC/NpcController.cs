using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NpcController : MonoBehaviour
{
    [SerializeField] Vector2 _currentDistance; 
    [SerializeField] float _totalDistance = 10; 
    [SerializeField] float speed = 0.7f;


    void Update(){

        if(GameObject.FindWithTag("Enemy") != null){
            return;
        }
        
        _currentDistance += new Vector2(5, 0) * Time.deltaTime;

        if( _currentDistance.x < _totalDistance)
            this.gameObject.transform.Translate(new Vector2(speed, 0) * Time.deltaTime);


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        SceneManager.LoadScene("Initialization");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);
    }
}
