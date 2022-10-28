using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Initialization : MonoBehaviour {
  private int _checkLoad = 0;
  private int _checkEquipmentLoad = 0;
  private string _playerAccount;
  private Dictionary<int, PlayerEquipment> _playerEquipments = new Dictionary<int, PlayerEquipment>();
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
      string b = "{\"description\":\"一把樸實無華的武器\",\"image\":\"ipfs://QmcRyo6LNNRRgAXSrdCuFqzZP4HXVhnsGe52uvFFB64oZE\",\"name\":\"\u57fa\u790e\u6b66\u5668\",\"attributes\":[{\"trait_type\":\"rarity\",\"value\":\"common\"},{\"trait_type\":\"atk\",\"value\":100},{\"trait_type\":\"def\",\"value\":100},{\"trait_type\":\"matk\",\"value\":0},{\"trait_type\":\"mdef\",\"value\":0},{\"trait_type\":\"cri\",\"value\":0},{\"trait_type\":\"criDmgRatio\",\"value\":0}]}";
      PlayerEquipment a = PlayerEquipment.CreateEquipment(b);
      _playerEquipments.Add(1, a);
      _playerEquipments.Add(2, a);
      PlayerInfo.PlayerEquipment = _playerEquipments;
      PlayerInfo.EquipmentTokenIds = new string[]{"", "1", "2"};
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

  private void SetEquipment(string equipmentUriList) {
    string[] equipmentUris = equipmentUriList.Split('/');
    int j = 1;
    foreach (string i in equipmentUris) {
      if (i == "") {
        continue;
      }
      int tokenId = int.Parse(PlayerInfo.EquipmentTokenIds[j]);
      string uri = "https://ipfs.io/ipfs/" + i;
      StartCoroutine(GetRequest(uri, tokenId));
      j++;
    }
    StartCoroutine(WaitEquipmentLoad(equipmentUris.Length - 1));
  }

  private void SetEquipmentTokenId(string tokenIds) {
    PlayerInfo.EquipmentTokenIds = tokenIds.Split('/');
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
    yield return new WaitUntil(() => _checkLoad >= 8);
    StartCoroutine(LoadSceneAsync("PlayerInit"));
    StartCoroutine(LoadSceneAsync("Main"));
    StartCoroutine(LoadSceneAsync("HomeMap"));
    StartCoroutine(LoadSceneAsync("DungeonEntryButtonTemp"));
    StartCoroutine(UnLoadSceneAsync("Initialization"));
    StartCoroutine(UnLoadSceneAsync("PlayerInit"));
  }

  private IEnumerator WaitEquipmentLoad(int length) {
    yield return new WaitUntil(() => _checkEquipmentLoad >= length);
    PlayerInfo.PlayerEquipment = _playerEquipments;
    _checkLoad++;
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

  private IEnumerator GetRequest(string uri, int index) {
    using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
      // Request and wait for the desired page.
      yield return webRequest.SendWebRequest();

      string[] pages = uri.Split('/');
      int page = pages.Length - 1;

      switch (webRequest.result) {
        case UnityWebRequest.Result.ConnectionError:
        case UnityWebRequest.Result.DataProcessingError:
          Debug.LogError(pages[page] + ": Error: " + webRequest.error);
          break;
        case UnityWebRequest.Result.ProtocolError:
          Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
          Debug.Log("Retry");
          StartCoroutine(GetRequest(uri, index));
          break;
        case UnityWebRequest.Result.Success:
          string data = webRequest.downloadHandler.text;
          Debug.Log(pages[page] + ":\nReceived: " + data);
          PlayerEquipment playerEquipment = PlayerEquipment.CreateEquipment(data);
          _playerEquipments.Add(index, playerEquipment);
          _checkEquipmentLoad++;
          break;
      }
    }
  }
}
