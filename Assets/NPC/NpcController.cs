using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using TMPro;

public class NpcController : MonoBehaviour {
  [DllImport("__Internal")]
  private static extern void exchangeMaterial(string enemyBooty);
  [SerializeField] Vector2 _currentDistance; 
  [SerializeField] float _totalDistance = 10; 
  [SerializeField] float speed = 0.7f;
  public GameObject PrefabOfShowMaterial;
  internal GameObject _showMaterial;
  private void Update() {
    if (GameObject.FindWithTag("Enemy") != null) {
      return;
    }
    _currentDistance += new Vector2(5, 0) * Time.deltaTime;
    if (_currentDistance.x < _totalDistance)
      this.gameObject.transform.Translate(new Vector2(speed, 0) * Time.deltaTime);
  }

  void OnCollisionEnter2D(Collision2D col) {
    GameObject player = GameObject.Find("Player");
    player.GetComponent<PlayerMove>().PlayerMoveAble = false;
    player.GetComponent<PlayerAttack>().AttackAble = false;
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
      LoadingSceneController.LoadScene();
    #endif
  }

  internal void ShowRewardMaterial(string materialOfSend) {   
    string[] materialOfGain = materialOfSend.Split(',');
    _showMaterial = Instantiate(PrefabOfShowMaterial);
    _showMaterial.transform.SetParent(GameObject.Find("Canvas").transform, false);
    _showMaterial.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "獲得以下素材" + 
    "\n紅寶石:" + materialOfGain[0] + "\n藍寶石:" + materialOfGain[1] + 
    "\n綠寶石:" + materialOfGain[2] + "\n遊戲幣:" + materialOfGain[3];
    #if UNITY_WEBGL && !UNITY_EDITOR
      LoadingSceneController.UnLoadScene();
    #endif
  }

  //離開副本使用
  internal void LeaveDungeonScene() {
    Destroy(_showMaterial);
    SceneManager.LoadScene("Initialization");
  }

  private void Cancel() {
    LoadingSceneController.UnLoadScene();
  }
}
