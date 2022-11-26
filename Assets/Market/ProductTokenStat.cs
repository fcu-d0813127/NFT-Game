using UnityEngine;

public class ProductTokenStat{
    public string rarity;
    public string part;
    public string level;
    public int[] attribute = new int[6];
    public int[] skills = new int[3];

    public ProductTokenStat(string rarityJson, string partJson, string levelJson, int[] attributeJson, int[] skillsJson){
        rarity = rarityJson;
        part = partJson;
        level = levelJson;
        attribute = attributeJson;
        skills = skillsJson;
    }
}
