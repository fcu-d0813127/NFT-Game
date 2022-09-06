using UnityEngine;

public class ApproveResponseController : MonoBehaviour {
  [SerializeField] private GameObject _approveResponsePrefab;
  [SerializeField] private GameObject _parentCanvas;
  private GameObject _myselfGameObject;

  private void Open() {
    if (_myselfGameObject != null) {
      return;
    }
    GameObject _approveResponseGameObject = Instantiate(_approveResponsePrefab);
    _approveResponseGameObject.transform.SetParent(_parentCanvas.transform, false);
    _myselfGameObject = _approveResponseGameObject;
  }

  private void Cancel() {
    Destroy(_myselfGameObject);
    _myselfGameObject = null;
  }
}
