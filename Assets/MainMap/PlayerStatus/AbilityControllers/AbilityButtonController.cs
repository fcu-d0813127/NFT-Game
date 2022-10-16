using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonController : MonoBehaviour {
  [SerializeField] private Button[] _plusButtons;
  [SerializeField] private Button[] _subButtons;
  [SerializeField] private PlayerStatusUiDisplay _playerClick;

  private void Awake() {
    _plusButtons[0].onClick.AddListener(PlusStr);
    _plusButtons[1].onClick.AddListener(PlusIntllegence);
    _plusButtons[2].onClick.AddListener(PlusDex);
    _plusButtons[3].onClick.AddListener(PlusVit);
    _plusButtons[4].onClick.AddListener(PlusLuk);
    _subButtons[0].onClick.AddListener(SubStr);
    _subButtons[1].onClick.AddListener(SubIntllegence);
    _subButtons[2].onClick.AddListener(SubDex);
    _subButtons[3].onClick.AddListener(SubVit);
    _subButtons[4].onClick.AddListener(SubLuk);
  }

  private void PlusStr() {
    _playerClick.UpdateAbility(0, true);
  }

  private void PlusIntllegence() {
    _playerClick.UpdateAbility(1, true);
  }

  private void PlusDex() {
    _playerClick.UpdateAbility(2, true);
  }

  private void PlusVit() {
    _playerClick.UpdateAbility(3, true);
  }

  private void PlusLuk() {
    _playerClick.UpdateAbility(4, true);
  }

  private void SubStr() {
    _playerClick.UpdateAbility(0, false);
  }

  private void SubIntllegence() {
    _playerClick.UpdateAbility(1, false);
  }

  private void SubDex() {
    _playerClick.UpdateAbility(2, false);
  }

  private void SubVit() {
    _playerClick.UpdateAbility(3, false);
  }

  private void SubLuk() {
    _playerClick.UpdateAbility(4, false);
  }
}
