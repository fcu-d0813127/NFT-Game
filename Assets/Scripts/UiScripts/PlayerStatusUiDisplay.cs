using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerStatusUiDisplay : MonoBehaviour {
  private int _tmpDistributableAbility;
  private int[] _originAbility = new int[5];
  private int[] _tmpAbility = new int[5];
  private int[] _tmpAttribute = new int[6];
  [SerializeField] private TMP_Text _name;
  [SerializeField] private TMP_Text _level;
  [SerializeField] private TMP_Text _exp;
  [SerializeField] private TMP_Text _distributableAbility;
  [SerializeField] private TMP_Text[] _ability;
  [SerializeField] private TMP_Text[] _attribute;
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
    UpdateAttribute();
  }

  private void Awake() {
    _playerStatus.gameObject.SetActive(false);
    _saveButton.onClick.AddListener(Save);
  }

  private void Update() {
    if (Keyboard.current.sKey.isPressed && PopUpWindowController.IsBackpackOpen == false) {
      PopUpWindowController.IsPlayerStatusOpen = true;
      _playerStatus.gameObject.SetActive(true);
      _name.text = PlayerInfo.PlayerStatus.name;
      _level.text = PlayerInfo.PlayerStatus.level.ToString();
      _exp.text = PlayerInfo.PlayerStatus.experience.ToString();
      LoadAbility();
      DisplayAbility();
      UpdateAttribute();
    } else if (Keyboard.current.qKey.isPressed) {
      PopUpWindowController.IsPlayerStatusOpen = false;
      _playerStatus.gameObject.SetActive(false);
    }
  }

  private void LoadAbility() {
    _tmpDistributableAbility = PlayerInfo.PlayerStatus.distributableAbility;
    _tmpAbility[0] = PlayerInfo.PlayerAbility.str;
    _tmpAbility[1] = PlayerInfo.PlayerAbility.intllegence;
    _tmpAbility[2] = PlayerInfo.PlayerAbility.dex;
    _tmpAbility[3] = PlayerInfo.PlayerAbility.vit;
    _tmpAbility[4] = PlayerInfo.PlayerAbility.luk;
    Array.Copy(_tmpAbility, _originAbility, 5);
  }

  private void DisplayAbility() {
    _distributableAbility.text = $"{_tmpDistributableAbility}";
    for (int i = 0; i < 5; i++) {
      _ability[i].text = _tmpAbility[i].ToString();
    }
  }

  private void UpdateAttribute() {
    PlayerAbility playerAbility = new PlayerAbility(
      _tmpAbility[0], _tmpAbility[1], _tmpAbility[2], _tmpAbility[3], _tmpAbility[4]
    );
    PlayerAttribute playerAttribute = new PlayerAttribute(playerAbility, new int[6]);
    _tmpAttribute[0] = (int)(playerAttribute.Atk + PlayerInfo.EquipAttribute.Atk);
    _tmpAttribute[1] = (int)(playerAttribute.Matk + PlayerInfo.EquipAttribute.Matk);
    _tmpAttribute[2] = (int)(playerAttribute.Def + PlayerInfo.EquipAttribute.Def);
    _tmpAttribute[3] = (int)(playerAttribute.Mdef + PlayerInfo.EquipAttribute.Mdef);
    _tmpAttribute[4] = (int)((playerAttribute.Cri + PlayerInfo.EquipAttribute.Cri) * 100);
    _tmpAttribute[5] = (int)((playerAttribute.CriDmgRatio + PlayerInfo.EquipAttribute.CriDmgRatio) *
        100);
    DisplayAttribute();
  }

  private void DisplayAttribute() {
    for (int i = 0; i < 4; i++) {
      _attribute[i].text = _tmpAttribute[i].ToString();
    }
    _attribute[4].text = $"{_tmpAttribute[4]}%";
    _attribute[5].text = $"{_tmpAttribute[5]}%";
  }

  private void Save() {
    PlayerInfo.PlayerStatus.distributableAbility = _tmpDistributableAbility;
    PlayerAbility playerAbility = new PlayerAbility(
      _tmpAbility[0], _tmpAbility[1], _tmpAbility[2], _tmpAbility[3], _tmpAbility[4]
    );
    PlayerAttribute playerAttribute = new PlayerAttribute(playerAbility, new int[6]);
    PlayerInfo.PlayerAbility = playerAbility;
    PlayerInfo.PlayerAttribute = playerAttribute;
    LoadAbility();
    DisplayAbility();
  }
}
