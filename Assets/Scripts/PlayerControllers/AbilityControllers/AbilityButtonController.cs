using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonController : MonoBehaviour {
  public Button[] PlusButtons;
  public Button[] SubButtons;
  public PlayerStatusUiDisplay PlayerClick;

  private void Awake() {
    PlusButtons[0].onClick.AddListener(PlusStr);
    PlusButtons[1].onClick.AddListener(PlusIntllegence);
    PlusButtons[2].onClick.AddListener(PlusDex);
    PlusButtons[3].onClick.AddListener(PlusVit);
    PlusButtons[4].onClick.AddListener(PlusLuk);
    SubButtons[0].onClick.AddListener(SubStr);
    SubButtons[1].onClick.AddListener(SubIntllegence);
    SubButtons[2].onClick.AddListener(SubDex);
    SubButtons[3].onClick.AddListener(SubVit);
    SubButtons[4].onClick.AddListener(SubLuk);
  }

  private void PlusStr() {
    PlayerClick.UpdateAbility(0, true);
  }

  private void PlusIntllegence() {
    PlayerClick.UpdateAbility(1, true);
  }

  private void PlusDex() {
    PlayerClick.UpdateAbility(2, true);
  }

  private void PlusVit() {
    PlayerClick.UpdateAbility(3, true);
  }

  private void PlusLuk() {
    PlayerClick.UpdateAbility(4, true);
  }

  private void SubStr() {
    PlayerClick.UpdateAbility(0, false);
  }

  private void SubIntllegence() {
    PlayerClick.UpdateAbility(1, false);
  }

  private void SubDex() {
    PlayerClick.UpdateAbility(2, false);
  }

  private void SubVit() {
    PlayerClick.UpdateAbility(3, false);
  }

  private void SubLuk() {
    PlayerClick.UpdateAbility(4, false);
  }
}
