using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D _player;
    public float _activeMoveSpeed;
    public Transform _topPlayerPoint;
    public Transform _bottomPlayerPoint;
    private bool _facingRight;
    private bool _hasW;
    private float _startingPlayerTopPtDiff;
    private float _startingPlayerTopPtDiff2;
    private float _currentPlayerTopPtDiff;
    private bool _shouldSlowCameraWhenGoingUp;
    private bool _dashActivated;
    public Transform _leftCameraTransform;
    public Transform _rightCameraTransform;
    private DragEnum _dragDirection;
    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        _activeMoveSpeed = 8f;
        _facingRight = true;
        _startingPlayerTopPtDiff = _topPlayerPoint.position.y - transform.position.y;
        _startingPlayerTopPtDiff2 = _topPlayerPoint.position.y - transform.position.y;
        _dragDirection = DragEnum.None;
    }


private void FixedUpdate()
    {
        if (!_dashActivated)
        {
            if (_dragDirection == DragEnum.Up)
            {
                _dashActivated = true;
                _shouldSlowCameraWhenGoingUp = true;
                _hasW = true;
                _currentPlayerTopPtDiff = 0f;
            }
        }

        if (_hasW)
        {
            if (_shouldSlowCameraWhenGoingUp)
            {
                if (_topPlayerPoint.position.y >= _startingPlayerTopPtDiff2)
                {
                    _startingPlayerTopPtDiff2 -= Time.deltaTime * 7f;
                    _topPlayerPoint.position = new Vector3(transform.position.x, transform.position.y + _startingPlayerTopPtDiff2, transform.position.z);
                }
                if (_topPlayerPoint.position.y <= transform.position.y)
                {
                    _shouldSlowCameraWhenGoingUp = false;
                }
            }
            else
            {
                if (_currentPlayerTopPtDiff <= _startingPlayerTopPtDiff)
                {
                    _currentPlayerTopPtDiff += Time.deltaTime * 6f;
                    _topPlayerPoint.position = new Vector3(transform.position.x, transform.position.y + _currentPlayerTopPtDiff, transform.position.z);
                }
                if (_currentPlayerTopPtDiff >= _startingPlayerTopPtDiff)
                {
                    _hasW = false;
                    _dashActivated = false;
                    _dragDirection = DragEnum.None;
                    _startingPlayerTopPtDiff2 = _topPlayerPoint.position.y - transform.position.y;
                }
            }
        }

#if UNITY_ANDROID
        //creating neutral zone for character movements
        if (Input.acceleration.x > .025f)
            _player.velocity = new Vector3(30f * Input.acceleration.x, _player.velocity.y, 0f);
        else if (Input.acceleration.x < -.025f)
            _player.velocity = new Vector3(30f * Input.acceleration.x, _player.velocity.y, 0f);
#endif

        //transform.position = Vector3.Lerp(transform.position, new Vector3(0f, 10f, 0f), Time.deltaTime * _activeMoveSpeed);
    }
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        MoveLeftRight();
#endif

#if UNITY_ANDROID
        //creating neutral zone for flip
        if (_player.velocity.x < -.025f && _facingRight)
        {
            _facingRight = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (_player.velocity.x > .025f && !_facingRight)
        {
            _facingRight = true;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
#endif
    }
    private void MoveLeftRight()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.acceleration.x > .01f)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _player.velocity = new Vector3(_activeMoveSpeed, _player.velocity.y, 0f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.acceleration.x < -.01f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _player.velocity = new Vector3(-_activeMoveSpeed, _player.velocity.y, 0f);
        }
        else
            _player.velocity = new Vector3(0f, _player.velocity.y, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        // Draws a line from the player transform to the targets position in the scene view only
        if (_topPlayerPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _topPlayerPoint.position);
        }
        if (_bottomPlayerPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _bottomPlayerPoint.position);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("RightCamera"))
        {
            _player.position = new Vector3(_leftCameraTransform.position.x + 1f, _player.position.y, 0f);
        }
        else if (collision.gameObject.tag.Equals("LeftCamera"))
        {
            _player.position = new Vector3(_rightCameraTransform.position.x -1f, _player.position.y, 0f);
        }
    }
}
