using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeMarkers : MonoBehaviour {
    public Color FocusedMenuButtonColor = new Color(1f, 0.95f, 0.5f, 1);

    private SpriteRenderer _leftEye;
    private SpriteRenderer _rightEye;
    private SpriteRenderer _nose;
    private SpriteRenderer _mouth;
    private SpriteRenderer _head;
    private SpriteRenderer _bottom;
    
    // Use this for initialization
    void Start () {
        _leftEye = GameObject.Find("LeftEye").GetComponent<SpriteRenderer>();
        _rightEye = GameObject.Find("RightEye").GetComponent<SpriteRenderer>();
        _nose = GameObject.Find("Nose").GetComponent<SpriteRenderer>();
        _mouth = GameObject.Find("Mouth").GetComponent<SpriteRenderer>();
        _head = GameObject.Find("Head").GetComponent<SpriteRenderer>();
        _bottom = GameObject.Find("Bottom").GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            checkTouch(Input.mousePosition);
        }
        if (Input.touchCount > 0)
        {
            foreach (var touch in Input.touches)
            {
                checkTouch(touch.position);
            }
        }
    }

    void checkTouch(Vector3 pos)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        var hit = Physics2D.OverlapPoint(touchPos);

        if (hit)
        {
            SetMarkerBackToDefault(); //reset everything before changing the focused marker color.

            if (hit.transform.gameObject.name == "LeftEye")
            {
                hit.gameObject.GetComponent<SpriteRenderer>().color = FocusedMenuButtonColor;
            }
            else if (hit.transform.gameObject.name == "RightEye")
            {
                hit.gameObject.GetComponent<SpriteRenderer>().color = FocusedMenuButtonColor;
            }
            else if (hit.transform.gameObject.name == "Nose")
            {
                hit.gameObject.GetComponent<SpriteRenderer>().color = FocusedMenuButtonColor;
            }
            else if (hit.transform.gameObject.name == "Mouth")
            {
                hit.gameObject.GetComponent<SpriteRenderer>().color = FocusedMenuButtonColor;
            }
            else if (hit.transform.gameObject.name == "Head")
            {
                hit.gameObject.GetComponent<SpriteRenderer>().color = FocusedMenuButtonColor;
            }
            else if (hit.transform.gameObject.name == "Bottom")
            {
                hit.gameObject.GetComponent<SpriteRenderer>().color = FocusedMenuButtonColor;
            }
        }
    }

    private void SetMarkerBackToDefault()
    {
        //todo - check if sprite is the default before changing the sprite color..
        Color defaultColor = new Color(1, 1, 1, .5f);
        _leftEye.color = defaultColor;
        _rightEye.color = defaultColor;
        _nose.color = defaultColor;
        _mouth.color = defaultColor;
        _head.color = defaultColor;
        _bottom.color = defaultColor;
    }
}
