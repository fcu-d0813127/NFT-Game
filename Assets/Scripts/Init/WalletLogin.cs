using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WalletLogin : MonoBehaviour {
  private Button _loginButton;
  [SerializeField] private Animator _loginAnimation;
  [DllImport("__Internal")]
  private static extern void OnLogin();
  [DllImport("__Internal")]
  private static extern void EnableChangeAccountReload();

  private void Awake() {
    _loginButton = GetComponent<Button>();
    _loginButton.onClick.AddListener(OnLogin);  // WebGL 用
    // _loginButton.onClick.AddListener(Load);  // Editor 測試用
  }

  // WebGL 用
  private void SetPlayerAccount(string account) {
    PlayerInfo.AccountAddress = account;
    StartCoroutine(ChangeScene());
    EnableChangeAccountReload();
  }

  // Editor 測試用
  private void Load() {
    StartCoroutine(ChangeScene());
  }

  private IEnumerator ChangeScene() {
    _loginAnimation.SetTrigger("Create");
    yield return new WaitForSeconds(1f);
    yield return StartCoroutine(LoadSceneAsync());
  }

  private IEnumerator LoadSceneAsync() {
    AsyncOperation asyncLoad =
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    while (!asyncLoad.isDone) {
      yield return null;
    }
  }
}
