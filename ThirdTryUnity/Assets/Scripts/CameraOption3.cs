using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraOption3 : MonoBehaviour
{
    private Transform _camerasFocusPoint;
    public Transform _cameraMovingUpTarget;
    public Transform _cameraMovingDownTarget;
    private float _speed;
    public float _yMin;
    public float _yMax;
    public VelocityBounce2 _playerBounce;
    private PlayerControl _playerControl;
    public float _camerLerpSpeed;
    private Vector2 _ogVel;
    public bool _shouldTrans;
    public float _transitionSpeed;
    public float _diffTransStartPosEndPos;
    public bool _hitBounceBug;
    private bool _shouldResetMovingDownTarget;

    private void Start()
    {
        _camerasFocusPoint = _cameraMovingUpTarget;
        _playerBounce = FindObjectOfType<VelocityBounce2>();
        _playerControl = FindObjectOfType<PlayerControl>();
        _camerLerpSpeed = 1.25f;
        _speed = _camerLerpSpeed;
        _transitionSpeed = .275f;


        //high base value, will need to increase if we use a higher starting velocity
        _diffTransStartPosEndPos = 15f;
    }

    private void FixedUpdate()
    {
        _ogVel = _playerBounce._player.velocity;


        if (_playerControl._player.velocity.y < 0)
        {
            if (_playerControl._player.velocity.y > 0)
                Debug.Log("down" + _playerControl._player.velocity.y.ToString());

            _camerasFocusPoint = _cameraMovingDownTarget;
            _speed = _camerLerpSpeed;
            if (transform.position.y != _camerasFocusPoint.position.y && _playerControl._player.position.y >= _playerBounce._playersExactHeight - _diffTransStartPosEndPos)
            {
                _shouldTrans = true;
                _playerBounce.enabled = false;
                _playerBounce._player.velocity = new Vector2(_playerBounce._player.velocity.x, 0f);
            }
            //if (_camerasFocusPoint.localPosition.y - .05f > _cameraMovingDownTarget.localPosition.y)
            //{
            //    _camerasFocusPoint.localPosition = Vector3.Lerp(_cameraMovingUpTarget.localPosition, _cameraMovingDownTarget.localPosition, .025f);
            //}
            //else
            //{
            //    _shouldResetMovingDownTarget = true;
            //    _camerasFocusPoint = _cameraMovingDownTarget;
            //}

            //if (_shouldResetMovingDownTarget)
            //{
            //    _shouldResetMovingDownTarget = false;
            //    _cameraMovingUpTarget.localPosition = new Vector3(_playerControl.transform.position.x, 8.5f, _playerControl.transform.position.z);
            //}

            _speed = _camerLerpSpeed;

            CameraLerp();
        }
        else if (_playerControl._player.velocity.y > 0 && !_hitBounceBug)
        {
            if (_playerControl._player.velocity.y < 0)
                Debug.Log("up" + _playerControl._player.velocity.y.ToString());

            _camerasFocusPoint = _cameraMovingUpTarget;
            _speed = _camerLerpSpeed;

            CameraLerp();
        }
        else if (_playerControl._player.velocity.y >= 0 && _hitBounceBug)
        {
            if (_camerasFocusPoint.localPosition.y + .05f < _cameraMovingUpTarget.localPosition.y)
            {
                _camerasFocusPoint.localPosition = Vector3.Lerp(_cameraMovingDownTarget.localPosition, _cameraMovingUpTarget.localPosition, .03f);

            }
            else
            {
                _hitBounceBug = false;
                _camerasFocusPoint = _cameraMovingUpTarget;
                _cameraMovingDownTarget.localPosition = new Vector3(_playerControl.transform.position.x, -8.5f, _playerControl.transform.position.z);
            }

            _speed = _camerLerpSpeed;

            CameraLerp();
        }
    }

    private void MoveFromTopPtToBttmPt()
    {
        Vector3 v3 = transform.position;

        _camerasFocusPoint = _cameraMovingDownTarget;

        v3.y = Mathf.Lerp(v3.y, _camerasFocusPoint.position.y, _speed);

    }

    private void CameraLerp()
    {
        Vector3 v3 = transform.position;

        if (!_shouldTrans)
        {
            v3.y = Mathf.Lerp(v3.y, _camerasFocusPoint.position.y, _speed);
        }
        else
        {
            v3.y = Mathf.MoveTowards(v3.y, _camerasFocusPoint.position.y, _transitionSpeed);
            if (transform.position.y >= _camerasFocusPoint.position.y)
            {
                _shouldTrans = false;
                _playerBounce.enabled = true;
                _playerBounce._player.velocity = _ogVel;
            }
        }

        transform.position = new Vector3(v3.x, Mathf.Clamp(v3.y, _yMin, _yMax), v3.z);
    }

}
