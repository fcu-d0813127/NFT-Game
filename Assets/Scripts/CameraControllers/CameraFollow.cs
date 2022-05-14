using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
  public Transform Player;
  public float SmoothSpeed = 0.125f;
  public Vector3 Offset;

  private void FixedUpdate() {
    Vector3 desiredPosition = Player.position + Offset;
    Vector3 smoothedPosition = Vector3.Lerp(
                                   transform.position,
                                   desiredPosition,
                                   SmoothSpeed);
    transform.position = smoothedPosition;
  }
}
