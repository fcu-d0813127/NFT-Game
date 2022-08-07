using UnityEngine;
using UnityEngine.UI;

public class ForgingButtonController : MonoBehaviour {
  public static EquipmentItemData NowEquipmentItemData {get; private set;}
  [SerializeField] private Button _confirmButton;
  [SerializeField] private Button _clearButton;
  [SerializeField] private Image _generateItmeIcon;

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
      totalNum += int.Parse(block.Num.text);
    }
    if (totalNum < 300) {
      return;
    }
    foreach (BlockDataController block in blocks) {
      Destroy(block.gameObject);
    }
    _generateItmeIcon.color = Color.cyan;
    ProbabilityController.Instance.ClearProbabilityValue();
    CreateBlock.Instance.ResetGeneratePositionY();

    Attribute attribute = new Attribute {
      Atk = 100.0f,
      Matk = 100.0f,
      Def = 100.0f,
      Mdef = 100.0f,
      Cri = (float)100.0f / 10000,
      CriDmgRatio = (float)6.0f / 100
    };
    NowEquipmentItemData = new EquipmentItemData {
      Id = 1,
      DisplayName = $"Id: {1}",
      MaxStackSize = 1,
      Rarity = 1,
      Part = 1,
      Level = 1,
      Attribute = attribute,
      Skills = new int[0]
    };
    EquipmentItems.Add(NowEquipmentItemData);
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

  private BlockDataController[] HasBlock() {
    BlockDataController[] blocks = GameObject.FindObjectsOfType<BlockDataController>();
    return blocks == null ? null : blocks;
  }
}
