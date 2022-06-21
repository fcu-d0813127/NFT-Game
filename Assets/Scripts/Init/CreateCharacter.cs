using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CreateCharacter : MonoBehaviour {
  private Button _myselfButton;
  [SerializeField] private GameObject _initAnimation;
  [SerializeField] private TMP_InputField _name;
  // [DllImport("__Internal")]
  // private static extern void Init(string name, int career);

  private void Awake() {
    _initAnimation.SetActive(false);
    _myselfButton = GetComponent<Button>();
    _myselfButton.onClick.AddListener(InitPlayer);
  }

  private void InitPlayer() {
    // Init(_name.text, 0);
    PlayerInfo.Name = _name.text;
    StartCoroutine(PlayAnimation());
  }

  private void LoadInitScene() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
  }

  private IEnumerator PlayAnimation() {
    _initAnimation.SetActive(true);
    Animator animator = _initAnimation.GetComponent<Animator>();
    animator.SetTrigger("Init");
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
