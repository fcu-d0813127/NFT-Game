using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class MarketButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject prefabOfCheckMenu;
    public GameObject prefabOfInputUI;
    public void onClick(){
        if(this.gameObject.name == "NextPage"){
            GameObject.Find("MarketCanvas").GetComponent<MarketController>().setPage(1);
        }
        else if(this.gameObject.name == "PreviousPage"){
            GameObject.Find("MarketCanvas").GetComponent<MarketController>().setPage(-1);
        }
        else if(this.gameObject.name == "Yes"){
            GameObject marketcanvas = GameObject.Find("MarketCanvas");
            uint pagemode = marketcanvas.GetComponent<MarketController>().getPageMode();
            if(pagemode == 1){
                Destroy(GameObject.FindWithTag("Checkmenu"));
                marketcanvas.GetComponent<MarketController>().getAllowanceOf();
            }
            else if(pagemode == 2){
                Destroy(GameObject.FindWithTag("InputUi"));
                string InputPrice = GameObject.Find("InputField").GetComponent<TMP_InputField>().text;
                if(InputPrice == ""){
                    marketcanvas.GetComponent<MarketController>().ErrorMessage(2);
                }
                else{
                    marketcanvas.GetComponent<MarketController>().setListPrice(InputPrice);
                    marketcanvas.GetComponent<MarketController>().GetApproved();
                }
            }
            else if(pagemode == 3){
                Destroy(GameObject.FindWithTag("Checkmenu"));
                marketcanvas.GetComponent<MarketController>().Unlist();
            }
            marketcanvas.GetComponent<MarketController>().UnlockButton();
        }
        else if(this.gameObject.name == "No"){
            GameObject marketcanvas = GameObject.Find("MarketCanvas");
            uint pagemode = marketcanvas.GetComponent<MarketController>().getPageMode();
            if(pagemode == 2){
                Destroy(GameObject.FindWithTag("InputUi"));
            }
            else{
                Destroy(GameObject.FindWithTag("Checkmenu"));
            }
            marketcanvas.GetComponent<MarketController>().UnlockButton();
        }
        else if(this.gameObject.name == "Exit"){
            SceneManager.LoadScene("Initialization");
        }
        else if(this.gameObject.name == "ProductPage"){
            GameObject.Find("MarketCanvas").GetComponent<MarketController>().setPageMode(1);
            this.gameObject.GetComponent<Button>().interactable = false;
            GameObject.Find("ListPage").GetComponent<Button>().interactable = true;
            GameObject.Find("UnlistPage").GetComponent<Button>().interactable = true;
        }
        else if(this.gameObject.name == "ListPage"){
            GameObject.Find("MarketCanvas").GetComponent<MarketController>().setPageMode(2);
            this.gameObject.GetComponent<Button>().interactable = false;
            GameObject.Find("ProductPage").GetComponent<Button>().interactable = true;
            GameObject.Find("UnlistPage").GetComponent<Button>().interactable = true;
        }
        else if(this.gameObject.name == "UnlistPage"){
            GameObject.Find("MarketCanvas").GetComponent<MarketController>().setPageMode(3);
            this.gameObject.GetComponent<Button>().interactable = false;
            GameObject.Find("ListPage").GetComponent<Button>().interactable = true;
            GameObject.Find("ProductPage").GetComponent<Button>().interactable = true;
        }
        else if(this.gameObject.name == "Refresh"){
            GameObject.Find("MarketCanvas").GetComponent<MarketController>().RefreshPage();
        }
        else if(this.gameObject.name == "ErrorButton"){
            Destroy(GameObject.FindWithTag("ErrorMessage"));
        }
        else if(this.gameObject.name == "ApproveYes"){
            GameObject marketcanvas = GameObject.Find("MarketCanvas");
            uint pagemode = marketcanvas.GetComponent<MarketController>().getPageMode();
            marketcanvas.GetComponent<MarketController>().DestroyApporveUI();
            if(pagemode == 1){
                marketcanvas.GetComponent<MarketController>().ERC20Approve();
            }
            else if(pagemode == 2){
                marketcanvas.GetComponent<MarketController>().ERC721Approve();
            }
        }
        else if(this.gameObject.name == "ApproveNo"){
            GameObject marketcanvas = GameObject.Find("MarketCanvas");
            marketcanvas.GetComponent<MarketController>().DestroyApporveUI();
        }
        else{
            GameObject marketcanvas = GameObject.Find("MarketCanvas");
            uint pagemode = marketcanvas.GetComponent<MarketController>().getPageMode();
            if(pagemode == 1){ 
                string name = ("Product" + this.gameObject.name.Substring(6));
                GameObject CheckBox = Instantiate(prefabOfCheckMenu, new Vector3(85, 0, 0), Quaternion.Euler(0, 0, 0));
                CheckBox.transform.SetParent(GameObject.Find("MarketCanvas").transform, false);
                GameObject Marketcanvas = GameObject.Find("MarketCanvas");
                name = this.gameObject.name.Substring(6);
                Marketcanvas.GetComponent<MarketController>().setClickProduct(Convert.ToUInt32(name) - 1);
                GameObject.Find("ProductWantToBuyTMP").GetComponent<TextMeshProUGUI>().text = ("You want to buy" + '\n' 
                + "TokenId" + Marketcanvas.GetComponent<MarketController>().getProductTokenId() + '\n' + "Price" + Marketcanvas.GetComponent<MarketController>().getProductPrice());
            }
            else if(pagemode == 2){
                string name = ("Product" + this.gameObject.name.Substring(6));
                GameObject CheckBox = Instantiate(prefabOfInputUI, new Vector3(85, 0, 0), Quaternion.Euler(0, 0, 0));
                CheckBox.transform.SetParent(GameObject.Find("MarketCanvas").transform, false);
                GameObject Marketcanvas = GameObject.Find("MarketCanvas");
                name = this.gameObject.name.Substring(6);
                Marketcanvas.GetComponent<MarketController>().setClickProduct(Convert.ToUInt32(name) - 1);
                GameObject.Find("ListTMP").GetComponent<TextMeshProUGUI>().text = ("You want to list" + '\n' 
                + "TokenId" + Marketcanvas.GetComponent<MarketController>().getProductTokenId() + '\n' + 
                "Please input positive integer");
            }
            else if(pagemode == 3){
                string name = ("Product" + this.gameObject.name.Substring(6));
                GameObject CheckBox = Instantiate(prefabOfCheckMenu, new Vector3(85, 0, 0), Quaternion.Euler(0, 0, 0));
                CheckBox.transform.SetParent(GameObject.Find("MarketCanvas").transform, false);
                GameObject Marketcanvas = GameObject.Find("MarketCanvas");
                name = this.gameObject.name.Substring(6);
                Marketcanvas.GetComponent<MarketController>().setClickProduct(Convert.ToUInt32(name) - 1);
                GameObject.Find("ProductWantToBuyTMP").GetComponent<TextMeshProUGUI>().text = ("You want to Unlist" + '\n' 
                + "TokenId" + Marketcanvas.GetComponent<MarketController>().getProductTokenId() + '\n'+  "Price" + Marketcanvas.GetComponent<MarketController>().getProductPrice());
            }
            marketcanvas.GetComponent<MarketController>().LockButton();
        }
    }
}
