using UnityEngine;

[System.Serializable]
public class ProductAttribute{
    // Start is called before the first frame update
    public string atk;
    public string matk;
    public string def;
    public string mdef;
    public string cri;
    public string criDmgRatio;

    public ProductAttribute(string a, string b, string c, string d, string e, string f){
        atk = a;
        matk = b;
        def = c;
        mdef = d;
        cri = e;
        criDmgRatio = f;
    }
}
