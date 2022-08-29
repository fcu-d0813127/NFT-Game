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
  private BlockDataController _onSelectedBlock;
  private string _onSelectedMaterialName;
  private bool _isEdit = false;

  public void SetMaterial(MaterialBlockDataController material) {
    _onSelectedMaterial = material;
    _onSelectedMaterialName = material.Name.text;
  }
  
  public void SetBlockData(BlockDataController block) {
    _onSelectedBlock = block;
  }

  public void SetIsEdit(bool isEdit) {
    _isEdit = isEdit;
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
    string inputValue = GameObject.Find("InputField").GetComponent<TMP_InputField>().text;

    // Check is empty
    if (inputValue == "") {
      return;
    }

    int num = int.Parse(inputValue);
    if (num == 0) {
      return;
    }
    if (_isEdit) {
      int nowNum = int.Parse(_onSelectedBlock.Num.text);
      int diffValue = nowNum - num;
      SetIsEdit(false);
      _onSelectedBlock.UpdateMaterialNumValue(diffValue);
      _onSelectedBlock.MaterialBlockDataController.UpdateNum();
      _onSelectedBlock.SetNum(num);
      _onSelectedBlock.GetComponent<BlockButtonController>().EnableButton();
      _onSelectedBlock.GetComponent<BlockButtonController>().IsButtonEnable = true;
    } else {
      // Check is already exit
      if (_onSelectedBlock != null &&
          _onSelectedBlock.MaterialBlockDataController == _onSelectedMaterial) {
        if (_onSelectedBlock.Num.text == "200") {
          return;
        }
        int remaining = _onSelectedBlock.AddNum(num);
        if (remaining > 0) {
          num -= remaining;
        }
      } else {
        CreateBlock.Instance.Create(_onSelectedMaterialName, num, _onSelectedMaterial);
      }
      UpdateMaterialValue(num);
    }
    Destroy(_myselfGameObject);
    _myselfGameObject = null;
    ProbabilityController.Instance.UpdateProbability();
  }

  private void PressNo() {
    SetIsEdit(false);
    Destroy(_myselfGameObject);
    _myselfGameObject = null;
    BlockDataController block = CreateBlock.Instance.FindList(_onSelectedMaterialName);
    if (block != null && block.GetComponent<BlockButtonController>().IsButtonEnable == false) {
      block.GetComponent<BlockButtonController>().EnableButton();
    }
  }

  private void ConstraintInValidValue() {
    MaterialNum materialNum = TempMaterialNum.MaterialNum;
    string onSelectedName = MaterialChineseMapping.English[_onSelectedMaterialName];
    int maxValue =
        (int)typeof(MaterialNum).GetProperty(onSelectedName).GetValue(materialNum);
    TMP_InputField inputField = GameObject.Find("InputField").GetComponent<TMP_InputField>();
    if (inputField.text == "") {
      return;
    }
    int onSelectedNum = 0;
    if (_onSelectedBlock) {
      onSelectedNum = int.Parse(_onSelectedBlock.Num.text);
    }
    if (_isEdit) {
      maxValue += onSelectedNum;
    }
    int nowValue = int.Parse(inputField.text);
    if (nowValue > maxValue && maxValue <= 200) {
      inputField.text = maxValue.ToString();
    } else if (nowValue > 200 - onSelectedNum &&
               !_isEdit &&
               _onSelectedBlock != null &&
               _onSelectedBlock.MaterialBlockDataController == _onSelectedMaterial) {
      inputField.text = (200 - onSelectedNum).ToString();
    } else if (nowValue > 200) {
      inputField.text = "200";
    }
  }

  private void UpdateMaterialValue(int onSelectedNum) {
    MaterialNum materialNum = TempMaterialNum.MaterialNum;
    string onSelectedName = MaterialChineseMapping.English[_onSelectedMaterialName];
    int nowValue =
        (int)typeof(MaterialNum).GetProperty(onSelectedName).GetValue(materialNum);
    int finalValue = nowValue - onSelectedNum;
    typeof(MaterialNum).GetProperty(onSelectedName).SetValue(materialNum, finalValue);
    _onSelectedMaterial.UpdateNum();
  }
}
