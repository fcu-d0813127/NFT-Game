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
    
    public GameObject prefab;
    public void onClick(){
        if(this.gameObject.name == "NextPage"){
            GameObject.Find("MarketCanvas").GetComponent<MarketController>().setPage(1);
        }
        else if(this.gameObject.name == "PreviousPage"){
            GameObject.Find("MarketCanvas").GetComponent<MarketController>().setPage(-1);
        }
        else if(this.gameObject.name == "Yes"){
            Debug.Log("You want to buy product!");
            Destroy(GameObject.FindWithTag("Checkmenu"));
            GameObject.Find("MarketCanvas").GetComponent<MarketController>().getAllowanceOf();
        }
        else if(this.gameObject.name == "No"){
            Debug.Log("You deny to buy product!");
            Destroy(GameObject.FindWithTag("Checkmenu"));
        }
        else if(this.gameObject.name == "Exit"){
            Debug.Log("Exit");
        }
        else{
            string name = ("ProductInfo" + this.gameObject.name.Substring(7));
            GameObject CheckBox = Instantiate(prefab, new Vector3(85, 0, 0), Quaternion.Euler(0, 0, 0));
            CheckBox.transform.SetParent(GameObject.Find("MarketCanvas").transform, false);
            GameObject.Find("ProductWantToBuyTMP").GetComponent<TextMeshProUGUI>().text = ("You want to buy" + '\n' 
            + GameObject.Find(name).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
            name = this.gameObject.name.Substring(7);
            GameObject.Find("MarketCanvas").GetComponent<MarketController>().setClickProduct(Convert.ToUInt32(name) - 1);
        }
    }
}
