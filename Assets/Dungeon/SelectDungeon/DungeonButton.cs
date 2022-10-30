using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
public class DungeonButton : MonoBehaviour {
  [DllImport("__Internal")]
  private static extern void dungeonOf(int indexOfDungeon);
  private Button _myselfButton;
  private TMP_Text _nowSelectDungeon;
  private Image _dungeonPreview;
  [SerializeField] private GameObject _fadeIn;
  [SerializeField] private TMP_Text _displayName;

  private void Start() {
    _myselfButton = GetComponent<Button>();
    _nowSelectDungeon = GameObject.Find("DisplayName").GetComponent<TMP_Text>();
    _dungeonPreview = GameObject.Find("PreviewDungeon").GetComponent<Image>();
    _dungeonPreview.color = Color.clear;
    _myselfButton?.onClick.AddListener(ButtonHandler);
  }
  private void ButtonHandler() {
    string name = this.gameObject.name;
    if (name == "Exit") {
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      Destroy(player);
      StartCoroutine(LoadSceneAsync("Initialization"));
    } else {
      GameObject outline = transform.Find("Outline").gameObject;
      if (OnSelectedDungeon.Outline != null) {
        OnSelectedDungeon.Outline.SetActive(false);
      }
      outline.SetActive(true);
      OnSelectedDungeon.Outline = outline;
      _nowSelectDungeon.text = _displayName.text;
      OnSelectedDungeon.Name = name;
      _dungeonPreview.color = Color.white;
      _dungeonPreview.sprite = Resources.Load<Sprite>($"DungeonPreviewImages/{name}Preview");
      #if UNITY_EDITOR
      #endif
      #if UNITY_WEBGL && !UNITY_EDITOR
        if (name.Substring(0, 6) == "Button") {//排除前3副本以外
          
        } else {
          dungeonOf(Convert.ToInt32(name.Substring(7)) - 1);
        }
      #endif
    }
  }

  private IEnumerator LoadSceneAsync(string sceneName) {
    _fadeIn?.SetActive(true);
    Animator animator = _fadeIn.GetComponent<Animator>();
    animator.SetTrigger("Init");
    yield return new WaitForSeconds(1f);
    AsyncOperation asyncLoad =
        SceneManager.LoadSceneAsync(sceneName);
    while (!asyncLoad.isDone) {
      yield return null;
    }
  }
}