using UnityEngine;
using TMPro;

public class CreateBlock : MonoBehaviour {
  private float GeneratePositionY = -45.0f;

  [SerializeField] private GameObject _parentCanvas;
  [SerializeField] private GameObject _block;

  public void UpdateGeneratePositionY(float moveDistance) {
    GeneratePositionY -= moveDistance;
  }

  public void Create(GameObject block) {
    GameObject newBlock = Instantiate(_block);
    newBlock.transform.SetParent(_parentCanvas.transform, false);
    float blockX = block.GetComponent<RectTransform>().anchoredPosition.x;
    float blockY = block.GetComponent<RectTransform>().anchoredPosition.y;
    Vector2 originPosition = new Vector2(blockX, blockY);
    newBlock.GetComponent<RectTransform>().anchoredPosition = originPosition;
  }

  private void Update() {
    if (Input.GetKeyUp(KeyCode.Tab)) {
      BlockDataController block = FindList("Ruby");
      if (block != null) {
        block.AddNum(20);
        return;
      }
      Create("Ruby", 20);
    }
  }

  private void Create(string materialName, int materialNum) {
    // Create block
    GameObject block = Instantiate(_block);
    block.transform.SetParent(_parentCanvas.transform, false);

    // Set create position
    float positionX = 255.0f;
    block.GetComponent<RectTransform>().anchoredPosition =
        new Vector2(positionX, GeneratePositionY);

    // Set name and number
    BlockDataController blockData = block.GetComponent<BlockDataController>();
    blockData.Name.text = materialName;
    blockData.Num.text = materialNum.ToString();

    // Update next generate y
    GeneratePositionY += block.GetComponent<BlockAnimation>().MoveDistance;
  }

  private BlockDataController FindList(string materialName) {
    // Get all block
    BlockDataController[] blocks = GameObject.FindObjectsOfType<BlockDataController>();

    // Check is in. true: return object, false: return null
    foreach (var block in blocks) {
      if (block.Name.text == materialName) {
        return block;
      }
    }
    return null;
  }
}
