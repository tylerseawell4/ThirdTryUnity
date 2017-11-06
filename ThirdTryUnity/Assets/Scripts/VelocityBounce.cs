using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityBounce : MonoBehaviour
{

    public Rigidbody2D _player;
    public float _startingHeight;
    private float _maxSpeed;
    private bool _shouldIncreaseGravity;
    public float _startingGravity;
    public float _increaseHeightBy;
    private float _a;
    public float _bounceCount;
    private bool _shouldLog;
    public float _maxGravityValue;
    public bool _hitHeight;
    private float _highestY;
    public float _playerSpeed;
    private float _vSpeed;
    public float _gravityGoingUp;
    public float _gravityGoingDown;
    private bool _setGravityGoingDown;
    // Use this for initialization
    void Start()
    {
        //_player.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _maxSpeed = _startingHeight / 4;
        _a = (_startingHeight / 10f) * .001f;
        _shouldLog = true;
        _bounceCount = 0;
        _maxGravityValue = 1 / 10;
        _highestY = _player.transform.position.y;
        _playerSpeed = _player.velocity.magnitude;
        _vSpeed = 15f;
        _gravityGoingDown = 1f;
        _gravityGoingUp = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        _playerSpeed = _player.velocity.magnitude;

        var tempY = _player.transform.position.y;
        if (tempY < _highestY)
        {
            _hitHeight = true;

            if (_setGravityGoingDown)
            {
                _player.gravityScale = _gravityGoingDown;
                _gravityGoingDown -= .025f;
                _setGravityGoingDown = false;
            }
        }
        else
            _highestY = tempY;

        if (_player.velocity.magnitude >= _maxSpeed)
        {
            _shouldIncreaseGravity = true;
        }
        else
        {
            _shouldIncreaseGravity = false;
        }

        //if (_shouldIncreaseGravity && _player.gravityScale <= _maxGravityValue)
        //{
        //    _player.gravityScale += (.01f);
        //     //Debug.Log(_player.gravityScale);
        // }
        //else if (_player.gravityScale >= _maxGravityValue)
        //{

        //    if (_shouldLog)
        //    {
        //        //Debug.Log(_player.transform.position.y);
        //        _shouldLog = false;
        //    }

        //}
        ////Debug.Log(_player.velocity.magnitude);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _bounceCount++;
        // _player.velocity = Vector2.zero;
        //   _player.gravityScale = -10f;
        _vSpeed += 5f;
        _player.velocity = Vector3.up * _vSpeed;
        _player.gravityScale = _gravityGoingUp;
        _gravityGoingUp += .15f;
        //_player.drag += .025f;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _startingHeight += _increaseHeightBy;
        _maxSpeed = _startingHeight / 5;
        // _player.gravityScale = _startingGravity;
        _shouldLog = true;
        _maxGravityValue = _bounceCount / 10;
        _hitHeight = false;
        _highestY = _player.transform.position.y;
        _shouldIncreaseGravity = true;
        _setGravityGoingDown = true;
        //_bounceMat.bounciness = 0f;
    }
}
