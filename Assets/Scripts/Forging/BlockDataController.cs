using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlockDataController : MonoBehaviour {
  public TMP_Text Name;
  public TMP_Text Num;
  [SerializeField] Button _editButton;
  [SerializeField] Button _cancelButton;
  [SerializeField] private Animation _updateNum;
  [SerializeField] private BlockAnimation _blockAnimation;

  public void AddNum(int num) {
    int nowNum = int.Parse(Num.text);
    nowNum += num;
    Num.text = nowNum.ToString();

    // Play animation
    _updateNum.Play("UpdateNumAnimation");
  }

  private void Awake() {
    _cancelButton.onClick.AddListener(Cancel);
    _blockAnimation.FallDownAnimation();
  }

  private void Cancel() {
    _blockAnimation.CancelAnimation();
    
    // Destroy block
    StartCoroutine(DelayDestroy(1.0f));
    StartCoroutine(UpdateBlockList(1.0f));
  }

  private IEnumerator DelayDestroy(float delayTime) {
    yield return new WaitForSeconds(delayTime);
    Destroy(this.gameObject);
  }

  private IEnumerator UpdateBlockList(float delayTime) {
    yield return new WaitForSeconds(delayTime);
    GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
    float myY = GetComponent<RectTransform>().anchoredPosition.y;
    foreach (GameObject block in blocks) {
      float targetY = block.GetComponent<RectTransform>().anchoredPosition.y;
      if (targetY > myY) {
        CreateBlock createBlock =
            GameObject.Find("CreateSelectedBlock").GetComponentInParent<CreateBlock>();
        createBlock.Create(block);
        Destroy(block);
      }
    }
  }
}
