using UnityEngine;

[System.Serializable]
public class EnemyInformation{
  private static int[] EnemyBooty = new int[5];
  internal static string[] NameOfEnemyList = new string[] {"Goblin", "FlyingEye", "Mushroom", "Skeleton", "BringerOfDeath"};
  internal static Sprite[] EnemyImage = new Sprite[5];
  internal static bool[] EnemyQuantity = new bool[5];
  void Start() {
    SetEnemyImage();
    Debug.Log("ImageLoad!");
  }
  internal static void InitEnemyInformation() {
    for (int i = 0; i < EnemyBooty.Length; i++) {
      EnemyBooty[i] = 0;
    }
    SetEnemyImage();
  }

  internal static int AddBooty(string name) {
    for (int i = 0; i < NameOfEnemyList.Length; i++){
      if (NameOfEnemyList[i] == name.Substring(0, NameOfEnemyList[i].Length)) {
        EnemyBooty[i] += 1;
        return i;
      }
    }
    return -1;//錯誤，暫時無例外處理
  }
  internal static int GetOneEnemyBooty(int index) {
    return EnemyBooty[index];
  }
  private static void SetEnemyImage() {
    EnemyImage[0] = Resources.Load<Sprite>("EnemyImage/Goblin");
    EnemyImage[1] = Resources.Load<Sprite>("EnemyImage/FlyingEye");
    EnemyImage[2] = Resources.Load<Sprite>("EnemyImage/Mushroom");
    EnemyImage[3] = Resources.Load<Sprite>("EnemyImage/Skeleton");
    EnemyImage[4] = Resources.Load<Sprite>("EnemyImage/BringerOfDeath");
  }
  internal static void InitEnemyQuantity() {
    for (int i = 0; i < NameOfEnemyList.Length; i++) {
      EnemyQuantity[i] = false;
    }
  }
  internal static void SetEnemyQuantity(string enemyName) {
    for (int i = 0; i < NameOfEnemyList.Length; i++) {
      Debug.Log(enemyName);
      if (NameOfEnemyList[i] == enemyName) {
        EnemyQuantity[i] = true;
      }
    }
  }
}
