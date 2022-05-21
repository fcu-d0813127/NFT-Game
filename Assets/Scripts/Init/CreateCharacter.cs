using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CreateCharacter : MonoBehaviour {
  [SerializeField] private TMP_InputField _name;
  [DllImport("__Internal")]
  private static extern void Init(string name, int career);

  private void InitPlayer() {
    Init(_name.text, 0);
  }

  private void LoadInitScene() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
  }
}
