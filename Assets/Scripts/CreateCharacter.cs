using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CreateCharacter : MonoBehaviour {
  public Button CreateButton;
  public TMP_InputField Name;
  [DllImport("__Internal")]
  private static extern void Init(string name, int career);

  private void Awake() {
    CreateButton.onClick.AddListener(InitPlayer);
  }

  private void InitPlayer() {
    Init(Name.text, 0);
  }

  private void LoadInitScene() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
  }
}
