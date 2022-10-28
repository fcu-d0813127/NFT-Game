using UnityEngine;

public class CreateBlock : MonoBehaviour {
  public static CreateBlock Instance {get; private set;}
  private const float _originGeneratePositionY = -45.0f;
  private float _generatePositionY = _originGeneratePositionY;

  [SerializeField] private GameObject _parentCanvas;
  [SerializeField] private GameObject _block;

  public void UpdateGeneratePositionY(float moveDistance) {
    _generatePositionY -= moveDistance;
  }

  public void Create(string materialName,
                     int materialNum,
                     MaterialBlockDataController materialBlockDataController) {
    // Create block
    GameObject block = Instantiate(_block);
    block.transform.SetParent(_parentCanvas.transform, false);

    // Set create position
    BlockAnimation blockAnimation = block.GetComponent<BlockAnimation>();
    blockAnimation.SetPosition(_generatePositionY);
    blockAnimation.AddEndPosition(_generatePositionY);

    // Set name, number and cancel reference
    BlockDataController blockData = block.GetComponent<BlockDataController>();
    blockData.Name.text = materialName;
    blockData.Num.text = materialNum.ToString();
    blockData.Icon.sprite = materialBlockDataController.Icon.sprite;
    blockData.Icon.color = Color.white;
    blockData.SetMaterialBlockDataController(materialBlockDataController);

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

  public void ResetGeneratePositionY() {
    _generatePositionY = _originGeneratePositionY;
  }

  private void Awake() {
    Instance = this;
  }
}
