using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
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
  [SerializeField] private Button _confirmButton;
  [SerializeField] private Button _clearButton;
  [SerializeField] private Image _generateItmeIcon;
  [SerializeField] private TMP_Dropdown _selectPart;
  private MaterialNum _sendMaterialNum = new MaterialNum();

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
    if (totalNum < 300 || PlayerInfo.PlayerEquipment.equipments.Length == 30) {
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
        Rarity = 0,
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
      _generateItmeIcon.color = Color.cyan;
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

  private void SetEquipment(string equipmentJson) {
    PlayerEquipment equipments = PlayerEquipment.CreateEquipment(equipmentJson);
    PlayerEquipment.Equipment generateEquipment = equipments.equipments[0];
    Attribute attribute = new Attribute {
      Atk = generateEquipment.equipmentStatus.attribute[0],
      Matk = generateEquipment.equipmentStatus.attribute[1],
      Def = generateEquipment.equipmentStatus.attribute[2],
      Mdef = generateEquipment.equipmentStatus.attribute[3],
      Cri = (float)generateEquipment.equipmentStatus.attribute[4] / 10000,
      CriDmgRatio = (float)generateEquipment.equipmentStatus.attribute[5] / 100
    };
    NowEquipmentItemData = new EquipmentItemData {
      Id = generateEquipment.tokenId,
      DisplayName = $"Id: {generateEquipment.tokenId}",
      MaxStackSize = 1,
      Rarity = generateEquipment.equipmentStatus.rarity,
      Part = generateEquipment.equipmentStatus.part,
      Level = generateEquipment.equipmentStatus.level,
      Attribute = attribute,
      Skills = generateEquipment.equipmentStatus.skills
    };
    EquipmentItems.Add(NowEquipmentItemData);
    
    // 清除已經鍛造的素材
    BlockDataController[] blocks = HasBlock();
    foreach (BlockDataController block in blocks) {
      Destroy(block.gameObject);
    }
    _generateItmeIcon.color = Color.cyan;
    ProbabilityController.Instance.ClearProbabilityValue();
    CreateBlock.Instance.ResetGeneratePositionY();
    PlayerInfo.MaterialNum = TempMaterialNum.MaterialNum;
    _sendMaterialNum = new MaterialNum();
  }

  private BlockDataController[] HasBlock() {
    BlockDataController[] blocks = GameObject.FindObjectsOfType<BlockDataController>();
    return blocks == null ? null : blocks;
  }
}
