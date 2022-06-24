using UnityEngine;

public class CameraFollow : MonoBehaviour {
  private float _smoothTime = 0.07f;
  private Vector3 _offset = new Vector3(0f, 0f, -10f);
  private Vector3 _velocity = Vector3.zero;
  public Transform target;
  
  private void Awake() {
    if (target == null) {
      target = GameObject.Find("Player").GetComponent<Transform>();
    }
  }

  private void FixedUpdate() {
    
    Vector3 targetPosition = target.position + _offset;
    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity,
                                            _smoothTime);
  }


}
