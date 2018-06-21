using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuitPanelButtons : MonoBehaviour {

    private GameObject _scrollableList;
    private GameObject[] allChildren;
    // Use this for initialization
    void Start () {
        _scrollableList = GameObject.Find("ScrollableListPanel");     

    }

    public void ShaderButtonClick()
    {
        if (_scrollableList != null)
        {         
            if (_scrollableList.activeSelf)
            {
                _scrollableList.SetActive(false);
            }
            else
            {
                _scrollableList.SetActive(true);
            }
        }
    }
}
