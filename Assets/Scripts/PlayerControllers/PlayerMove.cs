using UnityEngine;

public class PlayerMove : MonoBehaviour {
  private float _playerSpeed = 10.0f;

  private void Update() {
    Vector2 move = new Vector2(
                       Input.GetAxis("Horizontal"),
                       Input.GetAxis("Vertical"));
    if (move != Vector2.zero) {
      transform.Translate(move * Time.deltaTime * _playerSpeed);
    }
  }
}