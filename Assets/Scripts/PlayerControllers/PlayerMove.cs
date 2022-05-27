using UnityEngine;

public class PlayerMove : MonoBehaviour {
  private Rigidbody2D _body;
  [SerializeField] private float _playerSpeed = 15.0f;

  private void Start() {
    _body = GetComponent<Rigidbody2D>();
  }

  private void FixedUpdate() {
    Vector2 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    Vector2 nowPosition = this.gameObject.transform.position;
    if (move != Vector2.zero) {
      _body.MovePosition(nowPosition + move * Time.deltaTime * _playerSpeed);
    }
  }
}