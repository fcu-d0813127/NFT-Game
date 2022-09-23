using UnityEngine;
using UnityEngine.UI;

public class MarketOpenPage : MonoBehaviour {
  private Button _myselfButton;

  private void Awake() {
    _myselfButton = GetComponent<Button>();
    _myselfButton.onClick.AddListener(OpenPage);
  }

  private void OpenPage() {
    string buttonName = this.gameObject.name;
    if (buttonName == "MyItems") {
      Application.OpenURL("https://testnets.opensea.io/zh-TW/account/test-hdeghz4b7b?search[resultModel]=ASSETS&search[sortBy]=LAST_TRANSFER_DATE");
    } else if (buttonName == "MarketList") {
      Application.OpenURL("https://testnets.opensea.io/zh-TW/collection/test-hdeghz4b7b");
    }
  }
}
