using UnityEngine;

public class BlockAnimation : MonoBehaviour {
  public float MoveDistance = 112.0f;
  public float NowPositionY;
  [SerializeField] private Animation _updateNumAnimation; 
  private float _generatePositionY;

  public void SetPosition(float generateY) {
    _generatePositionY = generateY;
  }

  public void AddEndPosition(float nowPositionY) {
    NowPositionY = nowPositionY - MoveDistance;
  }

  public void FallDownAnimation() {
    float startValue = _generatePositionY;
    float endValue = NowPositionY;

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

  public void CancelAnimation() {
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

    CreateBlock.Instance.UpdateGeneratePositionY(MoveDistance);
  }

  public void UpdateNumAnimation() {
    _updateNumAnimation.Play("UpdateNumAnimation");
  }
}
