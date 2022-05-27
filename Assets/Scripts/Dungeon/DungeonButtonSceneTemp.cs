using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DungeonButtonSceneTemp : MonoBehaviour {
  private Button _dungeonButton;

  private void Awake() {
    _dungeonButton = GetComponent<Button>();
    _dungeonButton.onClick.AddListener(Load);
  }

  private void Load() {
    StartCoroutine(LoadSceneAsync());
  }

  IEnumerator LoadSceneAsync() {
    AsyncOperation asyncLoad =
        SceneManager.LoadSceneAsync("EntryDungeon");
    while (!asyncLoad.isDone) {
      yield return null;
    }
  }
}
