using UnityEngine;
using TMPro;

public class MaterialDisplay : MonoBehaviour {
  [SerializeField] private TMP_Text _ruby;
  [SerializeField] private TMP_Text _sapphire;
  [SerializeField] private TMP_Text _emerald;

  private void Awake() {
    _ruby.text = PlayerInfo.MaterialNum.Ruby.ToString();
    _sapphire.text = PlayerInfo.MaterialNum.Sapphire.ToString();
    _emerald.text = PlayerInfo.MaterialNum.Emerald.ToString();
  }
}
