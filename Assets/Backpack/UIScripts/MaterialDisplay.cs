using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaterialDisplay : MonoBehaviour {
  [SerializeField] private Image _rubyIcon;
  [SerializeField] private Image _sapphireIcon;
  [SerializeField] private Image _emeraldIcon;
  [SerializeField] private TMP_Text _ruby;
  [SerializeField] private TMP_Text _sapphire;
  [SerializeField] private TMP_Text _emerald;

  private void Awake() {
    _rubyIcon.color = Color.white;
    _rubyIcon.sprite = MaterialIcon.Ruby;
    _sapphireIcon.color = Color.white;
    _sapphireIcon.sprite = MaterialIcon.Sapphire;
    _emeraldIcon.color = Color.white;
    _emeraldIcon.sprite = MaterialIcon.Emerald;
    _ruby.text = PlayerInfo.MaterialNum.Ruby.ToString();
    _sapphire.text = PlayerInfo.MaterialNum.Sapphire.ToString();
    _emerald.text = PlayerInfo.MaterialNum.Emerald.ToString();
  }
}
