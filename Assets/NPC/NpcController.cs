using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using TMPro;

public class NpcController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void exchangeMaterial(string enemyBooty);
    [SerializeField] Vector2 _currentDistance; 
    [SerializeField] float _totalDistance = 10; 
    [SerializeField] float speed = 0.7f;
    public GameObject PrefabOfShowMaterial;
    internal GameObject _showMaterial;
    void Update(){

        if(GameObject.FindWithTag("Enemy") != null){
            return;
        }
        
        _currentDistance += new Vector2(5, 0) * Time.deltaTime;

        if( _currentDistance.x < _totalDistance)
            this.gameObject.transform.Translate(new Vector2(speed, 0) * Time.deltaTime);


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        #if UNITY_EDITOR
            int[] enemyBootyTemp = new int[5];//這邊數字(怪物種類數量)寫死
            for (int i = 0; i < 5; i++) {//這邊數字(怪物種類數量)寫死
                enemyBootyTemp[i] = EnemyInformation.GetOneEnemyBooty(i);
            }
            PlayerInfo.MaterialNum.Ruby = PlayerInfo.MaterialNum.Ruby + enemyBootyTemp[0] * 10 + enemyBootyTemp[1] * 10 + enemyBootyTemp[2] * 10 + enemyBootyTemp[3] * 20 + enemyBootyTemp[4] * 100;
            PlayerInfo.MaterialNum.Sapphire = PlayerInfo.MaterialNum.Sapphire + enemyBootyTemp[1] * 10 + enemyBootyTemp[2] * 10 + enemyBootyTemp[3] * 20 + enemyBootyTemp[4] * 100;
            PlayerInfo.MaterialNum.Emerald = PlayerInfo.MaterialNum.Emerald + enemyBootyTemp[2] * 10 + enemyBootyTemp[3] * 20 + enemyBootyTemp[4] * 100;
            string useForFollowFuntion = PlayerInfo.MaterialNum.Ruby.ToString() + ',' + PlayerInfo.MaterialNum.Sapphire.ToString() + ',' + PlayerInfo.MaterialNum.Emerald.ToString();
            ShowRewardMaterial(useForFollowFuntion);
        #endif
        #if UNITY_WEBGL && !UNITY_EDITOR//這邊會先字串處理後再丟到jslib使用，避免一些麻煩
            int[] enemyBootyTemp  = new int[5];//這邊數字(怪物種類數量)寫死
            for (int i = 0; i < 5; i++) {//這邊數字(怪物種類數量)寫死
                enemyBootyTemp[i] = EnemyInformation.GetOneEnemyBooty(i);
            }
            string exchangeTemp = "";
            for (int i = 0; i < enemyBootyTemp.Length; i++){
                exchangeTemp = exchangeTemp + enemyBootyTemp[i] + ',';
            }
            exchangeTemp = exchangeTemp.Remove(exchangeTemp.Length - 1, 1);
            exchangeMaterial(exchangeTemp);
        #endif
    }

    internal void ShowRewardMaterial(string materialOfSend)
    {   
        string[] materialOfGain = materialOfSend.Split(',');
        _showMaterial = Instantiate(PrefabOfShowMaterial);
        _showMaterial.transform.SetParent(GameObject.Find("Canvas").transform, false);
        _showMaterial.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "You gain the following material\nRuby:" + materialOfGain[0] + "\nSapphire:" + materialOfGain[1] + "\nEmerald:" + materialOfGain[2];
    }
    internal void LeaveDungeonScene()//離開副本使用
    {
        Destroy(_showMaterial);
        SceneManager.LoadScene("Initialization");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);
    }
}
