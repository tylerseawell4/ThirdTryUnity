using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BounceHeightLimitIndicator : MonoBehaviour
{
    public Text _heightCounterText;
    private VelocityBounce2 _velBounce;
    public GameObject _heightIndicator;
    private float _ogPos;
    public GameObject _instantiatedIndicator;
    // Use this for initialization
    void Start()
    {
        _velBounce = FindObjectOfType<VelocityBounce2>();
        _ogPos = _velBounce._player.transform.position.y;
        _heightCounterText.text = "To Go: " + CalcDistanceToHeightGoingUp();
        _instantiatedIndicator = Instantiate(_heightIndicator, new Vector3(0, _velBounce._startingHeight, .02f), _heightIndicator.transform.rotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_velBounce._hitHeight)
            _heightCounterText.text = "To Go: " + CalcDistanceToHeightGoingUp();
        else
            _heightCounterText.text = "To Go: " + CalcDistanceToHeightGoingDown();
    }

    private string CalcDistanceToHeightGoingUp()
    {
        var playerPos = _velBounce._player.transform.position.y;
        var predictedDistance = _velBounce._startingHeight - playerPos;
        if (predictedDistance > 0)
            return Mathf.Round(predictedDistance).ToString();
        else
            return "0";
    }
    private string CalcDistanceToHeightGoingDown()
    {
        var playerPos = _velBounce._playersExactHeight;
        var distanceTraveled = playerPos - Mathf.Abs(_velBounce._player.transform.position.y);

        if (Mathf.Round(_velBounce._player.transform.position.y) > 0)
        {
            var predictedDistance = playerPos - distanceTraveled - _ogPos;
            return Mathf.Round(predictedDistance).ToString();
        }
        else
        {
            var predictedDistance = playerPos - distanceTraveled + _ogPos;
            return Mathf.Abs(Mathf.Round(predictedDistance)).ToString();
        }
    }
}
