using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatusUiDisplay : MonoBehaviour {
  public TMP_Text Name;
  public TMP_Text Level;
  public TMP_Text Exp;
  public TMP_Text DistributableAbility;
  public TMP_Text[] Ability;
  public Canvas PlayerStatus;
  public Button SaveButton;
  private int _tmpDistributableAbility;
  private int[] _originAbility = new int[5];
  private int[] _tmpAbility = new int[5];

  public void UpdateAbility(int index, bool operation) {
    // Not do
    //   The ability value is the same with previous -> can't sub
    //   The distributable ability value is zero -> can't plus
    if (_tmpAbility[index] == _originAbility[index] && !operation ||
        _tmpDistributableAbility == 0 && operation) {
      return;
    }
    // operation: false -> '-', true -> '+'
    if (operation) {
      _tmpDistributableAbility--;
      _tmpAbility[index]++;
    } else {
      _tmpDistributableAbility++;
      _tmpAbility[index]--;
    }
    DisplayAbility();
  }

  private void Awake() {
    PlayerStatus.gameObject.SetActive(false);
    SaveButton.onClick.AddListener(Save);
  }

  private void Update() {
    if (Input.GetKey(KeyCode.S)) {
      PlayerStatus.gameObject.SetActive(true);
      Name.text = $"Name: {PlayerInfo.PlayerStatus.Name}";
      Level.text = $"Level: {PlayerInfo.PlayerStatus.Level}";
      Exp.text = $"EXP: {PlayerInfo.PlayerStatus.Experience}";
      LoadAbility();
      DisplayAbility();
    } else if (Input.GetKey(KeyCode.Escape)) {
      PlayerStatus.gameObject.SetActive(false);
    }
  }

  private void LoadAbility() {
    _tmpDistributableAbility = PlayerInfo.PlayerStatus.DistributableAbility;
    _tmpAbility[0] = PlayerInfo.PlayerAbility.Str;
    _tmpAbility[1] = PlayerInfo.PlayerAbility.Intllegence;
    _tmpAbility[2] = PlayerInfo.PlayerAbility.Dex;
    _tmpAbility[3] = PlayerInfo.PlayerAbility.Vit;
    _tmpAbility[4] = PlayerInfo.PlayerAbility.Luk;
    Array.Copy(_tmpAbility, _originAbility, 5);
  }

  private void DisplayAbility() {
    DistributableAbility.text = $"{_tmpDistributableAbility}";
    Ability[0].text = $"Str: {_tmpAbility[0]}";
    Ability[1].text = $"Intllegence: {_tmpAbility[1]}";
    Ability[2].text = $"Dex: {_tmpAbility[2]}";
    Ability[3].text = $"Vit: {_tmpAbility[3]}";
    Ability[4].text = $"Luk: {_tmpAbility[4]}";
  }

  private void Save() {
    PlayerInfo.PlayerStatus.DistributableAbility = _tmpDistributableAbility;
    PlayerInfo.PlayerAbility.Str = _tmpAbility[0];
    PlayerInfo.PlayerAbility.Intllegence = _tmpAbility[1];
    PlayerInfo.PlayerAbility.Dex = _tmpAbility[2];
    PlayerInfo.PlayerAbility.Vit = _tmpAbility[3];
    PlayerInfo.PlayerAbility.Luk = _tmpAbility[4];
  }
}
