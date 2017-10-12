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
    public float _startingHeightCopy;
    // Use this for initialization
    void Start()
    {
        _bounceCount = 1;
        _player.gravityScale = 0f;
        _vMultiplier = 7.5f;
        _originalVMultiplier = _vMultiplier;
        _startingHeightCopy = _startingHeight;
        _maxHeightValue = _startingHeight;
        _camera = FindObjectOfType<CameraOption3>();
        _heightOffset = 5f;
    }

    // Update is called once per frame
    void Update()
    {
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
                _vMultiplier = _originalVMultiplier;
                _moveCharacterDown = true;
                _decrementGravity = false;
                _hitHeight = true;
                _hitBottom = false;
                _playersExactHeight = _player.transform.position.y;
                Debug.Log(_player.transform.position.y);
            }
        }

        if (_moveCharacterDown)
        {
            _player.velocity = new Vector3(_player.velocity.x, -_vMultiplier, 0f);
        }


        if (!_hitHeight)
        {
            _player.velocity = new Vector3(_player.velocity.x, _vMultiplier, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals("Enemy"))
        {
            _hitBottom = true;
            _bounceCount++;
            _hitHeight = false;
            _hitrequestedHeight = false;
            _hitHeight = false;
            _playersExactHeight = 0;

            if (_maxSpeed >= _vMultiplier && _bounceCount % 2 == 0)
            {
                _vMultiplier += 2f;
                _camera._transitionSpeed += .175f;
                _heightOffset += _bounceCount;
            }

            _originalVMultiplier = _vMultiplier;
            _player.velocity = Vector3.up * _vMultiplier;
            _moveCharacterDown = false;
            _startingHeight += _increaseHeightBy;
            _maxHeightValue = _startingHeight;
        }
    }
}
