using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon3EnemyCreater : MonoBehaviour
{   
    [SerializeField] GameObject skeleton;
    [SerializeField] GameObject mushroom;
    [SerializeField] GameObject flyingEye;

    [SerializeField] float[] rightGenerateRange;
    [SerializeField] float[] leftGenerateRange;
    void Awake() {
        rightGenerateRange = new float[4] {-16, 4, 0.9f, -1.6f};
        leftGenerateRange = new float[4] {-29, -22, 6, -7};
        GetComponent<EnemyCreater>().createEnemy(skeleton, 1, rightGenerateRange);
        GetComponent<EnemyCreater>().createEnemy(mushroom, 1, rightGenerateRange);
        GetComponent<EnemyCreater>().createEnemy(flyingEye, 1, rightGenerateRange);

        GetComponent<EnemyCreater>().createEnemy(skeleton, 2, leftGenerateRange);
        GetComponent<EnemyCreater>().createEnemy(mushroom, 2, leftGenerateRange);
        GetComponent<EnemyCreater>().createEnemy(flyingEye, 2, leftGenerateRange);
    }

  
}
