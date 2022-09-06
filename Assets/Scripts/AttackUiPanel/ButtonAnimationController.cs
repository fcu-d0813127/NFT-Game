using UnityEngine;

public class ButtonAnimationController : MonoBehaviour {
  [SerializeField] private Animation _normalAttackAnimation;
  [SerializeField] private Animation _bullectAttackAnimation;

  private void Update() {
    if (Input.GetKeyDown(KeyCode.K)) {
      _normalAttackAnimation.Play("CoolDown");
    } else if (Input.GetKeyDown(KeyCode.X)) {
      _bullectAttackAnimation.Play("CoolDown");
    }
  }
}
