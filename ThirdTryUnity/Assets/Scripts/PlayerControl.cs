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
    public GameObject _topEndPoint;
    private float _timeElapsed = 0f;
    public float _waitTime;
    private bool _hasW;
    private float _startingPlayerTopPtDiff;
    private float _startingPlayerTopPtDiff2;
    private float _currentPlayerTopPtDiff;
    private bool _hasBeenReassigned;
    private bool _shouldSlowCameraWhenGoingUp;
    private GameObject _dashPt;
    private bool _dashActivated;

    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        _activeMoveSpeed = 5f;
        _facingRight = true;
        _waitTime = .5f;
        _startingPlayerTopPtDiff = _topEndPoint.transform.position.y - transform.position.y;
        _startingPlayerTopPtDiff2= _topEndPoint.transform.position.y - transform.position.y;
    }
    private void FixedUpdate()
    {
        if (!_dashActivated)
        {
            if (Input.GetKeyDown(KeyCode.W))
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
                if (_topEndPoint.transform.position.y >= _startingPlayerTopPtDiff2)
                {
                    _startingPlayerTopPtDiff2 -= Time.deltaTime * 7f;
                    _topEndPoint.transform.position = new Vector3(transform.position.x, transform.position.y + _startingPlayerTopPtDiff2, transform.position.z);
                }
                if (_topEndPoint.transform.position.y <= transform.position.y)
                {
                    _shouldSlowCameraWhenGoingUp = false;
                }
            }
            else
            {
                if (_currentPlayerTopPtDiff <= _startingPlayerTopPtDiff)
                {
                    _currentPlayerTopPtDiff += Time.deltaTime * 6f;
                    _topEndPoint.transform.position = new Vector3(transform.position.x, transform.position.y + _currentPlayerTopPtDiff, transform.position.z);
                }
                if (_currentPlayerTopPtDiff >= _startingPlayerTopPtDiff)
                {
                    _topEndPoint.transform.parent = transform;
                    _hasW = false;
                    _dashActivated = false;
                    _startingPlayerTopPtDiff2 = _topEndPoint.transform.position.y - transform.position.y;
                    _dashPt = null;
                }
            }
        }





#if UNITY_ANDROID
        //creating neutral zone for character movements
        if (Input.acceleration.x > .025f)
            _player.velocity = new Vector3(10f * Input.acceleration.x, _player.velocity.y, 0f);
        else if (Input.acceleration.x < -.025f)
            _player.velocity = new Vector3(10f * Input.acceleration.x, _player.velocity.y, 0f);
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
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.acceleration.x > .01f)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _player.velocity = new Vector3(_activeMoveSpeed, _player.velocity.y, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.acceleration.x < -.01f)
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
}
