using UnityEngine;
using UnityEngine.UI;

public class ForgingButtonController : MonoBehaviour {
  public static EquipmentItemData NowEquipmentItemData;
  [SerializeField] private Button _confirmButton;
  [SerializeField] private Button _clearButton;
  [SerializeField] private Image _generateItmeIcon;

  private void Awake() {
    _clearButton.onClick.AddListener(Clear);
    _confirmButton.onClick.AddListener(Confirm);
    NowEquipmentItemData = null;
  }

  private void Confirm() {
    BlockButtonController[] blockButtons = HasBlock();
    if (blockButtons.Length == 0) {
      return;
    }
    foreach (BlockButtonController blockButton in blockButtons) {
      Destroy(blockButton.gameObject);
    }
    _generateItmeIcon.color = Color.gray;

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
    BlockButtonController[] blockButtons = HasBlock();
    if (blockButtons.Length == 0) {
      return;
    }
    foreach (BlockButtonController blockButton in blockButtons) {
      blockButton.Cancel();
    }
  }

  private BlockButtonController[] HasBlock() {
    BlockButtonController[] blocks = GameObject.FindObjectsOfType<BlockButtonController>();
    return blocks == null ? null : blocks;
  }
}
