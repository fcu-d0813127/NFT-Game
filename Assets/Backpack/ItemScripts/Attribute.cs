public class Attribute {
  public float Atk;
  public float Matk;
  public float Def;
  public float Mdef;
  public float Cri;
  public float CriDmgRatio;

  public static Attribute CopyAttribute(Attribute attribute) {
    Attribute copyAttribute = new Attribute();
    copyAttribute.Atk = attribute.Atk;
    copyAttribute.Matk = attribute.Matk;
    copyAttribute.Def = attribute.Def;
    copyAttribute.Mdef = attribute.Mdef;
    copyAttribute.Cri = attribute.Cri;
    copyAttribute.CriDmgRatio = attribute.CriDmgRatio;
    return copyAttribute;
  }
}
