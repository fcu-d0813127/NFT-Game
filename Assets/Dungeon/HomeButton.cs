using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour {
  private Button _homeButton;
  [SerializeField] GameObject player;
  private void Awake() {
    _homeButton = GetComponent<Button>();

    _homeButton.onClick.AddListener(Load);
        
  }

  private void Load() {
    StartCoroutine(LoadSceneAsync());
  }

  IEnumerator LoadSceneAsync() {
    AsyncOperation asyncLoad =
        SceneManager.LoadSceneAsync("Initialization");
    while (!asyncLoad.isDone) {
      yield return null;
    }
  }
}
