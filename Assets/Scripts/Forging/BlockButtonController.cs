using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockButtonController : MonoBehaviour {
  [SerializeField] private Button _editButton;
  [SerializeField] private Button _cancelButton;
  private BlockAnimation _blockAnimation;

  public void EnableButton() {
    _editButton.onClick.AddListener(Edit);
    _cancelButton.onClick.AddListener(Cancel);
  }

  public void DisableButton() {
    _editButton.onClick.RemoveAllListeners();
    _cancelButton.onClick.RemoveAllListeners();
  }

  public void Cancel() {
    _blockAnimation.CancelAnimation();
    BlockListController.Instance.AddOnCancelNum();
    List<BlockAnimation> needUpdateBlocks = FindNeedUpdateBlock();
    BlockListController.Instance.UpdateList(needUpdateBlocks);
    StartCoroutine(DelayDestroy());
  }

  private void Awake() {
    _blockAnimation = GetComponent<BlockAnimation>();
    EnableButton();
  }

  private void Edit() {
    InputFieldController.Instance.OpenInputField();
    InputFieldController.Instance.SetIsEdit(true);
    BlockDataController block = GetComponent<BlockDataController>();
    InputFieldController.Instance.SetBlockData(block);
    InputFieldController.Instance.SetMaterial(block.MaterialBlockDataController);
  }

  private IEnumerator DelayDestroy() {
    BlockDataController block = GetComponent<BlockDataController>();
    string name = block.Name.text;
    int value = int.Parse(block.Num.text);
    block.UpdateMaterialNumValue(name, value);
    yield return new WaitForSeconds(1.0f);
    BlockListController.Instance.SubOnCancelNum();
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
}
