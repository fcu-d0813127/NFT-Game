using UnityEngine;

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
      Create();
    }
  }

  private void Create() {
    float positionX = 255.0f;
    GameObject block = Instantiate(_block);
    block.transform.SetParent(_parentCanvas.transform, false);
    block.GetComponent<RectTransform>().anchoredPosition =
        new Vector2(positionX, GeneratePositionY);
    GeneratePositionY += block.GetComponent<BlockAnimation>().MoveDistance;
  }
}
