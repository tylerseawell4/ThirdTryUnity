using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapManager : MonoBehaviour
{
    private float _timeBetweenTaps = 0.25f; // Half a second before reset
    private int _tapCount = 0;
    public bool _doubleTap;
    public bool _singleTap;
    private float holdTime = .75f; //or whatever
    private float acumTime = 0;
    private bool _isHolding;
    public bool _holdActivated;

    // Use this for initialization
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //double tap
            if (_timeBetweenTaps > 0 && _tapCount == 1/*Number of Taps you want Minus One*/)
            {
                //double tap
                Debug.Log("Double Activated");
                _tapCount = 0;
                //gets set to true, so dash can be started in fixedupdate()
                _doubleTap = true;
            }
            else
            {
                _timeBetweenTaps = 0.25f;
                _tapCount += 1;
            }
        }

        if (Input.GetMouseButton(0))
        {
            _isHolding = true;

            acumTime += 1 * Time.deltaTime;
            //Debug.Log("Holding");

            if (acumTime >= holdTime && !_holdActivated)
            {
                _holdActivated = true;
                Debug.Log("Held Activated");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            acumTime = 0;
            _isHolding = false;
            _holdActivated = false;
            //Debug.Log("Not Holding");
        }

        if (_timeBetweenTaps > 0)
            _timeBetweenTaps -= 1 * Time.deltaTime;
        //single touch, should not fire until time between first tap and second tap is longer than .25 seconds (_timebetweentaps is less than 0)
        else if (_timeBetweenTaps < 0 && _tapCount > 0 && !_isHolding)
        {
            _tapCount = 0;
            _timeBetweenTaps = 0.25f;
            _singleTap = true;
            Debug.Log("Single Activated");
        }
        else
            _tapCount = 0;
    }
}
