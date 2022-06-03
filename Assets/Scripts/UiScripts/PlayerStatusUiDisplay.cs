using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerStatusUiDisplay : MonoBehaviour {
  private int _tmpDistributableAbility;
  private int[] _originAbility = new int[5];
  private int[] _tmpAbility = new int[5];
  [SerializeField] private TMP_Text _name;
  [SerializeField] private TMP_Text _level;
  [SerializeField] private TMP_Text _exp;
  [SerializeField] private TMP_Text _distributableAbility;
  [SerializeField] private TMP_Text[] _ability;
  [SerializeField] private Canvas _playerStatus;
  [SerializeField] private Button _saveButton;

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
    _playerStatus.gameObject.SetActive(false);
    _saveButton.onClick.AddListener(Save);
  }

  private void Update() {
    if (Keyboard.current.sKey.isPressed && PopUpWindowController.IsBackpackOpen == false) {
      PopUpWindowController.IsPlayerStatusOpen = true;
      _playerStatus.gameObject.SetActive(true);
      _name.text = PlayerInfo.PlayerStatus.Name;
      _level.text = PlayerInfo.PlayerStatus.Level.ToString();
      _exp.text = PlayerInfo.PlayerStatus.Experience.ToString();
      LoadAbility();
      DisplayAbility();
    } else if (Keyboard.current.escapeKey.isPressed) {
      PopUpWindowController.IsPlayerStatusOpen = false;
      _playerStatus.gameObject.SetActive(false);
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
    _distributableAbility.text = $"{_tmpDistributableAbility}";
    _ability[0].text = _tmpAbility[0].ToString();
    _ability[1].text = _tmpAbility[1].ToString();
    _ability[2].text = _tmpAbility[2].ToString();
    _ability[3].text = _tmpAbility[3].ToString();
    _ability[4].text = _tmpAbility[4].ToString();
  }

  private void Save() {
    PlayerInfo.PlayerStatus.DistributableAbility = _tmpDistributableAbility;
    PlayerInfo.PlayerAbility.Str = _tmpAbility[0];
    PlayerInfo.PlayerAbility.Intllegence = _tmpAbility[1];
    PlayerInfo.PlayerAbility.Dex = _tmpAbility[2];
    PlayerInfo.PlayerAbility.Vit = _tmpAbility[3];
    PlayerInfo.PlayerAbility.Luk = _tmpAbility[4];
    LoadAbility();
    DisplayAbility();
  }
}
