using UnityEngine;

[System.Serializable]
public class EnemyInformation{
    private static int[] EnemyBooty = new int[5];
    internal static string[] NameOfEnemyList = new string[] {"Goblin", "FlyingEye", "Mushroom", "Skeleton", "BringerOfDeath"};
    internal static void InitEnemyInformation () {
        for (int i = 0; i < EnemyBooty.Length; i++) {
            EnemyBooty[i] = 0;
        }
    }

    internal static int AddBooty (string name) {
        for (int i = 0; i < NameOfEnemyList.Length; i++){
            if (NameOfEnemyList[i] == name.Substring(0, NameOfEnemyList[i].Length)) {
                EnemyBooty[i] += 1;
                return i;
            }
        }
        return -1;//錯誤，暫時無例外處理
    }
    internal static int GetOneEnemyBooty (int index) {//螢幕顯示使用，不用就砍
        return EnemyBooty[index];
    }
    internal static int[] GetEnemyBooty () {//exchangeMatrial使用
        return EnemyBooty;
    }
}
