using UnityEngine;
public class UnlistProductInfo{
    // Start is called before the first frame update
    public static Product[] UnlistProduct;
    public static ProductTokenStat[] UnlistProductStat;
    public static Product[] CreateUnlistProductList(string unlistproductlistJson){
        string[] JsonTemp = unlistproductlistJson.Split(',');
        Product[] UnlistProductList = new Product[JsonTemp.Length / 3];
        int count = 0;
        string tokenIdTemp = null;
        string ownerTemp = null;
        string priceTemp = null;
        foreach(var x in JsonTemp){
            if(count % 3 == 0){
                tokenIdTemp = x;
            }   
            else if(count % 3 == 1){
                ownerTemp = x;
            }
            else if(count % 3 == 2){
                priceTemp = x;
                Product NewProduct = new Product(tokenIdTemp, ownerTemp, priceTemp);
                UnlistProductList[count / 3] = NewProduct;
            }
            count++;
        }
        return UnlistProductList;
    }
    public static ProductTokenStat[] CreateUnlistProductTokenStatList(string TokenStatJson){
        string[] TokenStat = TokenStatJson.Split('{', '}');
        uint count = 0;
        ProductTokenStat[] ProductTokenStatList = new ProductTokenStat[TokenStat.Length / 2];
        for(uint i = 0; i < TokenStat.Length; i++){
            if(string.Compare(TokenStat[i], "") != 0 && string.Compare(TokenStat[i], ",") != 0){
                Debug.Log(TokenStat[i]);
                string TokenStatTemp = "{" + TokenStat[i] + "}";
                Debug.Log(TokenStatTemp);
                ProductTokenStatList[count++] = JsonUtility.FromJson<ProductTokenStat>(TokenStatTemp);
            }
        }
        return ProductTokenStatList;
    }
}