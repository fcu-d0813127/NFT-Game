using System.Collections.Generic;
using UnityEngine;

public class MaterialBlockCreate : MonoBehaviour {
  public static MaterialBlockCreate Instance {get; private set;}
  [SerializeField] private GameObject _canvas;
  [SerializeField] private GameObject _materialBlockPrefab;
  private float _generatePositionY = 100.0f;

  private void Awake() {
    // 正式用
    TempMaterialNum.MaterialNum = PlayerInfo.MaterialNum;
    
    // 鍛造 scene 測試用
    // TempMaterialNum.MaterialNum = new MaterialNum {
    //   Ruby = 250,
    //   Sapphire = 250,
    //   Emerald = 250
    // };

    Instance = this;
    Create();
  }

  private void Create() {
    Dictionary<string, int> allMaterials = GetAllMaterials();
    foreach (var i in allMaterials) {
      GameObject newBlock = Instantiate(_materialBlockPrefab);
      newBlock.transform.SetParent(_canvas.transform, false);
      MaterialBlockDataController materialData =
          newBlock.GetComponent<MaterialBlockDataController>();
      string materialName = MaterialChineseMapping.Chinese[i.Key];
      materialData.SetNameAndNum(materialName, i.Value);
      SetPosition(newBlock);
    }
  }

  private Dictionary<string, int> GetAllMaterials() {
    Dictionary<string, int> materialList = new Dictionary<string, int>();
    var allMaterialValue = typeof(MaterialNum).GetProperties();
    MaterialNum materialNum = TempMaterialNum.MaterialNum;
    foreach (var i in allMaterialValue) {
      string name = i.Name;
      var value = typeof(MaterialNum).GetProperty(name).GetValue(materialNum);
      materialList.Add(name, (int)value);
    }
    return materialList;
  }

  private void SetPosition(GameObject materialBlock) {
    float originPositionX = materialBlock.GetComponent<RectTransform>().anchoredPosition.x;
    materialBlock.GetComponent<RectTransform>().localPosition =
        new Vector2(originPositionX, _generatePositionY);
    _generatePositionY -= 100.0f;
  }
}
