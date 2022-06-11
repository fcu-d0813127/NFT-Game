using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreater : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int enemyNum;

    [SerializeField] private LayerMask layermask;

    private int layerAsLayerMask;
    void Start()
    {

        if (enemyNum == 0) {
            gameObject.SetActive(false);
        }
        else {
            int tempCount = 0;
            int whileCount = 0;
            while (tempCount < enemyNum) {

                Vector2 genaratePos = new Vector2(Random.Range(-12.0f, 10.0f), Random.Range(6.8f, -5f)); //生成之位置
                //Vector2 boxSize = new Vector2(3f, 2.5f); //該矩形區域的大小

                //查看該矩形區域是否有任何碰撞體
                if (!Physics2D.OverlapBox(genaratePos, enemy.transform.localScale / 2, 0.0f, layermask)) {
                    Instantiate(enemy, genaratePos, new Quaternion(0, 0, 0, 1));

                    tempCount++;
                }

                if (whileCount > 200) {
                    Debug.Log("物件難以找到隨機生成位置，請重新檢查程式碼 !");
                    return;

                }
                else {
                    whileCount++;
                }

            }

        }
    }
}
