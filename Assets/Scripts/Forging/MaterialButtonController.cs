using UnityEngine;
using UnityEngine.UI;

public class MaterialButtonController : MonoBehaviour {
  [SerializeField] private InputFieldController _inputFieldController;
  private Button _myselfButton;

  private void Awake() {
    _myselfButton = GetComponent<Button>();
    _myselfButton.onClick.AddListener(OpenInputFieldObject);
  }

  private void OpenInputFieldObject() {
    _inputFieldController.SetMaterialName(this.gameObject.name);
    _inputFieldController.OpenInputField();
  }
}
