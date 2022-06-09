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
        //Debug.Log(enemy.GetComponent<Renderer>().bounds.size.x);
        //Debug.Log(enemy.GetComponent<Renderer>().bounds.size.y);
        // Debug.Log(rt.rect.width);
        // Debug.Log(rt.rect.height);


        if (enemyNum == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            int tempCount = 0;
            int whileCount = 0;
            while (tempCount < enemyNum)
            {

                Vector2 genaratePos = new Vector2(Random.Range(-9.0f, 7.0f), Random.Range(-4f, 4f)); //生成之位置
                //Vector2 boxSize = new Vector2(3f, 2.5f); //該矩形區域的大小

                if (!Physics2D.OverlapBox(genaratePos, enemy.transform.localScale / 2, 0.0f, layermask)) //查看該矩形區域是否有任何碰撞體
                {
                    Instantiate(enemy, genaratePos, new Quaternion(0, 0, 0, 1));

                    tempCount++;
                }

                if (whileCount > 200)
                {
                    Debug.Log("物件難以找到隨機生成位置，請重新檢查程式碼 !");
                    return;

                }
                else
                {
                    whileCount++;
                }

            }

        }
    }
}
