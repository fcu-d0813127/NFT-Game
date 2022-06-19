using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon2EnemyCreater : MonoBehaviour
{
    [SerializeField] GameObject skeleton;
    [SerializeField] float[] generateRange = new float[4] {-35.0f, -11.5f, 26f, 15f};


    // Start is called before the first frame update
    void Start() {
        generateRange = new float[4] {-35.0f, -11.5f, 26f, 15f};

        Debug.Log(generateRange);
        GetComponent<EnemyCreater>().createEnemy(skeleton, 10, generateRange);
    }
}
