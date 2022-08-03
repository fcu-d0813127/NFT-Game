using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockButtonController : MonoBehaviour {
  [SerializeField] private Button _editButton;
  [SerializeField] private Button _cancelButton;
  private BlockListController _blockListController;
  private BlockAnimation _blockAnimation;

  public void EnableButton() {
    _editButton.onClick.AddListener(Edit);
    _cancelButton.onClick.AddListener(Cancel);
  }

  public void DisableButton() {
    _editButton.onClick.RemoveAllListeners();
    _cancelButton.onClick.RemoveAllListeners();
  }

  private void Awake() {
    _blockListController =
        GameObject.Find("BlockListController").GetComponent<BlockListController>();
    _blockAnimation = GetComponent<BlockAnimation>();
    EnableButton();
  }

  private void Edit() {

  }

  private void Cancel() {
    _blockAnimation.CancelAnimation();
    _blockListController.AddOnCancelNum();
    List<BlockAnimation> needUpdateBlocks = FindNeedUpdateBlock();
    _blockListController.UpdateList(needUpdateBlocks);
    StartCoroutine(DelayDestroy());
  }

  private IEnumerator DelayDestroy() {
    BlockDataController block = GetComponent<BlockDataController>();
    string name = block.Name.text;
    int value = int.Parse(block.Num.text);
    UpdateMaterialNumValue(name, value);
    yield return new WaitForSeconds(1.0f);
    _blockListController.SubOnCancelNum();
    block.MaterialBlockDataController.UpdateNum();
    Destroy(this.gameObject);
  }

  private List<BlockAnimation> FindNeedUpdateBlock() {
    List<BlockAnimation> needUpdateBlocks = new List<BlockAnimation>();
    GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
    float myPositionY = GetComponent<RectTransform>().anchoredPosition.y;
    foreach (GameObject block in blocks) {
      float targetPositionY = block.GetComponent<RectTransform>().anchoredPosition.y;
      if (targetPositionY > myPositionY) {
        needUpdateBlocks.Add(block.GetComponent<BlockAnimation>());
      }
    }
    return needUpdateBlocks;
  }

  private void UpdateMaterialNumValue(string name, int addBack) {
    MaterialNum materialNum = PlayerInfo.MaterialNum;
    int nowValue = (int)typeof(MaterialNum).GetProperty(name).GetValue(materialNum);
    int finalValue = nowValue + addBack;
    typeof(MaterialNum).GetProperty(name).SetValue(materialNum, finalValue);
  }
}
