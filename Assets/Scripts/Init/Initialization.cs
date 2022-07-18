using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour {
  private string _playerAccount;
  [SerializeField] private Animator _fadeOut;
  [DllImport("__Internal")]
  private static extern void IsInited(string playerAccount);
  [DllImport("__Internal")]
  private static extern void LoadSkill(string playerAccount);
  [DllImport("__Internal")]
  private static extern void LoadAbility(string playerAccount);
  [DllImport("__Internal")]
  private static extern void LoadEquipment(string playerAccount);
  [DllImport("__Internal")]
  private static extern void LoadPlayerStatus(string playerAccount);
  [DllImport("__Internal")]
  private static extern void LoadRuby(string playerAccount);
  [DllImport("__Internal")]
  private static extern void LoadSapphire(string playerAccount);
  [DllImport("__Internal")]
  private static extern void LoadEmerald(string playerAccount);

  private void Awake() {
    // Editor 測試用
    string a = "{\"equipments\":[{\"tokenId\":\"1\",\"equipmentStatus\":{\"rarity\":\"1\",\"part\":\"4\",\"level\":\"1\",\"attribute\":[\"5000\",\"100\",\"1000\",\"100\",\"500\",\"6\"],\"skills\":[\"0\",\"0\",\"0\"]}}]}";
    PlayerInfo.PlayerEquipment = PlayerEquipment.CreateEquipment(a);
    PlayerInfo.PlayerStatus = new PlayerStatus{
      name = "111"
    };
    PlayerInfo.PlayerAbility = new PlayerAbility(10, 10, 10, 10, 10);
    PlayerInfo.PlayerAttribute = new PlayerAttribute(PlayerInfo.PlayerAbility, new int[6]);
    StartCoroutine(LoadSceneAsync("PlayerInit"));
    StartCoroutine(LoadSceneAsync("Main"));
    StartCoroutine(LoadSceneAsync("HomeMap"));
    StartCoroutine(LoadSceneAsync("DungeonEntryButtonTemp"));
    StartCoroutine(UnLoadSceneAsync("Initialization"));
    StartCoroutine(UnLoadSceneAsync("PlayerInit"));

    // WebGL 用
    // PlayerInfo.MaterialNum = new MaterialNum {
    //   Ruby = 0,
    //   Sapphire = 0,
    //   Emerald = 0
    // };
    // _playerAccount = PlayerInfo.AccountAddress;
    // IsInited(_playerAccount);
  }

  private void CheckInited(int isInited) {
    if (isInited == 0) {
      // Not inited -> Create
      StartCoroutine(LoadSceneAsync("CreateCharacter"));
    } else {
      // LoadSkill(_playerAccount);
      LoadAbility(_playerAccount);
      LoadEquipment(_playerAccount);
      LoadPlayerStatus(_playerAccount);
      LoadRuby(_playerAccount);
      LoadSapphire(_playerAccount);
      LoadEmerald(_playerAccount);

      StartCoroutine(LoadSceneAsync("PlayerInit"));
      StartCoroutine(LoadSceneAsync("Main"));
      StartCoroutine(LoadSceneAsync("HomeMap"));
      StartCoroutine(LoadSceneAsync("DungeonEntryButtonTemp"));
      StartCoroutine(UnLoadSceneAsync("Initialization"));
      StartCoroutine(UnLoadSceneAsync("PlayerInit"));
    }
  }

  private void SetSkill(string skill) {
    PlayerInfo.PlayerSkills = PlayerSkills.CreateSkills(skill);
  }

  private void SetAbility(string ability) {
    PlayerInfo.PlayerAbility = PlayerAbility.CreateAbility(ability);
    PlayerInfo.PlayerAttribute = new PlayerAttribute(PlayerInfo.PlayerAbility, new int[6]);
  }

  private void SetEquipment(string equipment) {
    PlayerInfo.PlayerEquipment = PlayerEquipment.CreateEquipment(equipment);
  }

  private void SetPlayerStatus(string playerStatus) {
    PlayerInfo.PlayerStatus = PlayerStatus.CreateStatus(playerStatus);
  }

  private void SetRuby(string balanceOfRuby) {
    PlayerInfo.MaterialNum.Ruby = int.Parse(balanceOfRuby);
  }

  private void SetSapphire(string balanceOfSapphire) {
    PlayerInfo.MaterialNum.Sapphire = int.Parse(balanceOfSapphire);
  }

  private void SetEmerald(string balanceOfEmerald) {
    PlayerInfo.MaterialNum.Emerald = int.Parse(balanceOfEmerald);
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
