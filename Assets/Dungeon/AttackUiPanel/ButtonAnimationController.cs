using UnityEngine;

public class ButtonAnimationController : MonoBehaviour {
  [SerializeField] private Animation _normalAttackAnimation;
  [SerializeField] private Animation _bullectAttackAnimation;
  private GameObject _player;

  private void Awake() {
    _player = GameObject.Find("Player");
  }

  private void Update() {
    if (_player == null || _player.GetComponent<PlayerAttack>().AttackAble == false) {
      return;
    }
    if (Input.GetKeyDown(KeyCode.K)) {
      _normalAttackAnimation.Play("CoolDown");
    } else if (Input.GetKeyDown(KeyCode.X)) {
      _bullectAttackAnimation.Play("CoolDown");
    }
  }
}
