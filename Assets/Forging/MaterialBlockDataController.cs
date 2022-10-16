using UnityEngine;
using TMPro;

public class MaterialBlockDataController : MonoBehaviour {
  public TMP_Text Name;
  public TMP_Text Num;
  [SerializeField] private Animation _updateNumAnimation;

  public void SetNameAndNum(string name, int num) {
    Name.text = name;
    Num.text = num.ToString();
  }

  public void UpdateNum() {
    MaterialNum materialNum = TempMaterialNum.MaterialNum;
    string name = MaterialChineseMapping.English[Name.text];
    int value = (int)typeof(MaterialNum).GetProperty(name).GetValue(materialNum);
    Num.text = value.ToString();
    _updateNumAnimation.Play("UpdateNumAnimation");
  }
}
