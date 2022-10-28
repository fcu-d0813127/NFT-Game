using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadForging : MonoBehaviour {
  private Button _myselfButton;

  private void Awake() {
    _myselfButton = GetComponent<Button>();
    _myselfButton.onClick.AddListener(LoadForgingScene);
  }

  private void LoadForgingScene() {
    PopUpWindowController.Clear();
    StartCoroutine(LoadSceneAsync());
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    Destroy(player);
  }

  private IEnumerator LoadSceneAsync() {
    AsyncOperation asyncLoad =
        SceneManager.LoadSceneAsync("Forging");
    while (!asyncLoad.isDone) {
      yield return null;
    }
  }
}
