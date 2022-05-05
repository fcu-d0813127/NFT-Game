using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WalletLogin : MonoBehaviour {
  public Button LoginButton;
  [DllImport("__Internal")]
  private static extern void OnLogin();
  [DllImport("__Internal")]
  private static extern void EnableChangeAccountReload();

  private void Awake() {
    LoginButton.onClick.AddListener(OnLogin);
  }

  private void SetPlayerAccount(string account) {
    PlayerPrefs.SetString("account", account);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    EnableChangeAccountReload();
  }
}
