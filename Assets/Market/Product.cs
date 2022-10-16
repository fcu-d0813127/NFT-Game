using UnityEngine;

[System.Serializable]
public class Product{
    public string tokenId;
    public string owner;
    public string price;
    
    public Product(string a, string b, string c){
        tokenId = a;
        owner = b;
        price = c;
    }
    
}
