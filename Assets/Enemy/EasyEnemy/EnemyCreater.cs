using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreater : MonoBehaviour
{

    [SerializeField] LayerMask dontTouchLayermask;

    //物件生成 : (物件, 數量, [X軸最左邊, x軸最右邊, y軸最上面, y軸最下面])
    public void createEnemy(GameObject enemy, int enemyNum, float[] range){
        int tempCount = 0; //實際生成到第幾隻
        int whileCount = 0; //第幾次嘗試生成
        EnemyInformation.SetEnemyQuantity(enemy.transform.name);
        while (tempCount < enemyNum) {
            //Vector2 generatePos = new Vector2(Random.Range(-12.0f, 10.0f), Random.Range(6.8f, -5f));
            Vector2 generatePos = new Vector2(Random.Range(range[0], range[1]), Random.Range(range[2], range[3]));
            
            //查看該矩形區域是否有任何碰撞體
            if (!Physics2D.OverlapBox(generatePos, enemy.transform.localScale / 2, 0.0f, dontTouchLayermask)) {
                Instantiate(enemy, generatePos, new Quaternion(0, 0, 0, 1));

                tempCount++;
            }

            if (whileCount > 200) {
                Debug.Log("物件難以找到隨機生成位置，請重新檢查程式碼 !");
                return;

            } else {
                whileCount++;
            }

        }
    }
}
