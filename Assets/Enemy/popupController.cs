using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class popupController : MonoBehaviour
{
    [SerializeField] float _timeRecord; 
    [SerializeField] float _allTime = 0.4f; 
    [SerializeField] float _objFontSize = 3f ;
    [SerializeField] float _bigger = 0.08f;
    // Start is called before the first frame update
    void Start()
    {
        
        //GetComponent<TMP_Text>().fontSize = 10;
    }

    // Update is called once per frame
    void Update()
    {
        float posX = GetComponent<RectTransform>().anchoredPosition.x;
        float posY = GetComponent<RectTransform>().anchoredPosition.y;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(posX,posY+0.02f);
        _objFontSize += _bigger;
        GetComponent<TMP_Text>().fontSize = _objFontSize;
        _timeRecord += Time.deltaTime;
        if(_timeRecord <= (_allTime/2))
            _objFontSize += _bigger;
        else
            _objFontSize -= _bigger;
        if(_timeRecord >= _allTime){
            Destroy(this.gameObject);
        }
    }

    public void getDamage(int damage){
        Debug.Log(damage);
    }
}
