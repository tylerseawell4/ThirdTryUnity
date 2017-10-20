﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityBounce2 : MonoBehaviour
{
    public Rigidbody2D _player;
    public float _startingHeight;
    private float _maxHeightValue;
    private float _vMultiplier;
    public bool _hitHeight;
    public bool _hitBottom;
    private bool _decrementGravity;
    private bool _moveCharacterDown;
    private bool _hitrequestedHeight;
    private float _originalVMultiplier;
    private CameraOption3 _camera;
    private int _bounceCount;
    private float _heightOffset;
    public float _increaseHeightBy;
    public float _maxSpeed;
    public float _playersExactHeight;
    private PlayerControl _playerControl;
    public bool _stopTrans;

    // Use this for initialization
    void Start()
    {
        _bounceCount = 1;
        _player.gravityScale = 0f;
        _vMultiplier = 7.5f;
        _originalVMultiplier = _vMultiplier;
        _maxHeightValue = _startingHeight;
        _camera = FindObjectOfType<CameraOption3>();
        _playerControl = FindObjectOfType<PlayerControl>();
        _heightOffset = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_player.velocity.y);
        if (_player.transform.position.y >= (_maxHeightValue - _heightOffset) && !_hitrequestedHeight)
        {
            _decrementGravity = true;
            _hitrequestedHeight = true;
            //Debug.Log(_player.transform.position.y);
        }

        if (_decrementGravity)
        {
            _vMultiplier -= .1f;
            _player.velocity = new Vector3(_player.velocity.x, _vMultiplier, 0f);

            if (_vMultiplier <= 0)
            {
                _hitHeight = true;
                _playerControl._forwardDashActivated = false;

                _playerControl._time = 0f;
                _playerControl._topPlayerPoint.localPosition = new Vector3(_playerControl.transform.position.x, 9.85f, _playerControl.transform.position.z);
                _playerControl._startingPlayerTopPtDiff = _playerControl._topPlayerPoint.position.y - transform.position.y;
                _playerControl._startingPlayerTopPtDiff2 = _playerControl._topPlayerPoint.position.y - transform.position.y;


                _vMultiplier = _originalVMultiplier;
                _moveCharacterDown = true;
                _decrementGravity = false;
                _hitBottom = false;
                _playersExactHeight = _player.transform.position.y;
                Debug.Log(_playersExactHeight);
            }
        }

        if (_moveCharacterDown)
        {
            if (_player.velocity.y > 0)
                Debug.Log("down" + _player.velocity.y.ToString());
            // Debug.Log(_player.velocity.magnitude);
            ;
            _player.velocity = new Vector3(_player.velocity.x, -_vMultiplier, 0f);
        }


        if (!_hitHeight)
        {
            if (_player.velocity.y < 0)
                Debug.Log("up" + _player.velocity.y.ToString());
            //  Debug.Log(_player.velocity.magnitude);

            _player.velocity = new Vector3(_player.velocity.x, _vMultiplier, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            _playerControl._forwardDashActivated = false;
            _playerControl._time = 0f;

            _hitBottom = true;
            _bounceCount++;
            _hitHeight = false;
            _hitrequestedHeight = false;
            _hitHeight = false;
            _playersExactHeight = 0;

            if (_maxSpeed >= _vMultiplier && _bounceCount % 3 == 0)
            {
                _vMultiplier += 2f;
                _heightOffset += _bounceCount;

                _camera._camerLerpSpeed += .225f;
                _camera._transitionSpeed += .05f;
                _camera._diffTransStartPosEndPos += 5;
            }

            _originalVMultiplier = _vMultiplier;
            _player.velocity = Vector3.up * _vMultiplier;
            _moveCharacterDown = false;
            _startingHeight += _increaseHeightBy;
            _maxHeightValue = _startingHeight;
        }
    }
}
