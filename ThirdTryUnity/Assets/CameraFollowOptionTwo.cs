using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollowOptionTwo : MonoBehaviour
{
    public Rigidbody2D _player;
    private Transform _camerasFocusPoint;
    public Transform _cameraMovingUpTarget;
    public Transform _cameraMovingDownTarget;
    public Transform _endPoint;
    public float _speed = 1.0f;
    public float _yMin;
    public float _yMax;
    private Vector3 _v3;
    private PlayerBounce _playerBounce;
    public float _distanceCameraStopsFromTop;
    private float _differenceBetweenPoints;
    private float _startingEndPointYValue;
    private float _nextEndPosition;
    private float _currentEndPointPosition;
    public float[] _endPointYs;

    private void Start()
    {
        _camerasFocusPoint = _cameraMovingUpTarget;
        _playerBounce = FindObjectOfType<PlayerBounce>();
        _v3 = transform.position;
        _startingEndPointYValue = Math.Abs(_endPoint.position.y);
        _differenceBetweenPoints = (Math.Abs(_endPoint.position.y) - Math.Abs(_camerasFocusPoint.position.y));
        _currentEndPointPosition = _endPoint.position.y;
    }

    private void FixedUpdate()
    {

        //Determining if the players rigidbody is moving up or down. A negative Y velocity means moving down, positive Y velocity means moving up.
        if (_player.velocity.y > 0)
        {
            _camerasFocusPoint = _cameraMovingUpTarget;
        }
        else
        {
            _camerasFocusPoint = _cameraMovingDownTarget;
        }

        //Calculating the difference between the player and the end point based on the bounce count and the endPoints Y value.
        if (_playerBounce._bounceCount <= 1)
        {
            _differenceBetweenPoints = (_startingEndPointYValue - Math.Abs(_camerasFocusPoint.position.y));
            _currentEndPointPosition = _playerBounce._nextEndPointPosition;
        }
        else
        {
            _differenceBetweenPoints = (_endPointYs[_playerBounce._bounceCount - 2] - Math.Abs(_camerasFocusPoint.position.y));
            _currentEndPointPosition = _playerBounce._nextEndPointPosition;
        }
        
     
        //Determining the difference between the player and the end point to stop or start the camera
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
        v3.y = Mathf.Lerp(v3.y, _camerasFocusPoint.position.y, _speed);
        transform.position = new Vector3(v3.x, Mathf.Clamp(v3.y, _yMin, _yMax), v3.z);
    }

}
