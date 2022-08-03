using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldController : MonoBehaviour {
  public static InputFieldController Instance {get; private set;}
  [SerializeField] private GameObject _inputFieldPrefab;
  [SerializeField] private GameObject _parentCanvas;
  private Button _noButton;
  private Button _yesButton;
  private GameObject _myselfGameObject = null;
  private MaterialBlockDataController _onSelectedMaterial;
  private string _onSelectedMaterialName;

  public void SetMaterial(MaterialBlockDataController material) {
    _onSelectedMaterial = material;
    _onSelectedMaterialName = material.Name.text;
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

  private void Awake() {
    Instance = this;
  }

  private void Update() {
    if (_myselfGameObject != null) {
      ConstraintInValidValue();
    }
  }

  private void PressYes() {
    int num = int.Parse(GameObject.Find("InputField").GetComponent<TMP_InputField>().text);
    
    // Check is already exit
    BlockDataController block = CreateBlock.Instance.FindList(_onSelectedMaterialName);
    if (block != null) {
      block.AddNum(num);
    } else {
      CreateBlock.Instance.Create(_onSelectedMaterialName, num, _onSelectedMaterial);
    }
    Destroy(_myselfGameObject);
    _myselfGameObject = null;
    UpdateMaterialValue(num);
  }

  private void PressNo() {
    Destroy(_myselfGameObject);
    _myselfGameObject = null;
  }

  private void ConstraintInValidValue() {
    MaterialNum materialNum = PlayerInfo.MaterialNum;
    int maxValue =
        (int)typeof(MaterialNum).GetProperty(_onSelectedMaterialName).GetValue(materialNum);
    TMP_InputField inputField = GameObject.Find("InputField").GetComponent<TMP_InputField>();
    if (inputField.text == "") {
      return;
    }
    int nowValue = int.Parse(inputField.text);
    if (nowValue > maxValue) {
      inputField.text = maxValue.ToString();
    }
  }

  private void UpdateMaterialValue(int onSelectedNum) {
    MaterialNum materialNum = PlayerInfo.MaterialNum;
    int nowValue =
        (int)typeof(MaterialNum).GetProperty(_onSelectedMaterialName).GetValue(materialNum);
    int finalValue = nowValue - onSelectedNum;
    typeof(MaterialNum).GetProperty(_onSelectedMaterialName).SetValue(materialNum, finalValue);
    _onSelectedMaterial.UpdateNum();
  }
}
