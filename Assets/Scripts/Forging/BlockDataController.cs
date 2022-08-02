using UnityEngine;
using TMPro;

public class BlockDataController : MonoBehaviour {
  public TMP_Text Name;
  public TMP_Text Num;

  public void AddNum(int num) {
    int nowNum = int.Parse(Num.text);
    nowNum += num;
    Num.text = nowNum.ToString();
    GetComponent<BlockAnimation>().UpdateNumAnimation();
  }
}
