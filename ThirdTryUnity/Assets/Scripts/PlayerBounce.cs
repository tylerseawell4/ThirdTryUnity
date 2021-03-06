﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{

    public Rigidbody2D _thePlayer;
    public Transform _startMarker;
    public Transform _endMarker;
    public float _currentEndPointPosition;
    public float _nextEndPointPosition;
    public float _speed = 1.0f;
    private float _startTime;
    private float _journeyLength;
    private bool _hitHeight;
    public float _increaseGravityScale;
    public float _increaseUpwardPlayerSpeed;
    public float _afterBounceGravityScale;
    public int _maxBounceCount;
    public int _bounceCount = 0;
    public float _increaseHeightOfEndPoint;
    public float _gravityCap;
    public float _forceOffset;
    public float _addForceValue;
    private bool _teleportLeft;
    private bool _teleportRight;
    public Transform _leftCameraTransform;
    public Transform _rightCameraTransform;
    // Use this for initialization
    void Start()
    {
        if (_gravityCap < _afterBounceGravityScale)
            throw new ArgumentException("Gravity Cap cannot be lower than gravity after bounce!");

        _hitHeight = true;
        _startTime = Time.time;
        _journeyLength = Vector3.Distance(_startMarker.position, _endMarker.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (!_hitHeight)
        {
            float distCovered = (Time.time - _startTime) * _speed;
            float fracJourney = distCovered / _journeyLength;
            if (_teleportRight)
            {
                _thePlayer.MovePosition(new Vector3(_rightCameraTransform.position.x - .9f, _thePlayer.position.y));
                _teleportRight = false;
            }
            else if (_teleportLeft)
            {
                _thePlayer.MovePosition(new Vector3(_leftCameraTransform.position.x + .9f, _thePlayer.position.y));
                _teleportLeft = false;
            }
            else
                _thePlayer.position = Vector3.Lerp(new Vector3(_startMarker.position.x, _startMarker.position.y, transform.position.z), new Vector3(_endMarker.position.x, _endMarker.position.y, transform.position.z), fracJourney);
        }
        else
        {
            if (_teleportRight)
            {
                _thePlayer.MovePosition(new Vector3(_rightCameraTransform.position.x - .9f, _thePlayer.position.y));
                _teleportRight = false;
            }
            else if (_teleportLeft)
            {
                _thePlayer.MovePosition(new Vector3(_leftCameraTransform.position.x + .9f, _thePlayer.position.y));
                _teleportLeft = false;
            }
        }
    }
    private void Update()
    {
        _endMarker.position = new Vector3(transform.position.x, _endMarker.position.y);
        _startMarker.position = new Vector3(transform.position.x, _startMarker.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MakeEmBounce")
        {
            if (_bounceCount == 0)
            {
                _thePlayer.gravityScale = _afterBounceGravityScale;
                _thePlayer.AddForce(transform.up * (_addForceValue + _forceOffset), ForceMode2D.Impulse);
                _forceOffset += 1.5f;
            }
            else if (_bounceCount < _maxBounceCount)
            {
                _thePlayer.AddForce(transform.up * (_addForceValue + (_increaseUpwardPlayerSpeed + _forceOffset)), ForceMode2D.Impulse);
                _forceOffset += 1.5f;
            }
            else
                _thePlayer.AddForce(transform.up * (_addForceValue + (_increaseUpwardPlayerSpeed + _forceOffset)), ForceMode2D.Impulse);


            _hitHeight = false;
            _startTime = Time.time;
            _journeyLength = Vector3.Distance(_startMarker.position, _endMarker.position);


            if (_bounceCount < _maxBounceCount)
                _speed += _increaseUpwardPlayerSpeed;

            if (_bounceCount != _maxBounceCount)
                _bounceCount++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EndPoint")
        {
            _currentEndPointPosition = _endMarker.position.y;
            _hitHeight = true;
            if (_bounceCount != _maxBounceCount)
            {
                _endMarker.position = new Vector3(_endMarker.position.x, _endMarker.position.y + _increaseHeightOfEndPoint);
                _nextEndPointPosition = _endMarker.position.y;

            }
        }
        if (collision.gameObject.tag == "LeftCamera")
        {
            _teleportRight = true;
            _teleportLeft = false;
        }
        if (collision.gameObject.tag == "RightCamera")
        {
            _teleportLeft = true;
            _teleportRight = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EndPoint")
            if (_bounceCount <= _maxBounceCount)
                if (_thePlayer.gravityScale < _gravityCap)
                    _thePlayer.gravityScale += _increaseGravityScale;
    }
}
