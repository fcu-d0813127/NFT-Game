using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ForgingIconController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
  [SerializeField] private GameObject _detail;

  public void OnPointerEnter(PointerEventData eventData) {
    EquipmentItemData equipmentItemData = ForgingButtonController.NowEquipmentItemData;
    if (equipmentItemData == null) {
      return;
    }
    DetailDisplay detailDisplay = _detail.GetComponent<DetailDisplay>();
    detailDisplay.OnHover(equipmentItemData);
    _detail?.SetActive(true);
  }

  public void OnPointerExit(PointerEventData eventData) {
    _detail?.SetActive(false);
  }

  private void Awake() {
    _detail.SetActive(false);
  }

  private void Update() {
    if (ForgingButtonController.NowEquipmentItemData != null) {
      Vector3 mousePosition = Mouse.current.position.ReadValue();
      _detail.transform.position = mousePosition;
    }
  }
}
