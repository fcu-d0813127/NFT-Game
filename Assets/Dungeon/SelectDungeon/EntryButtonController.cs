using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntryButtonController : MonoBehaviour {
  [DllImport("__Internal")]
  private static extern void EntryDungeonSmartContract(int indexOfDungeon);
  private Button _entryButton;

  private void Awake() {
    _entryButton = GetComponent<Button>();
    _entryButton.onClick.AddListener(Entry);
  }

  private void Entry() {
    string dungeonName = OnSelectedDungeon.Name;
    if (dungeonName == null) {
      return;
    }
    string numbersOnly = Regex.Replace(dungeonName, "[^0-9]", "");
    int dungeonIndexOf = int.Parse(numbersOnly) - 1;
    if (dungeonIndexOf > 2) {
      return;
    }
    // WebGL 用
    #if UNITY_WEBGL && !UNITY_EDITOR
      EntryDungeonSmartContract(dungeonIndexOf);
    #endif

    // Editor 測試用
    #if UNITY_EDITOR
      EntryDungeonScene();
    #endif
  }

  private void EntryDungeonScene() {
    string dungeonName = OnSelectedDungeon.Name;
    SceneManager.LoadScene(dungeonName);
    SceneManager.LoadScene("AttackUiPanel", LoadSceneMode.Additive);
  }
}
