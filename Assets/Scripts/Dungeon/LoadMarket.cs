using UnityEngine;
using UnityEngine.UI;

public class LoadMarket : MonoBehaviour {
  private Button _dungeonButton;
  [SerializeField] private Animation _myItems;
  [SerializeField] private Animation _marketList;

  private void Awake() {
    _dungeonButton = GetComponent<Button>();
    _dungeonButton.onClick.AddListener(OpenButtons);
  }

  private void OpenButtons() {
    _myItems.Play("MyItems");
    _marketList.Play("MarketList");
  }
}
