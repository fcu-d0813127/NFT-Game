using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DungeonButton : MonoBehaviour {
  private Button _myselfButton;
  private TMP_Text _nowSelectDungeon;

  private void Start() {
    _myselfButton = GetComponent<Button>();
    _nowSelectDungeon = GameObject.Find("message").GetComponent<TMP_Text>();
    _myselfButton?.onClick.AddListener(ButtonHandler);
  }
  private void ButtonHandler() {
    string name = this.gameObject.name;
    if (name == "Exit") {
      StartCoroutine(LoadSceneAsync());
    } else if (name == "Entry") {
      Debug.Log("You entry " + _nowSelectDungeon.text);
    } else {
      _nowSelectDungeon.text = name;
    }
  }

  IEnumerator LoadSceneAsync() {
    AsyncOperation asyncLoad =
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    while (!asyncLoad.isDone) {
      yield return null;
    }
  }
}