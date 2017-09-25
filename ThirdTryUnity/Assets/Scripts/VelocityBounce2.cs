using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityBounce2 : MonoBehaviour
{
    public Rigidbody2D _player;
    public float _startingHeight;
    private float _maxHeightValue;
    private float _vMultiplier;
    public bool _hitHeight;
    private bool _decrementGravity;
    private bool _moveCharacterDown;
    private bool _hitrequestedHeight;
    private float _originalVMultiplier;
    private CameraOption3 _camera;
    private int _bounceCount;
    private float _heightOffset;
    public float _increaseHeightBy;
    // Use this for initialization
    void Start()
    {
        _bounceCount = 1;
        _player.gravityScale = 0f;
        _vMultiplier = 7.5f;
        _originalVMultiplier = _vMultiplier;
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
            Debug.Log(_player.transform.position.y);
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
                Debug.Log(_player.transform.position.y);
            }
        }

        if (_moveCharacterDown)
        {
            _player.velocity = new Vector3(_player.velocity.x, -_vMultiplier, 0f) ;
        }


        if (!_hitHeight)
        {
            _player.velocity = new Vector3(_player.velocity.x, _vMultiplier, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _bounceCount++;
        _hitHeight = false;
        _hitrequestedHeight = false;
        _vMultiplier += 2f;
        _originalVMultiplier = _vMultiplier;
        _player.velocity = Vector3.up * _vMultiplier;
        _moveCharacterDown = false;
        _startingHeight += _increaseHeightBy;
        _heightOffset += _bounceCount;
        _maxHeightValue =  _startingHeight;
        _camera._transitionSpeed += .5f;
    }
}
