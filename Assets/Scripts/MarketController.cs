using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
public class MarketController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ProductList();
    [DllImport("__Internal")]
    private static extern void OnLogin();
    [DllImport("__Internal")]
    private static extern void GetBalanceOf();
    [DllImport("__Internal")]
    private static extern void PurchaseProduct(string _tokenId);
    [DllImport("__Internal")]
    private static extern void GetApprove();
    [DllImport("__Internal")]
    private static extern void GetAllowanceOf(string _allowance, string _balance);
    internal int page = 0;
    internal string balance;
    internal uint ClickProduct = 0;
    [SerializeField]private GameObject prefebOfProduct;
    [SerializeField]private GameObject prefebOfButton;
    internal GameObject[] Product = new GameObject[10];
    internal GameObject[] ProductOfButton = new GameObject[10];
    internal int ProductLength = 0;
    float LastTime, TimeBetween = 1f;
    internal GameObject TotalManager;
    internal GameObject ProductInfoManager;
    internal GameObject ButtonManager;
    // Start is called before the first frame update
    void Start()
    {
        page = 0;
        LastTime = Time.time;
        TotalManager = GameObject.Find("TotalManager");
        ProductInfoManager = GameObject.Find("ProductInfoManager");
        ButtonManager = GameObject.Find("ButtonManager");
        OnLogin();//OnLogin到時候可砍
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("a")){//提供一個方法刷新資料
            if(Time.time - LastTime >= TimeBetween){
                ProductList();
                getBalanceOf();
                LastTime = Time.time;
            }
        }
    }
    private void ConnectAccount(string account){//merge後可砍 GetBalanceOf可以丟到Start
        Debug.Log("Connect");
        GetBalanceOf();
    }
    private void SetProductList(string productlist){//讀資料 存到資料結構裡
        ProductInfo.Product = ProductInfo.CreateProductList(productlist);
        BrowseProduct();
    }
    private void setBalanceOf(string _balance){
        balance = _balance;
        getBalanceOf();
    }
    private void getBalanceOf(){//用於螢幕上顯示
        Debug.Log("Dollar!");
        GameObject.Find("Dollar").GetComponent<TextMeshProUGUI>().text = ("Balance:" + balance);
    }
    private void BrowseProduct(){//用於螢幕上顯示資料數據
        foreach(GameObject i in Product){
            Destroy(i);
        }
        foreach(GameObject i in ProductOfButton){
            Destroy(i);
        }
        ProductLength = ProductInfo.Product.Length - page * 10;
        if(ProductLength > 10)
            ProductLength = 10;
        for(int i = 0; i < ProductLength; i++){
            Product[i] = Instantiate(prefebOfProduct);
            Product[i].transform.SetParent(ProductInfoManager.transform, false);
            Product[i].name = "ProductInfo" + (i + 1).ToString();
            ProductOfButton[i] = Instantiate(prefebOfButton);
            ProductOfButton[i].transform.SetParent(ButtonManager.transform, false);
            ProductOfButton[i].name = "Product" + (i + 1).ToString();
        }
        if(ProductLength < 4){
            TotalManager.GetComponent<RectTransform>().sizeDelta = new Vector2(715f, 223f * 3);
            TotalManager.GetComponent<RectTransform>().anchoredPosition = new Vector2(-60.60f, -5f);
            ProductInfoManager.GetComponent<RectTransform>().sizeDelta = new Vector2(459.13f, 222f * 3);
            ProductInfoManager.GetComponent<RectTransform>().anchoredPosition = new Vector2(305f, -332f);
            ButtonManager.GetComponent<RectTransform>().sizeDelta = new Vector2(156.16f, 222f * 3);
            ButtonManager.GetComponent<RectTransform>().anchoredPosition = new Vector2(630f, -332f);
        }
        else{
            TotalManager.GetComponent<RectTransform>().sizeDelta = new Vector2(715f, 223f * ProductLength);
            TotalManager.GetComponent<RectTransform>().anchoredPosition = new Vector2(-60.60f, -5f + (- 111f * (ProductLength - 3)));
            ProductInfoManager.GetComponent<RectTransform>().sizeDelta = new Vector2(459.13f, 222f * ProductLength);
            ProductInfoManager.GetComponent<RectTransform>().anchoredPosition = new Vector2(305f, -332f + (-111f * (ProductLength - 3)));
            ButtonManager.GetComponent<RectTransform>().sizeDelta = new Vector2(156.16f, 222f * ProductLength);
            ButtonManager.GetComponent<RectTransform>().anchoredPosition = new Vector2(630f, -332f + (-111f * (ProductLength - 3)));
        }
        for(int i = 0; i < ProductLength; i++){
            Product[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ("TokenId:" + ProductInfo.Product[i + page * 10].tokenId + '\n' 
            + "Price:" + ProductInfo.Product[i + page * 10].price);
        }
    }
    internal void Purchase(){
        PurchaseProduct(getProductTokenId());
    }
    internal void ERC20Approve(){//讓玩家允許操作ERC20 approve
        Debug.Log("Aproving!");
        GetApprove();
    }
    private void PurchaseMessage(){//更新用
        Debug.Log("Purchase Suecess!");
        GetBalanceOf();
        ProductList();
        page = 0;
    }
    private string getProductTokenId(){//拿現在按下按鈕的商品資訊 下面的function也是
        return ProductInfo.Product[page * 10 + ClickProduct].tokenId;
    }
    private string getProductPrice(){
        return ProductInfo.Product[page * 10 + ClickProduct].price;
    }
    internal void getAllowanceOf(){
        Debug.Log("Allowance!");
        Debug.Log("Price" + getProductPrice() + "Balance" + balance);
        GetAllowanceOf(getProductPrice(), balance); 
    }
    internal void setPage(int a){//設定市場在第幾頁
        page+=a;
        if(page < 0){
            page = 0;
        }
        else if(ProductInfo.Product.Length <= page * 10){
            page--;
        }
        else{
            BrowseProduct();
        }
    }
    internal void setClickProduct(uint a){//設定按到第幾個Button 用於purchase使用
        ClickProduct = a;
    }
    internal void ErrorMessage(){
        Debug.Log("You don't have enough money!");
    }
}
