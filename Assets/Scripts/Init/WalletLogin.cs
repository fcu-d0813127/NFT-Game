using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WalletLogin : MonoBehaviour {
  [SerializeField] private Button _loginButton;
  // [DllImport("__Internal")]
  // private static extern void OnLogin();
  // [DllImport("__Internal")]
  // private static extern void EnableChangeAccountReload();

  private void Awake() {
    // LoginButton.onClick.AddListener(OnLogin);
    _loginButton.onClick.AddListener(Load);
  }

  // private void SetPlayerAccount(string account) {
  //   PlayerPrefs.SetString("account", account);
  //   StartCoroutine(LoadSceneAsync());
  //   EnableChangeAccountReload();
  // }

  private void Load() {
    StartCoroutine(LoadSceneAsync());
  }

  IEnumerator LoadSceneAsync() {
    AsyncOperation asyncLoad =
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    while (!asyncLoad.isDone) {
      yield return null;
    }
  }
}
