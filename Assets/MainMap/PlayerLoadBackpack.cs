using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLoadBackpack : MonoBehaviour {
  private void Awake() {
    StartCoroutine(LoadSceneAsync());
  }

  IEnumerator LoadSceneAsync() {
    AsyncOperation asyncLoad =
        SceneManager.LoadSceneAsync("Backpack", LoadSceneMode.Additive);
    while (!asyncLoad.isDone) {
      yield return null;
    }
  }
}
