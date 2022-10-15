using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RefreshEquipment : MonoBehaviour {
  [DllImport("__Internal")]
  private static extern void LoadEquipment(string playerAccount, int isInit);
  private int _checkEquipmentLoad = 0;
  private Button _refreshEquipmentButton;
  private List<PlayerEquipment> _playerEquipments = new List<PlayerEquipment>();

  private void Awake() {
    _refreshEquipmentButton = GetComponent<Button>();
    _refreshEquipmentButton.onClick.AddListener(Refresh);
  }

  private void Refresh() {
    #if UNITY_WEBGL && !UNITY_EDITOR
      LoadEquipment(PlayerInfo.AccountAddress, 0);
    #endif
    #if UNITY_EDITOR
      RefreshBackpack();
    #endif
  }

  private void SetEquipment(string equipmentUriList) {
    string[] equipmentUris = equipmentUriList.Split('/');
    foreach (string i in equipmentUris) {
      string uri = "https://ipfs.io/ipfs/" + i;
      StartCoroutine(GetRequest(uri));
    }
    StartCoroutine(WaitEquipmentLoad(equipmentUris.Length));
  }

  private void RefreshBackpack() {
    PlayerInventoryHolder.Instance.ResetEquipmentBackpack();
  }

  private IEnumerator WaitEquipmentLoad(int length) {
    yield return new WaitUntil(() => _checkEquipmentLoad >= length);
    PlayerInfo.PlayerEquipment = _playerEquipments;
    RefreshBackpack();
  }

  private IEnumerator GetRequest(string uri) {
    using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
      // Request and wait for the desired page.
      yield return webRequest.SendWebRequest();

      string[] pages = uri.Split('/');
      int page = pages.Length - 1;

      switch (webRequest.result) {
        case UnityWebRequest.Result.ConnectionError:
        case UnityWebRequest.Result.DataProcessingError:
          Debug.LogError(pages[page] + ": Error: " + webRequest.error);
          break;
        case UnityWebRequest.Result.ProtocolError:
          Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
          break;
        case UnityWebRequest.Result.Success:
          string data = webRequest.downloadHandler.text;
          Debug.Log(pages[page] + ":\nReceived: " + data);
          PlayerEquipment playerEquipment = PlayerEquipment.CreateEquipment(data);
          _playerEquipments.Add(playerEquipment);
          _checkEquipmentLoad++;
          break;
      }
    }
  }
}
