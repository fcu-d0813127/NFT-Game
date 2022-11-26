using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;

public class CoinController : MonoBehaviour {
  [DllImport("__Internal")]
  private static extern void GetMyCoin();
  [SerializeField] private TMP_Text _myCoin;
  [SerializeField] private TMP_Text _dungeonCost;

  private void Awake() {
    #if UNITY_WEBGL && !UNITY_EDITOR
      GetMyCoin();
    #endif
  }

  private void SetMyCoin(string myCoin) {
    _myCoin.text = myCoin;
  }

  private void SetDungeonCost(string dungeonCost) {
    _dungeonCost.text = dungeonCost;
  }
}
