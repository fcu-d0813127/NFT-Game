using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class ForgingButtonController : MonoBehaviour {
  public static EquipmentItemData NowEquipmentItemData {get; private set;}
  [DllImport("__Internal")]
  private static extern void ForgingSmartContract(
      int part,
      int amountOfRuby,
      int amountOfSapphire,
      int amountOfEmerald,
      int myAmountOfRuby,
      int myAmountOfSapphire,
      int myAmountOfEmerald);
  private List<PlayerEquipment> _playerEquipments = new List<PlayerEquipment>();
  [SerializeField] private Button _confirmButton;
  [SerializeField] private Button _clearButton;
  [SerializeField] private Image _generateItemIcon;
  [SerializeField] private TMP_Dropdown _selectPart;
  private MaterialNum _sendMaterialNum = new MaterialNum();
  private int _generateItemTokenId;

  private void Awake() {
    _clearButton.onClick.AddListener(Clear);
    _confirmButton.onClick.AddListener(Confirm);
    NowEquipmentItemData = null;
  }

  private void Confirm() {
    BlockDataController[] blocks = HasBlock();
    if (blocks.Length == 0) {
      return;
    }
    int totalNum = 0;
    foreach (BlockDataController block in blocks) {
      string name = MaterialChineseMapping.English[block.Name.text];
      int num = int.Parse(block.Num.text);
      typeof(MaterialNum).GetProperty(name).SetValue(_sendMaterialNum, num);
      totalNum += num;
    }
    if (totalNum < 300 || PlayerInfo.PlayerEquipment.Count == 30) {
      _sendMaterialNum = new MaterialNum();
      return;
    }
    int part = _selectPart.value;
    // WebGL 用
    #if UNITY_WEBGL && !UNITY_EDITOR
      ForgingSmartContract(
          part,
          _sendMaterialNum.Ruby,
          _sendMaterialNum.Sapphire,
          _sendMaterialNum.Emerald,
          PlayerInfo.MaterialNum.Ruby,
          PlayerInfo.MaterialNum.Sapphire,
          PlayerInfo.MaterialNum.Emerald);
    #endif

    // Editor 測試用
    #if UNITY_EDITOR
      Attribute attribute = new Attribute {
        Atk = 100.0f,
        Matk = 100.0f,
        Def = 100.0f,
        Mdef = 100.0f,
        Cri = 0.01f,
        CriDmgRatio = 0.01f
      };
      NowEquipmentItemData = new EquipmentItemData {
        Id = 0,
        DisplayName = "Test",
        MaxStackSize = 1,
        Rarity = "common",
        Part = part,
        Level = 1,
        Attribute = attribute,
        Skills = new int[3]
      };
      EquipmentItems.Add(NowEquipmentItemData);
      // 清除已經鍛造的素材
      foreach (BlockDataController block in blocks) {
        Destroy(block.gameObject);
      }
      _generateItemIcon.color = Color.cyan;
      ProbabilityController.Instance.ClearProbabilityValue();
      CreateBlock.Instance.ResetGeneratePositionY();
      PlayerInfo.MaterialNum = TempMaterialNum.MaterialNum;
      _sendMaterialNum = new MaterialNum();
    #endif
  }

  private void Clear() {
    BlockDataController[] blocks = HasBlock();
    if (blocks.Length == 0) {
      return;
    }
    foreach (BlockDataController block in blocks) {
      block.GetComponent<BlockButtonController>().Cancel();
    }
  }

  private void SetTokenId(int tokenId) {
    _generateItemTokenId = tokenId;
  }

  private void SetEquipment(string equipmentUri) {
    string equipmentHash = equipmentUri.Split('/')[1];
    string uri = "https://ipfs.io/ipfs/" + equipmentHash;
    StartCoroutine(GetRequest(uri));
  }

  private void CreateEquipment(PlayerEquipment equipment) {
    Attribute attribute = new Attribute {
      Atk = int.Parse(equipment.attributes[1].value),
      Matk = int.Parse(equipment.attributes[3].value),
      Def = int.Parse(equipment.attributes[2].value),
      Mdef = int.Parse(equipment.attributes[4].value),
      Cri = (float)int.Parse(equipment.attributes[5].value) / 10000,
      CriDmgRatio = (float)int.Parse(equipment.attributes[6].value) / 100
    };
    int part = -1;
    int nameLength = equipment.name.Length;
    string partName = equipment.name.Substring(nameLength - 2);
    if (partName == "武器") {
      part = 0;
    } else if (partName == "頭盔") {
      part = 3;
    } else if (partName == "胸甲") {
      part = 1;
    } else if (partName == "護腿") {
      part = 2;
    } else if (partName == "靴子") {
      part = 4;
    }
    string[] imageSplitArray = equipment.image.Split('/');
    int imageSplitArrayLength = imageSplitArray.Length;
    string imageHash = imageSplitArray[imageSplitArrayLength - 1];
    string uri = "https://ipfs.io/ipfs/" + imageHash;
    NowEquipmentItemData = new EquipmentItemData {
      Id = _generateItemTokenId,
      DisplayName = equipment.name,
      MaxStackSize = 1,
      Rarity = equipment.attributes[0].value,
      Part = part,
      Attribute = attribute,
      Icon = null
    };
    StartCoroutine(GetTexture(uri, NowEquipmentItemData));
    
    // 清除已經鍛造的素材
    BlockDataController[] blocks = HasBlock();
    foreach (BlockDataController block in blocks) {
      Destroy(block.gameObject);
    }
    _generateItemIcon.color = Color.cyan;
    ProbabilityController.Instance.ClearProbabilityValue();
    CreateBlock.Instance.ResetGeneratePositionY();
    PlayerInfo.MaterialNum = TempMaterialNum.MaterialNum;
    _sendMaterialNum = new MaterialNum();
  }

  private BlockDataController[] HasBlock() {
    BlockDataController[] blocks = GameObject.FindObjectsOfType<BlockDataController>();
    return blocks == null ? null : blocks;
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
          StartCoroutine(GetRequest(uri));
          Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
          break;
        case UnityWebRequest.Result.Success:
          string data = webRequest.downloadHandler.text;
          Debug.Log(pages[page] + ":\nReceived: " + data);
          PlayerEquipment playerEquipment = PlayerEquipment.CreateEquipment(data);
          CreateEquipment(playerEquipment);
          break;
      }
    }
  }

  private IEnumerator GetTexture(string uri, EquipmentItemData equipment) {
    UnityWebRequest www = UnityWebRequestTexture.GetTexture(uri);
    yield return www.SendWebRequest();

    if (www.result != UnityWebRequest.Result.Success) {
      Debug.Log(www.error);
      StartCoroutine(GetTexture(uri, equipment));
    } else {
      Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
      _generateItemIcon.color = Color.white;
      equipment.Icon = Sprite.Create(myTexture,
                                     new Rect(0.0f, 0.0f, myTexture.width, myTexture.height),
                                     new Vector2(0.5f, 0.5f));
    }
  }
}
