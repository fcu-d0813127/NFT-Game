using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour {
  private int _checkLoad = 0;
  private string _playerAccount;
  [SerializeField] private Animator _fadeOut;
  [DllImport("__Internal")]
  private static extern void IsInited(string playerAccount);
  [DllImport("__Internal")]
  private static extern void LoadSkill(string playerAccount);
  [DllImport("__Internal")]
  private static extern void LoadAbility(string playerAccount);
  [DllImport("__Internal")]
  private static extern void LoadEquipment(string playerAccount, int isInit);
  [DllImport("__Internal")]
  private static extern void LoadEquip(string playerAccount);
  [DllImport("__Internal")]
  private static extern void LoadPlayerStatus(string playerAccount);
  [DllImport("__Internal")]
  private static extern void LoadRuby(string playerAccount);
  [DllImport("__Internal")]
  private static extern void LoadSapphire(string playerAccount);
  [DllImport("__Internal")]
  private static extern void LoadEmerald(string playerAccount);

  private void Awake() {
    if (PlayerInfo.EquipEquipments == null) {
      PlayerInfo.EquipEquipments = new int[5];
    }
    PlayerInfo.EquipAttribute = new Attribute();
    // Editor 測試用
    #if UNITY_EDITOR
      PlayerInfo.MaterialNum = new MaterialNum {
        Ruby = 250,
        Sapphire = 250,
        Emerald = 250
      };
      string a = "{\"equipments\":[{\"tokenId\":\"1\",\"equipmentStatus\":{\"rarity\":\"1\",\"part\":\"4\",\"level\":\"1\",\"attribute\":[\"5000\",\"100\",\"1000\",\"100\",\"500\",\"6\"],\"skills\":[\"0\",\"0\",\"0\"]}},{\"tokenId\":\"2\",\"equipmentStatus\":{\"rarity\":\"1\",\"part\":\"4\",\"level\":\"1\",\"attribute\":[\"5000\",\"100\",\"1000\",\"100\",\"500\",\"6\"],\"skills\":[\"0\",\"0\",\"0\"]}}]}";
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
    #endif

    // WebGL 用
    #if UNITY_WEBGL && !UNITY_EDITOR
      PlayerInfo.EquipAttribute = new Attribute();
      PlayerInfo.MaterialNum = new MaterialNum {
        Ruby = 0,
        Sapphire = 0,
        Emerald = 0
      };
      _playerAccount = PlayerInfo.AccountAddress;
      IsInited(_playerAccount);
    #endif
  }

  private void CheckInited(int isInited) {
    if (isInited == 0) {
      // Not inited -> Create
      StartCoroutine(LoadSceneAsync("CreateCharacter"));
    } else {
      // LoadSkill(_playerAccount);
      LoadAbility(_playerAccount);
      LoadEquipment(_playerAccount, 1);
      LoadEquip(_playerAccount);
      LoadPlayerStatus(_playerAccount);
      LoadRuby(_playerAccount);
      LoadSapphire(_playerAccount);
      LoadEmerald(_playerAccount);

      StartCoroutine(WaitDataLoad());
    }
  }

  private void SetSkill(string skill) {
    // PlayerInfo.PlayerSkills = PlayerSkills.CreateSkills(skill);
  }

  private void SetAbility(string ability) {
    PlayerInfo.PlayerAbility = PlayerAbility.CreateAbility(ability);
    PlayerInfo.PlayerAttribute = new PlayerAttribute(PlayerInfo.PlayerAbility, new int[6]);
    _checkLoad++;
  }

  private void SetEquipment(string equipment) {
    PlayerInfo.PlayerEquipment = PlayerEquipment.CreateEquipment(equipment);
    _checkLoad++;
  }

  private void SetEquips(string equips) {
    PlayerInfo.EquipEquipments = PlayerEquips.CreateEquips(equips);
    _checkLoad++;
  }

  private void SetPlayerStatus(string playerStatus) {
    PlayerInfo.PlayerStatus = PlayerStatus.CreateStatus(playerStatus);
    _checkLoad++;
  }

  private void SetRuby(string balanceOfRuby) {
    PlayerInfo.MaterialNum.Ruby = int.Parse(balanceOfRuby);
    _checkLoad++;
  }

  private void SetSapphire(string balanceOfSapphire) {
    PlayerInfo.MaterialNum.Sapphire = int.Parse(balanceOfSapphire);
    _checkLoad++;
  }

  private void SetEmerald(string balanceOfEmerald) {
    PlayerInfo.MaterialNum.Emerald = int.Parse(balanceOfEmerald);
    _checkLoad++;
  }

  private IEnumerator WaitDataLoad() {
    yield return new WaitUntil(() => _checkLoad >= 7);
    StartCoroutine(LoadSceneAsync("PlayerInit"));
    StartCoroutine(LoadSceneAsync("Main"));
    StartCoroutine(LoadSceneAsync("HomeMap"));
    StartCoroutine(LoadSceneAsync("DungeonEntryButtonTemp"));
    StartCoroutine(UnLoadSceneAsync("Initialization"));
    StartCoroutine(UnLoadSceneAsync("PlayerInit"));
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
