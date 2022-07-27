using UnityEngine;

public class ProductTokenStat{
    public string rarity;
    public string part;
    public string level;
    ProductAttribute attribute;
    public string[] skills = new string[3];

    public ProductTokenStat(string a, string b, string c, ProductAttribute d, string[] e){
        rarity = a;
        part = b;
        level = c;
        attribute = d;
        skills = e;
    }
}
