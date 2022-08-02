using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockListController : MonoBehaviour {
  private int _onCancelNum = 0;
  private bool _readyUpdateList = false;
  private List<BlockAnimation> _blockAnimationPlayQueue = new List<BlockAnimation>();

  public int GetOnCancelNum() {
    return _onCancelNum;
  }

  public void AddOnCancelNum() {
    _onCancelNum++;
    if (!_readyUpdateList) {
      StartCoroutine(DelayUpdateList());
    }
  }

  public void SubOnCancelNum() {
    _onCancelNum--;
  }

  public void UpdateList(List<BlockAnimation> blockAnimations) {
    foreach (BlockAnimation blockAnimation in blockAnimations) {
      float nowPositionY = blockAnimation.NowPositionY;
      float realNowPositionY =
          blockAnimation.gameObject.GetComponent<RectTransform>().anchoredPosition.y;
      blockAnimation.SetPosition(realNowPositionY);
      blockAnimation.AddEndPosition(nowPositionY);
      bool needAdd = IsInQueue(blockAnimation);
      if (needAdd) {
        _blockAnimationPlayQueue.Add(blockAnimation);
      }
    }
  }

  private IEnumerator DelayUpdateList() {
    _readyUpdateList = true;
    yield return new WaitUntil(() => _onCancelNum == 0);
    _readyUpdateList = false;
    UpdateListAnimation();
  }

  private void UpdateListAnimation() {
    foreach (BlockAnimation blockAnimation in _blockAnimationPlayQueue) {
      if (blockAnimation == null) {
        continue;
      }
      blockAnimation.FallDownAnimation();
    }
    ClearQueue();
  }

  private bool IsInQueue(BlockAnimation blockAnimation) {
    foreach (BlockAnimation block in _blockAnimationPlayQueue) {
      if (block == blockAnimation) {
        return false;
      }
    }
    return true;
  }

  private void ClearQueue() {
    _blockAnimationPlayQueue.Clear();
  }
}
