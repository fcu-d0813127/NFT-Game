[System.Serializable]
public class PlayerAttribute : Attribute {
  public PlayerAttribute(PlayerAbility playerAbility, int[] equipmentItemAttribute) {
    int str = playerAbility.str;
    int intllegence = playerAbility.intllegence;
    int vit = playerAbility.vit;
    int dex = playerAbility.dex;
    int luk = playerAbility.luk;
    int eAtk = equipmentItemAttribute[0];
    int eMatk = equipmentItemAttribute[1];
    int eDef = equipmentItemAttribute[2];
    int eMdef = equipmentItemAttribute[3];
    int eCri = equipmentItemAttribute[4];
    int eCriDmgRatio = equipmentItemAttribute[5];
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
        (8000 * dex / (dex + 250) +
        2000 * luk / (luk + 250) +
        (1 + 0.25f * dex / (dex + 250)) * eCri) / 20000;
    this.CriDmgRatio = 1.5f + (1 + 0.25f * luk / (luk + 250)) * eCriDmgRatio;
  }
}
