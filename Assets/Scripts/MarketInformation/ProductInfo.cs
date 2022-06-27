using UnityEngine;
public class ProductInfo{
    // Start is called before the first frame update
    public static Product[] Product;

    public static Product[] CreateProductList(string productlistJson){
        string[] JsonTemp = productlistJson.Split(',');
        Product[] ProductList = new Product[JsonTemp.Length / 3];
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
                ProductList[count / 3] = NewProduct;
            }
            count++;
        }
        return ProductList;
    }
}
