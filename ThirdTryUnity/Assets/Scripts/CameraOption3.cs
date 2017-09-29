using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraOption3 : MonoBehaviour
{
    private Transform _camerasFocusPoint;
    public Transform _cameraMovingUpTarget;
    public Transform _cameraMovingDownTarget;
    public float _speed = 1.0f;
    public float _yMin;
    public float _yMax;
    private VelocityBounce2 _playerBounce;
    public float _transitionSpeed;
    private void Start()
    {
        _camerasFocusPoint = _cameraMovingUpTarget;
        _playerBounce = FindObjectOfType<VelocityBounce2>();
        _transitionSpeed = 1.25f;
    }

    private void FixedUpdate()
    {
        if (_playerBounce._hitHeight)
        {
            _camerasFocusPoint = _cameraMovingDownTarget;
            ////Debug.Log(_playerBounce._player.velocity.magnitude);
            _speed = _transitionSpeed;
            CameraLerp();
        }
        else
        {
            _camerasFocusPoint = _cameraMovingUpTarget;
            _speed = 1.0f;
            CameraLerp();
        }
    }

    private void CameraLerp()
    {
        Vector3 v3 = transform.position;
        if (!_playerBounce._hitHeight)
            v3.y = Mathf.Lerp(v3.y, _camerasFocusPoint.position.y, _speed);
        else
            v3.y = Mathf.Lerp(v3.y, _camerasFocusPoint.position.y, _speed * Time.deltaTime);
        transform.position = new Vector3(v3.x, Mathf.Clamp(v3.y, _yMin, _yMax), v3.z);
    }

}
