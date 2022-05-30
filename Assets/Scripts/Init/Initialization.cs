using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour {
  private string _playerAccount;
  [SerializeField] private Animator _fadeOut;
  // [DllImport("__Internal")]
  // private static extern void IsInited(string playerAccount);
  // [DllImport("__Internal")]
  // private static extern void LoadSkill(string playerAccount);
  // [DllImport("__Internal")]
  // private static extern void LoadAbility(string playerAccount);
  // [DllImport("__Internal")]
  // private static extern void LoadEquipment(string playerAccount);
  // [DllImport("__Internal")]
  // private static extern void LoadPlayerStatus(string playerAccount);

  private void Awake() {
    // _playerAccount = PlayerPrefs.GetString("account");
    // IsInited(_playerAccount);
    string tempName = PlayerInfo.TempName;
    PlayerInfo.PlayerStatus = new PlayerStatus(tempName, 1, 1, 5, 1, 1);
    PlayerInfo.PlayerAbility = new PlayerAbility(10, 10, 10, 10, 10);
    StartCoroutine(LoadSceneAsync("Main"));
    StartCoroutine(LoadSceneAsync("HomeMap"));
    StartCoroutine(LoadSceneAsync("DungeonEntryButtonTemp"));
    StartCoroutine(UnLoadSceneAsync("Initialization"));
  }

  private void CheckInited(int isInited) {
    // if (isInited == 0) {
    //   // Not inited -> Create
    //   SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    // } else {
    //   LoadSkill(_playerAccount);
    //   LoadAbility(_playerAccount);
    //   LoadEquipment(_playerAccount);
    //   LoadPlayerStatus(_playerAccount);
    // }
  }

  private void SetSkill(string skill) {
    PlayerInfo.PlayerSkills = PlayerSkills.CreateSkills(skill);
  }

  private void SetAbility(string ability) {
    PlayerInfo.PlayerAbility = PlayerAbility.CreateAbility(ability);
  }

  private void SetEquipment(string equipment) {
    PlayerInfo.PlayerEquipment = PlayerEquipment.CreateEquipment(equipment);
  }

  private void SetPlayerStatus(string playerStatus) {
    PlayerInfo.PlayerStatus = PlayerStatus.CreateStatus(playerStatus);
  }

  private IEnumerator UnLoadSceneAsync(string sceneName) {
    _fadeOut.SetTrigger("Start");
    yield return new WaitForSeconds(1f);
    SceneManager.UnloadSceneAsync(sceneName);
  }

  private IEnumerator LoadSceneAsync(string sceneName) {
    AsyncOperation asyncLoad =
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    while (!asyncLoad.isDone) {
      yield return null;
    }
  }
}
