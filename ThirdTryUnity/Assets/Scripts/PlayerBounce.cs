using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{

    public Rigidbody2D _thePlayer;
    public Transform _startMarker;
    public Transform _endMarker;
    public float _speed = 1.0f;
    private float _startTime;
    private float _journeyLength;
    private bool _hitHeight;
    public float _increaseGravityScale;
    public float _increaseUpwardPlayerSpeed;
    public float _afterBounceGravityScale;
    public int _numberOfBouncesToIncreaseBy;
    private int _bounceCount = 1;
    public float _increaseHeightOfEndPoint;
    public float _gravityCap;
    public float _forceOffset;
    public float _addForceValue;
    private bool _teleportLeft;
    private bool _teleportRight;
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
                _thePlayer.MovePosition(new Vector3(2.8f, _thePlayer.position.y));
                _teleportRight = false;
            }
            else if (_teleportLeft)
            {
                _thePlayer.MovePosition(new Vector3(-2.8f, _thePlayer.position.y));
                _teleportLeft = false;
            }
            else
                _thePlayer.position = Vector3.Lerp(new Vector3(_startMarker.position.x, _startMarker.position.y, transform.position.z), new Vector3(_endMarker.position.x, _endMarker.position.y, transform.position.z), fracJourney);
        }
        else
        {
            if (_teleportRight)
            {
                _thePlayer.MovePosition(new Vector3(2.8f, _thePlayer.position.y));
                _teleportRight = false;
            }
            else if (_teleportLeft)
            {
                _thePlayer.MovePosition(new Vector3(-2.8f, _thePlayer.position.y));
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
            if (_bounceCount == 1)
            {
                _thePlayer.gravityScale = _afterBounceGravityScale;
                _thePlayer.AddForce(transform.up * (_addForceValue + _forceOffset++), ForceMode2D.Impulse);
            }
            else if (_bounceCount < _numberOfBouncesToIncreaseBy)
                _thePlayer.AddForce(transform.up * (_addForceValue + (_increaseUpwardPlayerSpeed + _forceOffset++)), ForceMode2D.Impulse);
            else
                _thePlayer.AddForce(transform.up * (_addForceValue + (_increaseUpwardPlayerSpeed + _forceOffset)), ForceMode2D.Impulse);


            _hitHeight = false;
            _startTime = Time.time;
            _journeyLength = Vector3.Distance(_startMarker.position, _endMarker.position);


            if (_bounceCount <= _numberOfBouncesToIncreaseBy)
                _speed += _increaseUpwardPlayerSpeed;

            _bounceCount++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EndPoint")
        {
            _hitHeight = true;
            if (_bounceCount <= _numberOfBouncesToIncreaseBy)
                _endMarker.position = new Vector3(_endMarker.position.x, _endMarker.position.y + _increaseHeightOfEndPoint);
        }
        if (collision.gameObject.tag == "LeftCamera")
        {
            _teleportRight = true;
            _teleportLeft = false;
        }
        if (collision.gameObject.tag == "RightCamera")
        {
            _teleportLeft= true;
            _teleportRight = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EndPoint")
            if (_bounceCount <= _numberOfBouncesToIncreaseBy)
                if (_thePlayer.gravityScale < _gravityCap)
                    _thePlayer.gravityScale += _increaseGravityScale;
    }
}
