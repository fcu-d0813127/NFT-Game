using UnityEngine;
using UnityEngine.UI;

public class MaterialButtonController : MonoBehaviour {
  private Button _myselfButton;

  private void Awake() {
    _myselfButton = GetComponent<Button>();
    _myselfButton.onClick.AddListener(OpenInputFieldObject);
  }

  private void OpenInputFieldObject() {
    MaterialBlockDataController material = GetComponent<MaterialBlockDataController>();
    InputFieldController.Instance.SetMaterial(material);
    InputFieldController.Instance.OpenInputField();
  }
}
