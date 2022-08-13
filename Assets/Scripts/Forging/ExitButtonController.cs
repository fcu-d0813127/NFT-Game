using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitButtonController : MonoBehaviour {
  private Button _exitButton;

  private void Awake() {
    _exitButton = GetComponent<Button>();
    _exitButton.onClick.AddListener(Exit);
  }

  private void Exit() {
    StartCoroutine(LoadSceneAsync("Initialization"));
  }

  private IEnumerator LoadSceneAsync(string sceneName) {
    AsyncOperation asyncLoad =
        SceneManager.LoadSceneAsync(sceneName);
    while (!asyncLoad.isDone) {
      yield return null;
    }
  }
}
