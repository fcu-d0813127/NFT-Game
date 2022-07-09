using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon2EnemyCreater : MonoBehaviour
{
    [SerializeField] GameObject skeleton;
    [SerializeField] GameObject mushroom;
    [SerializeField] GameObject flyingEye;
    [SerializeField] int skeletonNum1;
    [SerializeField] int skeletonNum2;
    [SerializeField] int mushroomNum;
    [SerializeField] int flyingEyeNum;

    [SerializeField] float[] skeletonGenerateRange1; //中上方大型區域
    [SerializeField] float[] skeletonGenerateRange2; //下方走道
    [SerializeField] float[] mushroomGenerateRange;
    [SerializeField] float[] flyingEyeGenerateRange;

    // Start is called before the first frame update
    void Start() {
        //設定生成數量
        skeletonNum1 = 10;
        skeletonNum2 = 3;
        mushroomNum = 7;
        flyingEyeNum = 4;
        
        // 設定生成位置 (左右上下圍出一個範圍)
        skeletonGenerateRange1 = new float[4] {-35.0f, -11.5f, 26f, 15f};
        skeletonGenerateRange2 = new float[4] {-15.0f, 4.5f, 1.2f, -1.4f};
        mushroomGenerateRange = new float[4] {-46.0f, -35f, 9.5f, -0.5f};
        flyingEyeGenerateRange = new float[4] {-34f, -20f, -7.6f, -9.8f};

        //實際生成
        GetComponent<EnemyCreater>().createEnemy(skeleton, skeletonNum1, skeletonGenerateRange1);
        GetComponent<EnemyCreater>().createEnemy(skeleton, skeletonNum2, skeletonGenerateRange2);
        GetComponent<EnemyCreater>().createEnemy(mushroom, mushroomNum, mushroomGenerateRange);
        GetComponent<EnemyCreater>().createEnemy(flyingEye, flyingEyeNum, flyingEyeGenerateRange);
    }
}
