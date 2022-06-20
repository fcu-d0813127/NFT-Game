using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerStatusUiDisplay : MonoBehaviour {
  private int _tmpDistributableAbility;
  private int[] _originAbility = new int[5];
  private int[] _tmpAbility = new int[5];
  private int[] _tmpPanel = new int[6];
  [SerializeField] private TMP_Text _name;
  [SerializeField] private TMP_Text _level;
  [SerializeField] private TMP_Text _exp;
  [SerializeField] private TMP_Text _distributableAbility;
  [SerializeField] private TMP_Text[] _ability;
  [SerializeField] private TMP_Text[] _panel;
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
    UpdatePanel();
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
      UpdatePanel();
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
    for (int i = 0; i < 5; i++) {
      _ability[i].text = _tmpAbility[i].ToString();
    }
  }

  private void UpdatePanel() {
    PlayerAbility playerAbility = new PlayerAbility(
      _tmpAbility[0], _tmpAbility[1], _tmpAbility[2], _tmpAbility[3], _tmpAbility[4]
    );
    PlayerPanel playerPanel = new PlayerPanel(playerAbility, new int[6]);
    _tmpPanel[0] = (int)playerPanel.Atk;
    _tmpPanel[1] = (int)playerPanel.Matk;
    _tmpPanel[2] = (int)playerPanel.Def;
    _tmpPanel[3] = (int)playerPanel.Mdef;
    _tmpPanel[4] = (int)playerPanel.Cri;
    _tmpPanel[5] = (int)(playerPanel.CriDmgRatio * 100);
    DisplayPanel();
  }

  private void DisplayPanel() {
    for (int i = 0; i < 5; i++) {
      _panel[i].text = _tmpPanel[i].ToString();
    }
    _panel[5].text = $"{_tmpPanel[5]}%";
  }

  private void Save() {
    PlayerInfo.PlayerStatus.DistributableAbility = _tmpDistributableAbility;
    PlayerAbility playerAbility = new PlayerAbility(
      _tmpAbility[0], _tmpAbility[1], _tmpAbility[2], _tmpAbility[3], _tmpAbility[4]
    );
    PlayerPanel playerPanel = new PlayerPanel(playerAbility, new int[6]);
    PlayerInfo.PlayerAbility = playerAbility;
    PlayerInfo.PlayerPanel = playerPanel;
    LoadAbility();
    DisplayAbility();
  }
}
