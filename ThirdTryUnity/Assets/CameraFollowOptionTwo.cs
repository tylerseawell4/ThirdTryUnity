using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollowOptionTwo : MonoBehaviour
{

    public Transform _target;
    public float _speed = 1.0f;
    public Transform _endPoint;
    public float _yMin;
    public float _yMax;
    private Vector3 _v3;
    private PlayerBounce _playerBounce;
    public float _distanceCameraStopsFromTop;
    private float _differenceBetweenPoints;
    private float _startingEndPointYValue;
    private float _nextEndPosition;
    private float _currentOne;
    private float _nextOne;
    public float[] _endPointYs;
    private void Start()
    {
        _playerBounce = FindObjectOfType<PlayerBounce>();
        _v3 = transform.position;
        _startingEndPointYValue = Math.Abs(_endPoint.position.y);
        _differenceBetweenPoints = (Math.Abs(_endPoint.position.y) - Math.Abs(_target.position.y));
        _currentOne = _endPoint.position.y;
    }

    private void FixedUpdate()
    {
        //if(_playerBounce._bounceCount < 3)
        //{
        //    _differenceBetweenPoints = (_startingEndPointYValue - Math.Abs(_target.position.y));
        //    _currentOne = _playerBounce._nextEndPointPosition;
        //}
        //else
        //{
        //    _differenceBetweenPoints = (_playerBounce._nextEndPointPosition - Math.Abs(_target.position.y));
        //    _currentOne = _playerBounce._nextEndPointPosition;
        //}          
        if (_playerBounce._bounceCount  <= 1)
        {
            _differenceBetweenPoints = (_startingEndPointYValue - Math.Abs(_target.position.y));
            _currentOne = _playerBounce._nextEndPointPosition;
        }
        else
        {
            Debug.Log(_playerBounce._bounceCount);
        Debug.Log(_endPointYs[_playerBounce._bounceCount - 2]);
                _differenceBetweenPoints = (_endPointYs[_playerBounce._bounceCount - 2] - Math.Abs(_target.position.y));
                _currentOne = _playerBounce._nextEndPointPosition;
       }
        //else if (_playerBounce._bounceCount == 3)
        //{
        //    Debug.Log(_playerBounce._currentEndPointPosition);
        //    _differenceBetweenPoints = (13 - Math.Abs(_target.position.y));
        //    _currentOne = _playerBounce._nextEndPointPosition;
        //}
        //else if (_playerBounce._bounceCount == 4)
        //{
        //    _differenceBetweenPoints = (21 - Math.Abs(_target.position.y));
        //    _currentOne = _playerBounce._nextEndPointPosition;
        //}
        //else if (_playerBounce._bounceCount == 5)
        //{
        //    _differenceBetweenPoints = (29 - Math.Abs(_target.position.y));
        //    _currentOne = _playerBounce._nextEndPointPosition;
        //}

    }

    void LateUpdate()
    {
        if (_distanceCameraStopsFromTop >= _differenceBetweenPoints)
        {
            _speed = 0f;
            CameraLerp();
        }
        else
        {
            _speed = 1.0f;
            CameraLerp();
        }

    }
    private void CameraLerp()
    {
        Vector3 v3 = transform.position;
        v3.y = Mathf.Lerp(v3.y, _target.position.y, _speed);
        transform.position = new Vector3(v3.x, Mathf.Clamp(v3.y, _yMin, _yMax), v3.z);
    }

}
