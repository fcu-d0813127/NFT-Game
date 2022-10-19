using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dungeon1EnemyCreater : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject prefabOfEnemyInformation;
    [SerializeField] float[] generateRange = new float[4] {-12.0f, 10.0f, 6.8f, -5f};

    void Awake() {
        EnemyInformation.InitEnemyQuantity();
        GetComponent<EnemyCreater>().createEnemy(enemy, 10, generateRange);
        EnemyInformation.InitEnemyInformation();
        DynamicEnemyList();
    }

    private void DynamicEnemyList() {
        GameObject enemyManage = NormalUseLibrary.FindInActiveObjectByName("EnemyMannage");
        string[] NameOfEnemyList = EnemyInformation.NameOfEnemyList;
        Sprite[] EnemyImage = EnemyInformation.EnemyImage;
        bool[] EnemyQuantity = EnemyInformation.EnemyQuantity;
        for (int i = 0; i < NameOfEnemyList.Length; i++) {
            if (EnemyQuantity[i] == true) {
                GameObject oneEnemy = Instantiate(prefabOfEnemyInformation, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
                oneEnemy.transform.name = NameOfEnemyList[i];
                oneEnemy.GetComponent<Image>().sprite = EnemyImage[i];
                oneEnemy.transform.GetChild(0).gameObject.transform.name = "0";
                oneEnemy.transform.SetParent(enemyManage.transform, false);
            }
        }
    }
}