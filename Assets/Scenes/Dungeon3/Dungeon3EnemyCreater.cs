using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dungeon3EnemyCreater : MonoBehaviour
{   
    [SerializeField] GameObject skeleton;
    [SerializeField] GameObject mushroom;
    [SerializeField] GameObject flyingEye;
    [SerializeField] GameObject prefabOfEnemyInformation;

    [SerializeField] float[] rightGenerateRange;
    [SerializeField] float[] leftGenerateRange;
    // Start is called before the first frame update
    void Awake() {
        EnemyInformation.InitEnemyQuantity();
        rightGenerateRange = new float[4] {-16, 4, 0.9f, -1.6f};
        leftGenerateRange = new float[4] {-29, -22, 6, -7};
        GetComponent<EnemyCreater>().createEnemy(skeleton, 1, rightGenerateRange);
        GetComponent<EnemyCreater>().createEnemy(mushroom, 1, rightGenerateRange);
        GetComponent<EnemyCreater>().createEnemy(flyingEye, 1, rightGenerateRange);

        GetComponent<EnemyCreater>().createEnemy(skeleton, 2, leftGenerateRange);
        GetComponent<EnemyCreater>().createEnemy(mushroom, 2, leftGenerateRange);
        GetComponent<EnemyCreater>().createEnemy(flyingEye, 2, leftGenerateRange);
        EnemyInformation.InitEnemyInformation();
        DynamicEnemyList();
    }

    private void DynamicEnemyList() {
        GameObject enemyManage = NormalUseLibrary.FindInActiveObjectByName("EnemyMannage");
        string[] NameOfEnemyList = EnemyInformation.NameOfEnemyList;
        Sprite[] EnemyImage = EnemyInformation.EnemyImage;
        bool[] EnemyQuantity = EnemyInformation.EnemyQuantity;
        EnemyQuantity[4] = true;
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
