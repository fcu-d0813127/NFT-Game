using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldController : MonoBehaviour {
  [SerializeField] private GameObject _inputFieldPrefab;
  [SerializeField] private GameObject _parentCanvas;
  [SerializeField] private CreateBlock _createBlock;
  private Button _noButton;
  private Button _yesButton;
  private GameObject _myselfGameObject;
  private string _onSelectedMaterialName;

  public void SetMaterialName(string materialName) {
    _onSelectedMaterialName = materialName;
  }

  public void OpenInputField() {
    GameObject inputField = Instantiate(_inputFieldPrefab);
    inputField.transform.SetParent(_parentCanvas.transform, false);
    _noButton = GameObject.Find("No").GetComponent<Button>();
    _yesButton = GameObject.Find("Yes").GetComponent<Button>();
    _noButton.onClick.AddListener(PressNo);
    _yesButton.onClick.AddListener(PressYes);
    _myselfGameObject = inputField;
  }

  private void PressYes() {
    int num = int.Parse(GameObject.Find("InputField").GetComponent<TMP_InputField>().text);
    
    // Check is already exit
    BlockDataController block = _createBlock.FindList(_onSelectedMaterialName);
    if (block != null) {
      block.AddNum(num);
    } else {
      _createBlock.Create(_onSelectedMaterialName, num);
    }
    Destroy(_myselfGameObject);
  }

  private void PressNo() {
    Destroy(_myselfGameObject);
  }
}
