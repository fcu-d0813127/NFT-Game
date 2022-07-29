using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlockAnimation : MonoBehaviour {
  public float MoveDistance = 112.0f;
  [SerializeField] Button _cancelButton;

  private void Start() {
    Button[] buttons = GetComponentsInChildren<Button>();
    foreach (Button button in buttons) {
      if (button.gameObject.name == "Cancel") {
        _cancelButton = button;
      }
    }
    _cancelButton.onClick.AddListener(CancelAnimation);
    FallDownAnimation();
  }

  private void FallDownAnimation() {
    float startValue = GetComponent<RectTransform>().anchoredPosition.y;
    float endValue = startValue - MoveDistance;

    Animation anim = GetComponent<Animation>();
    AnimationCurve curve;

    AnimationClip clip = new AnimationClip();
    clip.legacy = true;

    Keyframe[] keys = new Keyframe[2];

    // 往下掉
    keys[0] = new Keyframe(0.0f, startValue);
    keys[1] = new Keyframe(1.0f, endValue);
    curve = new AnimationCurve(keys);
    clip.SetCurve("", typeof(Transform), "localPosition.y", curve);
    startValue = GetComponent<RectTransform>().anchoredPosition.x;
    keys[0] = new Keyframe(0.0f, startValue);
    keys[1] = new Keyframe(1.0f, startValue);
    curve = new AnimationCurve(keys);
    clip.SetCurve("", typeof(Transform), "localPosition.x", curve);

    anim.AddClip(clip, clip.name);
    anim.Play(clip.name);
  }

  private void CancelAnimation() {
    float startValue = GetComponent<RectTransform>().anchoredPosition.x;
    float endValue = startValue + 1000.0f;

    Animation anim = GetComponent<Animation>();
    AnimationCurve curve;

    AnimationClip clip = new AnimationClip();
    clip.legacy = true;

    Keyframe[] keys = new Keyframe[2];

    // 往右移
    keys[0] = new Keyframe(0.0f, startValue);
    keys[1] = new Keyframe(1.0f, endValue);
    curve = new AnimationCurve(keys);
    clip.SetCurve("", typeof(Transform), "localPosition.x", curve);
    startValue = GetComponent<RectTransform>().anchoredPosition.y;
    keys[0] = new Keyframe(0.0f, startValue);
    keys[1] = new Keyframe(1.0f, startValue);
    curve = new AnimationCurve(keys);
    clip.SetCurve("", typeof(Transform), "localPosition.y", curve);

    anim.AddClip(clip, clip.name);
    anim.Play(clip.name);

    CreateBlock createBlock =
        GameObject.Find("CreateSelectedBlock").GetComponentInParent<CreateBlock>();
    createBlock.UpdateGeneratePositionY(MoveDistance);
    
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
