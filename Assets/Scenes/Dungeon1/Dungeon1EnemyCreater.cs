using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon1EnemyCreater : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float[] generateRange = new float[4] {-12.0f, 10.0f, 6.8f, -5f};


    // Start is called before the first frame update
    void Start() {
        GetComponent<EnemyCreater>().createEnemy(enemy, 10, generateRange);
    }
}
