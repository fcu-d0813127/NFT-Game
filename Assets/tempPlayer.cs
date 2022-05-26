using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float move_speed = 10.0f;

    // Update is called once per frame
    void Update() {
         if (Input.GetKey(KeyCode.RightArrow)) {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.Translate(move_speed * Time.deltaTime, 0, 0);
            
        } else if (Input.GetKey(KeyCode.LeftArrow))  {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.Translate(-move_speed * Time.deltaTime, 0, 0);
        }
        
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(0, move_speed * Time.deltaTime, 0);

        } else if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(0, -move_speed * Time.deltaTime, 0);
        } 
    }
}
