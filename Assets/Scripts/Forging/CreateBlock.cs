using UnityEngine;

public class CreateBlock : MonoBehaviour {
  private float _generatePositionY = -45.0f;

  [SerializeField] private GameObject _parentCanvas;
  [SerializeField] private GameObject _block;

  public void UpdateGeneratePositionY(float moveDistance) {
    _generatePositionY -= moveDistance;
  }

  public void Create(GameObject block) {
    GameObject newBlock = Instantiate(_block);
    newBlock.transform.SetParent(_parentCanvas.transform, false);

    // Set name and number
    BlockDataController oldBlockData = block.GetComponent<BlockDataController>();
    BlockDataController newBlockData = newBlock.GetComponent<BlockDataController>();
    newBlockData.Name.text = oldBlockData.Name.text;
    newBlockData.Num.text = oldBlockData.Num.text;

    BlockAnimation newBlockAnimation = newBlock.GetComponent<BlockAnimation>();

    // Set position y
    float originY = block.GetComponent<RectTransform>().anchoredPosition.y;
    newBlockAnimation.SetPosition(originY);
    newBlockAnimation.AddEndPosition(originY);

    // Play
    newBlock.GetComponent<BlockAnimation>().FallDownAnimation();
  }

  public void Create(string materialName, int materialNum) {
    // Create block
    GameObject block = Instantiate(_block);
    block.transform.SetParent(_parentCanvas.transform, false);

    // Set create position
    BlockAnimation blockAnimation = block.GetComponent<BlockAnimation>();
    blockAnimation.SetPosition(_generatePositionY);
    blockAnimation.AddEndPosition(_generatePositionY);

    // Set name and number
    BlockDataController blockData = block.GetComponent<BlockDataController>();
    blockData.Name.text = materialName;
    blockData.Num.text = materialNum.ToString();

    // Update next generate y
    _generatePositionY += block.GetComponent<BlockAnimation>().MoveDistance;

    // Play animation
    blockAnimation.FallDownAnimation();
  }

  public BlockDataController FindList(string materialName) {
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

  private void Update() {
    if (Input.GetKeyUp(KeyCode.Tab)) {
      Create("Ruby", 20);
    }
  }
}
