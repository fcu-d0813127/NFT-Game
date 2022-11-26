using UnityEngine;
using TMPro;

public class ProbabilityController : MonoBehaviour {
  public static ProbabilityController Instance {get; private set;}
  private const float _lagendary = 5.0f * 20.0f;
  private const float _rare = 10.0f * 20.0f;
  private const float _uncommon = 30.0f * 20.0f;
  private const float _common = 55.0f * 20.0f;
  [SerializeField] private TMP_Text _lagendaryProbabilityUI;
  [SerializeField] private TMP_Text _rareProbabilityUI;
  [SerializeField] private TMP_Text _uncommonProbabilityUI;
  [SerializeField] private TMP_Text _commonProbabilityUI;
  private float _lagendaryProbability;
  private float _rareProbability;
  private float _uncommonProbability;
  private float _commonProbability;

  public void UpdateProbability() {
    int totalNum = GetTotalMaterialNum();
    if (totalNum < 300) {
      ClearProbabilityValue();
      return;
    }
    int offset = totalNum - 300;
    _commonProbability = _common - (offset << 1);
    _uncommonProbability =
        _uncommon +
        (offset > 100 ? 10.0f * 20.0f : offset << 1) +
        (offset > 200 ? 5.0f * 20.0f : (offset > 100 ? offset - 100 : 0.0f));
    _rareProbability = _rare + (offset <= 100 ? 0.0f : offset - 100);
    _lagendaryProbability = _lagendary + (offset <= 200 ? 0.0f : offset - 200);
    UpdateUiText();
  }

  public void ClearProbabilityValue() {
    _lagendaryProbabilityUI.text = "X";
    _rareProbabilityUI.text = "X";
    _uncommonProbabilityUI.text = "X";
    _commonProbabilityUI.text = "X";
  }

  private void Awake() {
    Instance = this;
  }

  private void UpdateUiText() {
    _lagendaryProbabilityUI.text = (_lagendaryProbability / 20.0f).ToString("0.00");
    _rareProbabilityUI.text = (_rareProbability / 20.0f).ToString("0.00");
    _uncommonProbabilityUI.text = (_uncommonProbability / 20.0f).ToString("0.00");
    _commonProbabilityUI.text = (_commonProbability / 20.0f).ToString("0.00");
  }

  private int GetTotalMaterialNum() {
    int totalNum = 0;
    BlockDataController[] blocks = GameObject.FindObjectsOfType<BlockDataController>();
    foreach (BlockDataController block in blocks) {
      totalNum += int.Parse(block.Num.text);
    }
    return totalNum;
  }

  private int GetRubyNum() {
    BlockDataController[] blocks = GameObject.FindObjectsOfType<BlockDataController>();
    foreach (BlockDataController block in blocks) {
      if (block.Name.text == "Ruby") {
        return int.Parse(block.Num.text);
      }
    }
    return 0;
  }
    
  private int GetSapphireNum() {
    BlockDataController[] blocks = GameObject.FindObjectsOfType<BlockDataController>();
    foreach (BlockDataController block in blocks) {
      if (block.Name.text == "Sapphire") {
        return int.Parse(block.Num.text);
      }
    }
    return 0;
  }
}
