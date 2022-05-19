using UnityEngine;

public class PlayerMove : MonoBehaviour {
  private CharacterController _controller;
  private Vector3 _playerVelocity;
  private float _playerSpeed = 10.0f;

  private void Start() {
    _controller = gameObject.AddComponent<CharacterController>();
  }

  void Update() {
    Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
    _controller.Move(move * Time.deltaTime * _playerSpeed);
    if (move != Vector3.zero) {
      gameObject.transform.position = move;
    }
    _controller.Move(_playerVelocity * Time.deltaTime);
  }
}