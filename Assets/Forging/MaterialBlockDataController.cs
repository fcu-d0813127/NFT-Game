using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaterialBlockDataController : MonoBehaviour {
  public TMP_Text Name;
  public TMP_Text Num;
  public Image Icon;
  [SerializeField] private Animation _updateNumAnimation;

  public void SetNameAndNum(string name, int num) {
    Name.text = name;
    Num.text = num.ToString();
    Icon.color = Color.white;
    if (name == "紅寶石") {
      Icon.sprite = MaterialIcon.Ruby;
    } else if (name == "藍寶石") {
      Icon.sprite = MaterialIcon.Sapphire;
    } else {
      Icon.sprite = MaterialIcon.Emerald;
    }
  }

  public void UpdateNum() {
    MaterialNum materialNum = TempMaterialNum.MaterialNum;
    string name = MaterialChineseMapping.English[Name.text];
    int value = (int)typeof(MaterialNum).GetProperty(name).GetValue(materialNum);
    Num.text = value.ToString();
    _updateNumAnimation.Play("UpdateNumAnimation");
  }
}
