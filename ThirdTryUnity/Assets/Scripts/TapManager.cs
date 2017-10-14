using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapManager : MonoBehaviour
{
    private float _timeBetweenTaps = 0.15f;
    private int _tapCount = 0;
    public bool _doubleTap;
    public bool _singleTap;
    private float _holdTime = .75f; //or whatever
    private float _acumTime = 0;
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
                //gets set to true, so dash can be started in fixedupdate()
                _doubleTap = true;
                //double tap
                Debug.Log("Double Activated");
                _tapCount = 0;
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

            _acumTime += 1 * Time.deltaTime;
            //Debug.Log("Holding");

            if (_acumTime >= _holdTime && !_holdActivated)
            {
                _holdActivated = true;
                Debug.Log("Held Activated");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _acumTime = 0;
            _isHolding = false;
            _holdActivated = false;
            //Debug.Log("Not Holding");
        }

        if (_timeBetweenTaps > 0)
            _timeBetweenTaps -= 1 * Time.deltaTime;
        //single touch, should not fire until time between first tap and second tap is longer than .25 seconds (_timebetweentaps is less than 0)
        else if (_timeBetweenTaps < 0 && _tapCount > 0 && !_isHolding)
        {
            _singleTap = true;
            _tapCount = 0;
            _timeBetweenTaps = 0.25f;
            Debug.Log("Single Activated");
        }
        else
            _tapCount = 0;
    }
}
