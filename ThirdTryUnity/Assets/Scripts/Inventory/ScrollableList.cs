using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollableList : MonoBehaviour {
   
    private GameObject _scrollablePanel;
    private ScrollRect _scroll;

    public void Start()
    {
        _scrollablePanel = GameObject.Find("ScrollableListPanel");
        _scroll = _scrollablePanel.GetComponent<ScrollRect>();
    }
    private void Update()
    {
      
    }
    public void MoveScrollBar(string button)
    {
       
        if (button == "right")
        {
           
        }
        else if (button =="left")
        {
            
        }
    }
}
