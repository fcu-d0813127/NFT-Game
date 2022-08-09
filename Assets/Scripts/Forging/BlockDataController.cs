using UnityEngine;
using TMPro;

public class BlockDataController : MonoBehaviour {
  public TMP_Text Name;
  public TMP_Text Num;
  public MaterialBlockDataController MaterialBlockDataController {get; private set;}

  public int AddNum(int num) {
    int nowNum = int.Parse(Num.text);
    nowNum += num;
    int remaining = nowNum - 200;
    if (remaining >= 0) {
      nowNum = 200;
    }
    Num.text = nowNum.ToString();
    GetComponent<BlockAnimation>().UpdateNumAnimation();
    return remaining;
  }

  public void SetMaterialBlockDataController(
      MaterialBlockDataController materialBlockDataController) {
    MaterialBlockDataController = materialBlockDataController;
  }

  public void UpdateMaterialNumValue(int addBack) {
    string name = Name.text;
    MaterialNum materialNum = PlayerInfo.MaterialNum;
    int nowValue = (int)typeof(MaterialNum).GetProperty(name).GetValue(materialNum);
    int finalValue = nowValue + addBack;
    typeof(MaterialNum).GetProperty(name).SetValue(materialNum, finalValue);
  }

  public void SetNum(int num) {
    Num.text = num.ToString();
    GetComponent<BlockAnimation>().UpdateNumAnimation();
  }
}
