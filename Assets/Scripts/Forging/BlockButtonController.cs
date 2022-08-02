using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockButtonController : MonoBehaviour {
  [SerializeField] private Button _editButton;
  [SerializeField] private Button _cancelButton;
  private BlockListController _blockListController;
  private BlockAnimation _blockAnimation;

  private void Awake() {
    _blockListController =
        GameObject.Find("BlockListController").GetComponent<BlockListController>();
    _blockAnimation = GetComponent<BlockAnimation>();
    _editButton.onClick.AddListener(Edit);
    _cancelButton.onClick.AddListener(Cancel);
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
    yield return new WaitForSeconds(1.0f);
    _blockListController.SubOnCancelNum();
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
}
