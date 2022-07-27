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
    private static extern void ERC20balanceOf();
    [DllImport("__Internal")]
    private static extern void purchaseProduct(string _purchaseProductTokenId);
    [DllImport("__Internal")]
    private static extern void ERC20approve();
    [DllImport("__Internal")]
    private static extern void allowance(string _allowance, string _balance);
    [DllImport("__Internal")]
    private static extern void ERC721approve(string _ERC721approveTokenId);
    [DllImport("__Internal")]
    private static extern void listProduct(string _listProductTokenId, string _listProductPrice);
    [DllImport("__Internal")]
    private static extern void unlistProduct(string _unlistProductTokenId);
    [DllImport("__Internal")]
    private static extern void ProductListFromOwnerOf();
    [DllImport("__Internal")]
    private static extern void ERC721balanceOf();
    [DllImport("__Internal")]
    private static extern void tokenOfOwnerByIndex(ulong _boundary);
    [DllImport("__Internal")]
    private static extern void tokenStatOf(string _productTokenId, UInt32 _pagemode);
    [DllImport("__Internal")]
    private static extern void getApproved(string _getApprovedtokenId);
    internal int page = 0;
    internal string balance;
    internal uint ClickProduct = 0;
    internal uint PageMode;
    internal string ERC721balance;
    internal string[] TokenIdTemp;
    internal ProductTokenStat[] TokenStatTemp;
    internal string listPrice;
    [SerializeField]private GameObject prefebOfProduct;
    [SerializeField]private GameObject prefebOfErrorMessage;
    [SerializeField]private GameObject prefebOfApprove;
    [SerializeField]private GameObject prefebOfResponse;
    internal GameObject[] Product = new GameObject[10];
    internal int ProductLength = 0;
    internal GameObject ProductManager;
    internal GameObject PreviousButton;
    internal GameObject NextButton;
    internal GameObject IntroForProduct;
    internal GameObject _productPageButton;
    internal GameObject _listPageButton;
    internal GameObject _unlistPageButton;
    internal GameObject _refreshButton;
    internal GameObject _approveUI;
    private GameObject _responseUI;
    // Start is called before the first frame update
    void Start()
    {
        page = 0;
        PageMode = 1;
        PreviousButton = GameObject.Find("PreviousPage");
        NextButton = GameObject.Find("NextPage");
        ProductManager = GameObject.Find("ProductManager");
        IntroForProduct = GameObject.Find("IntroForProduct");
        _productPageButton = GameObject.Find("ProductPage");
        _listPageButton = GameObject.Find("ListPage");
        _unlistPageButton = GameObject.Find("UnlistPage");
        _refreshButton = GameObject.Find("Refresh");
        ERC20balanceOf();
        ERC721balanceOf();
        ProductListFromOwnerOf();
        ProductList();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SetProductList(string productlist){//讀資料 存到資料結構裡
        ProductInfo.Product = ProductInfo.CreateProductList(productlist);
        string ProductTokenId = "";
        for(uint i = 0; i < ProductInfo.Product.Length; i++){
            ProductTokenId += (ProductInfo.Product[i].tokenId + ",");
        }
        tokenStatOf(ProductTokenId, 1);

    }
    private void SetProductTokenStatList(string TokenStatList){
        ProductInfo.ProductStat = ProductInfo.CreateProductTokenStatList(TokenStatList);
        BrowseProduct();
    }
    private void setBalanceOf(string _balance){
        balance = _balance;
        BalanceScene();
    }
    private void BalanceScene(){//用於螢幕上顯示
        GameObject.Find("Dollar").GetComponent<TextMeshProUGUI>().text = ("Balance:" + balance);
    }
    private void BrowseProduct(){//用於螢幕上顯示資料數據
        foreach(GameObject i in Product){
            Destroy(i);
        }
        if(PageMode == 1){
            ProductLength = ProductInfo.Product.Length - page * 10;
            if(ProductLength > 10){
                ProductLength = 10;
            }
            IntroForProduct.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>".PadRight(23) + "TokenId".PadRight(26) + "Rare".PadRight(25) + "Level".PadRight(24) + "Price".PadRight(24) + "</mspace>";
            for(int i = 0; i < ProductLength; i++){
                Product[i] = Instantiate(prefebOfProduct);
                Product[i].transform.SetParent(ProductManager.transform, false);
                Product[i].name = "Product" + (i + 1).ToString();
                Product[i].transform.GetChild(0).gameObject.name = "Button" + (i + 1).ToString();
            }
            if(ProductLength < 6){
                ProductManager.GetComponent<RectTransform>().sizeDelta = new Vector2(103f, 575f);
                ProductManager.GetComponent<RectTransform>().anchoredPosition = new Vector2(-631f, 0f);
            }
            else{
                ProductManager.GetComponent<RectTransform>().sizeDelta = new Vector2(103f, 115f * (ProductLength));
                ProductManager.GetComponent<RectTransform>().anchoredPosition = new Vector2(-631f, -58f * (ProductLength - 5));
            }
            for(int i = 0; i < ProductLength; i++){
                var productPosition = Product[i].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.position;
                productPosition.x = 536f;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.position = productPosition;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>" + ProductInfo.Product[i + page * 10].tokenId + "</mspace>";
                productPosition = Product[i].transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.position;
                productPosition.x = 833f;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.position = productPosition;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>" + ProductInfo.ProductStat[i + page * 10].rarity + "</mspace>";
                productPosition = Product[i].transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.position;
                productPosition.x = 1133f;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.position = productPosition;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>" + ProductInfo.ProductStat[i + page * 10].level + "</mspace>";
                productPosition = Product[i].transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform.position;
                productPosition.x = 1427f;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform.position = productPosition;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>" + ProductInfo.Product[i + page * 10].price + "</mspace>";
            }
        }
        else if(PageMode == 2){
            ProductLength = TokenIdTemp.Length - page * 10;
            if(ProductLength > 10){
                ProductLength = 10;
            }
            IntroForProduct.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>".PadRight(24) + "TokenId".PadRight(32) + "Rare".PadRight(30) + "Level".PadRight(30) + "</mspace>";
            for(int i = 0; i < ProductLength; i++){
                Product[i] = Instantiate(prefebOfProduct);
                Product[i].transform.SetParent(ProductManager.transform, false);
                Product[i].name = "Product" + (i + 1).ToString();
                Product[i].transform.GetChild(0).gameObject.name = "Button" + (i + 1).ToString();
            }
            if(ProductLength < 6){
                ProductManager.GetComponent<RectTransform>().sizeDelta = new Vector2(103f, 575f);
                ProductManager.GetComponent<RectTransform>().anchoredPosition = new Vector2(-631f, 0f);
            }
            else{
                ProductManager.GetComponent<RectTransform>().sizeDelta = new Vector2(103f, 115f * (ProductLength));
                ProductManager.GetComponent<RectTransform>().anchoredPosition = new Vector2(-631f, -58f * (ProductLength - 5));
            }
            for(int i = 0; i < ProductLength; i++){
                var productPosition = Product[i].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.position;
                productPosition.x = 550f;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.position = productPosition;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>" + TokenIdTemp[i + page * 10] + "</mspace>";
                productPosition = Product[i].transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.position;
                productPosition.x = 912f;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.position = productPosition;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>" + TokenStatTemp[i + page * 10].rarity + "</mspace>";
                productPosition = Product[i].transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.position;
                productPosition.x = 1274f;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.position = productPosition;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>" + TokenStatTemp[i + page * 10].level + "</mspace>";
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "";
            }
        }
        else if(PageMode == 3){
            ProductLength = UnlistProductInfo.UnlistProduct.Length - page * 10;
            if(ProductLength > 10){
                ProductLength = 10;
            }
            IntroForProduct.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>".PadRight(23) + "TokenId".PadRight(26) + "Rare".PadRight(25) + "Level".PadRight(24) + "Price".PadRight(24) + "</mspace>";
            for(int i = 0; i < ProductLength; i++){
                Product[i] = Instantiate(prefebOfProduct);
                Product[i].transform.SetParent(ProductManager.transform, false);
                Product[i].name = "Product" + (i + 1).ToString();
                Product[i].transform.GetChild(0).gameObject.name = "Button" + (i + 1).ToString();
            }
            if(ProductLength < 6){
                ProductManager.GetComponent<RectTransform>().sizeDelta = new Vector2(103f, 575f);
                ProductManager.GetComponent<RectTransform>().anchoredPosition = new Vector2(-631f, 0f);
            }
            else{
                ProductManager.GetComponent<RectTransform>().sizeDelta = new Vector2(103f, 115f * (ProductLength));
                ProductManager.GetComponent<RectTransform>().anchoredPosition = new Vector2(-631f, -58f * (ProductLength - 5));
            }
            for(int i = 0; i < ProductLength; i++){
                var productPosition = Product[i].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.position;
                productPosition.x = 536f;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.position = productPosition;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>" + UnlistProductInfo.UnlistProduct[i + page * 10].tokenId + "</mspace>";
                productPosition = Product[i].transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.position;
                productPosition.x = 833f;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.position = productPosition;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>" + UnlistProductInfo.UnlistProductStat[i + page * 10].rarity + "</mspace>";
                productPosition = Product[i].transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.position;
                productPosition.x = 1133f;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.position = productPosition;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>" + UnlistProductInfo.UnlistProductStat[i + page * 10].level + "</mspace>";
                productPosition = Product[i].transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform.position;
                productPosition.x = 1427f;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform.position = productPosition;
                Product[i].transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>" + UnlistProductInfo.UnlistProduct[i + page * 10].price + "</mspace>";
            }
        }
    }
    internal void Purchase(){
        purchaseProduct(getProductTokenId());
    }
    internal void ERC20Approve(){//讓玩家允許操作ERC20 approve
        ERC20approve();
    }
    private void PurchaseMessage(){//更新Product用
        ERC20balanceOf();
        ProductList();
        page = 0;
        RefreshPage();
    }
    private void setUnlistProductList(string UnlistProductList){
        UnlistProductInfo.UnlistProduct = UnlistProductInfo.CreateUnlistProductList(UnlistProductList);
        string ProductTokenId = "";
        for(uint i = 0; i < UnlistProductInfo.UnlistProduct.Length; i++){
            ProductTokenId += (UnlistProductInfo.UnlistProduct[i].tokenId + ",");
        }
        tokenStatOf(ProductTokenId, 3);
    }
    private void SetUnlistProductTokenStatList(string TokenStatList){
        UnlistProductInfo.UnlistProductStat = UnlistProductInfo.CreateUnlistProductTokenStatList(TokenStatList);
        BrowseProduct();
    }
    internal void Unlist(){
        unlistProduct(getProductTokenId());
    }
    private void UnlistMessage(){//更新UnlistProduct用
        ProductListFromOwnerOf();
        page = 0;
        RefreshPage();
    }
    private void getERC721balanceOf(string balance){
        tokenOfOwnerByIndex(Convert.ToUInt64(balance));
    }
    private void getListTokenId(string outputstring){
        TokenIdTemp = outputstring.Split(',');
        outputstring = outputstring + ',';
        tokenStatOf(outputstring, 2);
    }
    private void SetListProductTokenStatList(string TokenStatList){
        TokenStatTemp = UnlistProductInfo.CreateUnlistProductTokenStatList(TokenStatList);
        BrowseProduct();
    }
    private void ListProduct(){
        listProduct(getProductTokenId(), listPrice);
    }
    internal void ERC721Approve(){
        ERC721approve(getProductTokenId());
    }
    
    private void ListMessage(){
        ERC721balanceOf();
        page = 0;
        RefreshPage();
    }
    internal string getProductTokenId(){//拿現在按下按鈕的商品資訊 下面的function也是
        if(PageMode == 1){
            return ProductInfo.Product[page * 10 + ClickProduct].tokenId;
        }
        else if(PageMode == 2){
            return TokenIdTemp[page * 10 + ClickProduct];
        }
        else{
            return UnlistProductInfo.UnlistProduct[page * 10 + ClickProduct].tokenId;
        }
    }
    internal string getProductPrice(){
        if(PageMode == 1){
            return ProductInfo.Product[page * 10 + ClickProduct].price;
        }
        else{
            return UnlistProductInfo.UnlistProduct[page * 10 + ClickProduct].price;
        }
    }
    internal void getAllowanceOf(){
        allowance(getProductPrice(), balance); 
    }
    internal void setPage(int a){//設定市場在第幾頁
        page+=a;
        if(PageMode == 1){
            if(page == 0){
                PreviousButton.GetComponent<Button>().interactable = false;
            }
            else{
                PreviousButton.GetComponent<Button>().interactable = true;
            }
            if((ProductInfo.Product.Length - page * 10) <= 10){
                NextButton.GetComponent<Button>().interactable = false;
            }
            else{
                NextButton.GetComponent<Button>().interactable = true;
            }
        }
        else if(PageMode == 2){
            if(page == 0){
                PreviousButton.GetComponent<Button>().interactable = false;
            }
            else{
                PreviousButton.GetComponent<Button>().interactable = true;
            }
            if((TokenIdTemp.Length - page * 10) <= 10){
                NextButton.GetComponent<Button>().interactable = false;
            }
            else{
                NextButton.GetComponent<Button>().interactable = true;
            }
        }
        else if(PageMode == 3){
            if(page == 0){
                PreviousButton.GetComponent<Button>().interactable = false;
            }
            else{
                PreviousButton.GetComponent<Button>().interactable = true;
            }
            if((UnlistProductInfo.UnlistProduct.Length - page * 10) <= 10){
                NextButton.GetComponent<Button>().interactable = false;
            }
            else{
                NextButton.GetComponent<Button>().interactable = true;
            }
        }
        BrowseProduct();
    }
    internal void setClickProduct(uint a){//設定按到第幾個Button 用於purchase使用
        ClickProduct = a;
    }
    internal void ErrorMessage(int ErrorEvent){
        if(ErrorEvent == 1){
            GameObject ErrorBox = Instantiate(prefebOfErrorMessage, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            ErrorBox.transform.SetParent(GameObject.Find("MarketCanvas").transform, false);
            ErrorBox.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "You don't have enough money!";
            UnlockButton();
        }
        else if(ErrorEvent == 2){
            GameObject ErrorBox = Instantiate(prefebOfErrorMessage, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            ErrorBox.transform.SetParent(GameObject.Find("MarketCanvas").transform, false);
            ErrorBox.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "You don't input any number!";
            UnlockButton();
        }
    }

    internal void RefreshPage(){
        if(PageMode == 1){
            ProductList();
        }
        else if(PageMode == 2){
            ERC721balanceOf();
        }
        else if(PageMode == 3){
            ProductListFromOwnerOf();
        }
        ERC20balanceOf();
        page = 0;
    }

    internal void setPageMode(uint a){
        PageMode = a;
        page = 0;
        BrowseProduct();
    }
    internal uint getPageMode(){
        return PageMode;
    }
    internal void setListPrice(string inputPrice){
        listPrice = inputPrice;
    }

    internal void LockButton(){
        _productPageButton.GetComponent<Button>().interactable = false;
        _listPageButton.GetComponent<Button>().interactable = false;
        _unlistPageButton.GetComponent<Button>().interactable = false;
        _refreshButton.GetComponent<Button>().interactable = false;
        for(int i = 0; i < ProductLength; i++){
            Product[i].transform.GetChild(0).gameObject.GetComponent<Button>().interactable = false;
        }
    }

    internal void UnlockButton(){
        var x = getPageMode();
        if(x == 1){
            _listPageButton.GetComponent<Button>().interactable = true;
            _unlistPageButton.GetComponent<Button>().interactable = true;
            _refreshButton.GetComponent<Button>().interactable = true;
        }
        else if(x == 2){
            _productPageButton.GetComponent<Button>().interactable = true;
            _unlistPageButton.GetComponent<Button>().interactable = true;
            _refreshButton.GetComponent<Button>().interactable = true;
        }
        else if(x == 3){
            _productPageButton.GetComponent<Button>().interactable = true;
            _listPageButton.GetComponent<Button>().interactable = true;
            _refreshButton.GetComponent<Button>().interactable = true;
        }
        for(int i = 0; i < ProductLength; i++){
            Product[i].transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
        }
    }

    internal void GetPlayerApprove(){
        _approveUI = Instantiate(prefebOfApprove,  new Vector3(85, 0, 0), Quaternion.Euler(0, 0, 0));
        _approveUI.transform.SetParent(GameObject.Find("MarketCanvas").transform, false);
        _approveUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "You need to allow us to operate your ERC20/ERC721";
    }

    internal void DestroyApporveUI(){
        Destroy(_approveUI);
    }

    internal void GetApproved(){
        getApproved(getProductTokenId());
    }

    internal void SignResponse(int signEvent){
        if(signEvent == 1){
            _responseUI = Instantiate(prefebOfResponse);
            _responseUI.transform.SetParent(GameObject.Find("MarketCanvas").transform, false);
            _responseUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Now, you sign ERC20 approve suecessfully!\nYou can sign the purchase!";
        }
        else if(signEvent == 2){
            _responseUI = Instantiate(prefebOfResponse);
            _responseUI.transform.SetParent(GameObject.Find("MarketCanvas").transform, false);
            _responseUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Now, you purchase product suecessfully!\nNow, you can check item in backpack!";
        }
        else if(signEvent == 3){
            _responseUI = Instantiate(prefebOfResponse);
            _responseUI.transform.SetParent(GameObject.Find("MarketCanvas").transform, false);
            _responseUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Now, you sign ERC721 approve suecessfully!\nYou can sign the list!";
        }
        else if(signEvent == 4){
            _responseUI = Instantiate(prefebOfResponse);
            _responseUI.transform.SetParent(GameObject.Find("MarketCanvas").transform, false);
            _responseUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Now, you list product suecessfully!\nYou can check the product in Unlist page!";
        }
        else if(signEvent == 5){
            _responseUI = Instantiate(prefebOfResponse);
            _responseUI.transform.SetParent(GameObject.Find("MarketCanvas").transform, false);
            _responseUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Now, you unlist product suecessfully!\nYou can check the item in backpack!";
        }
    }

    internal void DestroyResponseUI(){
        Destroy(_responseUI);
    }
}
