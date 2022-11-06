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
  [DllImport("__Internal")]
  private static extern void Init(string name);

  private void Awake() {
    _initAnimation.SetActive(false);
    _myselfButton = GetComponent<Button>();
    #if UNITY_WEBGL && !UNITY_EDITOR
      _myselfButton.onClick.AddListener(InitPlayer);
    #endif
    #if UNITY_EDITOR
      _myselfButton.onClick.AddListener(LoadInitScene);
    #endif
  }

  private void InitPlayer() {
    Init(_name.text);
    LoadingSceneController.LoadScene();
  }

  private void LoadInitScene() {
    StartCoroutine(ChangeScene());
  }

  private IEnumerator ChangeScene() {
    _initAnimation.SetActive(true);
    Animator animator = _initAnimation.GetComponent<Animator>();
    animator.SetTrigger("Init");
    yield return new WaitForSeconds(1f);
    LoadingSceneController.UnLoadScene();
    yield return StartCoroutine(LoadSceneAsync("Initialization"));
  }

  private IEnumerator LoadSceneAsync(string sceneName) {
    AsyncOperation asyncLoad =
        SceneManager.LoadSceneAsync(sceneName);
    while (!asyncLoad.isDone) {
      yield return null;
    }
  }

  private void Cancel() {
    LoadingSceneController.UnLoadScene();
  }
}
