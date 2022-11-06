using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RefreshEquipment : MonoBehaviour {
  [DllImport("__Internal")]
  private static extern void LoadEquipment(string playerAccount, int isInit);
  [SerializeField] private MouseItemData _mouseItem;
  private int _checkEquipmentLoad;
  private Button _refreshEquipmentButton;
  private Dictionary<int, PlayerEquipment> _playerEquipments = new Dictionary<int, PlayerEquipment>();

  private void Awake() {
    _refreshEquipmentButton = GetComponent<Button>();
    _refreshEquipmentButton.onClick.AddListener(Refresh);
  }

  private void Refresh() {
    if (_mouseItem.AssignedInventorySlot.ItemData != null) {
      _mouseItem.ClearSlot();
    }
    _playerEquipments.Clear();
    _checkEquipmentLoad = 0;
    #if UNITY_WEBGL && !UNITY_EDITOR
      LoadEquipment(PlayerInfo.AccountAddress, 0);
      LoadingSceneController.LoadScene();
    #endif
    #if UNITY_EDITOR
      RefreshBackpack();
    #endif
  }

  private void SetEquipmentTokenId(string tokenIds) {
    PlayerInfo.EquipmentTokenIds = tokenIds.Split('/');
  }

  private void SetEquipment(string equipmentUriList) {
    string[] equipmentUris = equipmentUriList.Split('/');
    int j = 1;
    foreach (string i in equipmentUris) {
      if (i == "") {
        continue;
      }
      int tokenId = int.Parse(PlayerInfo.EquipmentTokenIds[j]);
      string uri = "https://ipfs.io/ipfs/" + i;
      StartCoroutine(GetRequest(uri, tokenId));
      j++;
    }
    StartCoroutine(WaitEquipmentLoad(equipmentUris.Length - 1));
  }

  private void RefreshBackpack() {
    PlayerInventoryHolder.Instance.ResetEquipmentBackpack();
  }

  private IEnumerator WaitEquipmentLoad(int length) {
    yield return new WaitUntil(() => _checkEquipmentLoad >= length);
    PlayerInfo.PlayerEquipment = _playerEquipments;
    RefreshBackpack();
    LoadingSceneController.UnLoadScene();
  }

  private IEnumerator GetRequest(string uri, int tokenId) {
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
          Debug.Log("Retry");
          StartCoroutine(GetRequest(uri, tokenId));
          break;
        case UnityWebRequest.Result.Success:
          string data = webRequest.downloadHandler.text;
          Debug.Log(pages[page] + ":\nReceived: " + data);
          PlayerEquipment playerEquipment = PlayerEquipment.CreateEquipment(data);
          _playerEquipments.Add(tokenId, playerEquipment);
          _checkEquipmentLoad++;
          break;
      }
    }
  }
}
