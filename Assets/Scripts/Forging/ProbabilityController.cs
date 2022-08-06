using System.Collections;
using UnityEngine;
using TMPro;

public class ProbabilityController : MonoBehaviour {
  public static ProbabilityController Instance {get; private set;}
  [SerializeField] private TMP_Text _lagendaryProbabilityPhysic;
  [SerializeField] private TMP_Text _rareProbabilityPhysic;
  [SerializeField] private TMP_Text _uncommonProbabilityPhysic;
  [SerializeField] private TMP_Text _commonProbabilityPhysic;
  [SerializeField] private TMP_Text _lagendaryProbabilityMagic;
  [SerializeField] private TMP_Text _rareProbabilityMagic;
  [SerializeField] private TMP_Text _uncommonProbabilityMagic;
  [SerializeField] private TMP_Text _commonProbabilityMagic;
  private float _lagendaryProbability = 5.0f * 20.0f;
  private float _rareProbability = 10.0f * 20.0f;
  private float _uncommonProbability = 30.0f * 20.0f;
  private float _commonProbability = 55.0f * 20.0f;

  public void UpdateProbability() {
    int totalNum = GetTotalMaterialNum();
    int rubyNum = GetRubyNum();
    int sapphireNum = GetSapphireNum();
    float defaultDivide = 0.5f + (sapphireNum - rubyNum) * 0.0025f;
    if (totalNum < 300) {
      ClearProbabilityValue();
      return;
    }
    int offset = totalNum - 300;
    _commonProbability -= offset << 1;
    _uncommonProbability +=
        (offset > 100 ? 10.0f * 20.0f : offset << 1) +
        (offset > 200 ? 5.0f * 20.0f : (offset > 100 ? offset - 100 : 0.0f));
    _rareProbability += offset <= 100 ? 0.0f : offset - 100;
    _lagendaryProbability += offset <= 200 ? 0.0f : offset - 200;
    UpdateUiText(defaultDivide);
  }

  public void ClearProbabilityValue() {
    _lagendaryProbabilityPhysic.text = "X";
    _rareProbabilityPhysic.text = "X";
    _uncommonProbabilityPhysic.text = "X";
    _commonProbabilityPhysic.text = "X";
    _lagendaryProbabilityMagic.text = "X";
    _rareProbabilityMagic.text = "X";
    _uncommonProbabilityMagic.text = "X";
    _commonProbabilityMagic.text = "X";
  }

  private void Awake() {
    Instance = this;
  }

  private void UpdateUiText(float defaultDivide) {
    float _lagendaryPhysic = _lagendaryProbability / 20.0f * (1.0f - defaultDivide);
    float _rarePhysic = _rareProbability / 20.0f * (1.0f - defaultDivide);
    float _uncommonPhysic = _uncommonProbability / 20.0f * (1.0f - defaultDivide);
    float _commonPhysic = _commonProbability / 20.0f * (1.0f - defaultDivide);
    float _lagendaryMagic = _lagendaryProbability / 20.0f * defaultDivide;
    float _rareMagic = _rareProbability / 20.0f * defaultDivide;
    float _uncommonMagic = _uncommonProbability / 20.0f * defaultDivide;
    float _commonMagic = _commonProbability / 20.0f * defaultDivide;
    _lagendaryProbabilityPhysic.text = _lagendaryPhysic.ToString("0.0");
    _rareProbabilityPhysic.text = _rarePhysic.ToString("0.0");
    _uncommonProbabilityPhysic.text = _uncommonPhysic.ToString("0.0");
    _commonProbabilityPhysic.text = _commonPhysic.ToString("0.0");
    _lagendaryProbabilityMagic.text = _lagendaryMagic.ToString("0.0");
    _rareProbabilityMagic.text = _rareMagic.ToString("0.0");
    _uncommonProbabilityMagic.text = _uncommonMagic.ToString("0.0");
    _commonProbabilityMagic.text = _commonMagic.ToString("0.0");
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
