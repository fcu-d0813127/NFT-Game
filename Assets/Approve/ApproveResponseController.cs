using System.Collections;
using UnityEngine;

public class ApproveResponseController : MonoBehaviour {
  [SerializeField] private GameObject _approveResponsePrefab;
  [SerializeField] private GameObject _moneyNotEnoughPrefab;
  [SerializeField] private GameObject _parentCanvas;
  private GameObject _myselfGameObject;

  private void Open() {
    if (_myselfGameObject != null) {
      return;
    }
    GameObject approveResponseGameObject = Instantiate(_approveResponsePrefab);
    approveResponseGameObject.transform.SetParent(_parentCanvas.transform, false);
    _myselfGameObject = approveResponseGameObject;
  }

  private void OpenMoneyNotEnough() {
    GameObject moneyNotEnoughGameObject = Instantiate(_moneyNotEnoughPrefab);
    moneyNotEnoughGameObject.transform.SetParent(_parentCanvas.transform, false);
    StartCoroutine(DelayDestroy(moneyNotEnoughGameObject));
  }

  private IEnumerator DelayDestroy(GameObject gameObject) {
    yield return new WaitForSeconds(2.0f);
    Destroy(gameObject);
  }

  private void Cancel() {
    Destroy(_myselfGameObject);
    _myselfGameObject = null;
  }

  private void CloseLoading() {
    LoadingSceneController.UnLoadScene();
  }
}
