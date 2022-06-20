[System.Serializable]
public class PlayerPanel {
  public float Atk;
  public float Matk;
  public float Def;
  public float Mdef;
  public float Cri;
  public float CriDmgRatio;

  public PlayerPanel(PlayerAbility playerAbility, int[] equipmentItemAbility) {
    int str = playerAbility.Str;
    int intllegence = playerAbility.Intllegence;
    int vit = playerAbility.Vit;
    int dex = playerAbility.Dex;
    int luk = playerAbility.Luk;
    int eAtk = equipmentItemAbility[0];
    int eMatk = equipmentItemAbility[1];
    int eDef = equipmentItemAbility[2];
    int eMdef = equipmentItemAbility[3];
    int eCri = equipmentItemAbility[4];
    int eCriDmgRatio = equipmentItemAbility[5];
    this.Atk = 10000 * str / (str + 250) + (1 + 0.25f * str / (str + 250)) * eAtk;
    this.Matk =
        10000 * intllegence / (intllegence + 250) +
        (1 + 0.25f * intllegence / (intllegence + 250)) * eMatk;
    this.Def =
        5000 * vit / (vit + 250) +
        3000 * dex / (dex + 250) +
        2000 * str / (str + 250) +
        (1 + 0.25f * vit / (vit + 250)) * eDef;
    this.Mdef =
        5000 * vit / (vit + 250) +
        3000 * dex / (dex + 250) +
        2000 * intllegence / (intllegence + 250) +
        (1 + 0.25f * vit / (vit + 250)) * eMdef;
    this.Cri =
        8000 * dex / (dex + 250) +
        2000 * luk / (luk + 250) +
        (1 + 0.25f * dex / (dex + 250)) * eCri;
    this.CriDmgRatio = 1.5f + (1 + 0.25f * luk / (luk + 250)) * eCriDmgRatio;
  }
}
