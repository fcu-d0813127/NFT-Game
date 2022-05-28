using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour {
  private bool _playerMove = true;
  private Rigidbody2D _body;
  [SerializeField] private float _playerSpeed = 5.0f;

  private void Start() {
    _body = GetComponent<Rigidbody2D>();
  }

  private void FixedUpdate() {
    if (Keyboard.current.bKey.isPressed || Keyboard.current.sKey.isPressed) {
      _playerMove = false;
    } else if (Keyboard.current.escapeKey.isPressed) {
      _playerMove = true;
    }
    if (_playerMove == false) {
      return;
    }
    Vector2 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    Vector2 nowPosition = this.gameObject.transform.position;
    if (move != Vector2.zero) {
      _body.MovePosition(nowPosition + move * Time.deltaTime * _playerSpeed);
    }
  }
}