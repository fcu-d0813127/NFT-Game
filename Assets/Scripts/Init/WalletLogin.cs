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
    _loginButton.onClick.AddListener(OnLogin);
    _loginButton.onClick.AddListener(Load);
  }

   private void SetPlayerAccount(string account) {
     PlayerPrefs.SetString("account", account);
     StartCoroutine(LoadSceneAsync());
     EnableChangeAccountReload();
   }

  private void Load() {
    StartCoroutine(PlayAnimation());
  }

  private IEnumerator PlayAnimation() {
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
