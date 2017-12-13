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
    public bool _hitBottom;
    public bool _decrementGravity;
    public bool _moveCharacterDown;
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
    private int _runCount;
    private PickupSpawner _pickupSpawner;
    private Score _score;
    private float _velocityDecreaseAmt;
    private float _offset;
    private Color _originalColor;
    private EnemySpawner _enemySpawner;
    private float _playersOGPos;
    private float _bugBugHeight;
    public bool _playerCanMove;
    public GameObject _web;
    // Use this for initialization
    void Start()
    {
        _playerCanMove = true;
        _playersOGPos = gameObject.transform.position.y;
        _bounceCount = 0;
        _player.gravityScale = 0f;
        _vMultiplier = 7.5f;
        _originalVMultiplier = _vMultiplier;
        _maxHeightValue = _startingHeight;
        _camera = FindObjectOfType<CameraOption3>();
        _playerControl = FindObjectOfType<PlayerControl>();
        _pickupSpawner = FindObjectOfType<PickupSpawner>();
        _enemySpawner = FindObjectOfType<EnemySpawner>();
        _score = FindObjectOfType<Score>();
        _heightOffset = 5f;
        _runCount = 1;
        _velocityDecreaseAmt = .025f;
        _offset = 0;
        //_originalColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (!_playerCanMove)
        {
            _player.velocity = new Vector3(0, 0, 0f);
            return;
        }

        ///if (_player.transform.position.y >= (_maxHeightValue - 25f) && !_hitrequestedHeight)
            //GetComponent<SpriteRenderer>().color = Color.red;
            //add code to start making the player air streams or whatever UI to start fading to indicate slowing down


            //Debug.Log(_player.velocity.y);
            if (_player.transform.position.y >= (_maxHeightValue - 15f) && !_hitrequestedHeight)
            {
                // _score.ChangeCalculatingPoint(transform.position.y);
                _decrementGravity = true;
                _hitrequestedHeight = true;
                // Debug.Log(_player.transform.position.y);

            }

        //triggers falling code
        if (_decrementGravity)
        {
            //Debug.Log(_player.velocity);
            _vMultiplier -= _velocityDecreaseAmt;
            _player.velocity = new Vector3(_player.velocity.x, _vMultiplier, 0f);

            if (_vMultiplier <= 3)
            {
                _player.velocity = new Vector3(_player.velocity.x, 0, 0f);
                _hitHeight = true;
                _playerControl._forwardDashActivated = false;

                _playerControl._time = 0f;
                _playerControl._topPlayerPoint.localPosition = new Vector3(_playerControl.transform.position.x, 8.5f, _playerControl.transform.position.z);
                _playerControl._startingPlayerTopPtDiff = _playerControl._topPlayerPoint.position.y - transform.position.y;
                _playerControl._startingPlayerTopPtDiff2 = _playerControl._topPlayerPoint.position.y - transform.position.y;

                _startingHeight -= _bugBugHeight;

                _vMultiplier = 3;
                _moveCharacterDown = true;
                _decrementGravity = false;
                _hitBottom = false;
                _playersExactHeight = _player.transform.position.y;

                _playerControl._bottomPlayerPoint.localPosition = new Vector3(transform.position.x, -8.5f, transform.position.z);
                _camera._cameraMovingDownTarget.localPosition = new Vector3(transform.position.x, -8.5f, transform.position.z);
                _playerControl._startingPlayerBottomPtDiff = _playerControl._bottomPlayerPoint.position.y - transform.position.y;
                _playerControl._startingPlayerBottomPtDiff2 = _playerControl._bottomPlayerPoint.position.y - transform.position.y;

                // GetComponent<SpriteRenderer>().color = _originalColor;
                //  Debug.Log(_playersExactHeight);
            }
        }
        if (_playerCanMove)
        {
            if (_moveCharacterDown)
            {
                if (_player.velocity.y > 0)
                    Debug.Log("down" + _player.velocity.y.ToString());
                // Debug.Log(_player.velocity.magnitude);
                if (_vMultiplier < _originalVMultiplier)
                {
                    Debug.Log(_vMultiplier);
                    _vMultiplier += .1f;
                }
                else
                {
                    //Debug.Log(_vMultiplier);
                    _vMultiplier = _originalVMultiplier;
                }

                if (_player.velocity.y != -_vMultiplier)
                    _player.velocity = new Vector3(_player.velocity.x, -_vMultiplier, 0f);
            }


            if (!_hitHeight)
            {
                if (_player.velocity.y < 0)
                    Debug.Log("up" + _player.velocity.y.ToString());
                //  Debug.Log(_player.velocity.magnitude);

                if (_player.velocity.y != _vMultiplier)
                    _player.velocity = new Vector3(_player.velocity.x, _vMultiplier, 0f);
            }
        }
    }


    public int GetRunCount()
    {
        return _runCount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag.Equals("Ground") || collision.gameObject.tag.Equals("BounceBack"))
        {
            if (collision.gameObject.tag.Equals("BounceBack"))
            {
                _camera._hitBounceBug = true;
                _bugBugHeight = collision.transform.position.y - _playersOGPos;
            }
            else
                _bugBugHeight = 0f;

            _playerControl._forwardDashActivated = false;
            _playerControl._time = 0f;

            _playerControl._topPlayerPoint.localPosition = new Vector3(_playerControl.transform.position.x, 8.5f, _playerControl.transform.position.z);
            _camera._cameraMovingUpTarget.localPosition = new Vector3(transform.position.x, 8.5f, transform.position.z);

            _playerControl._score.BounceCount(); // Calling this every time the player collides with the ground to increase bounce count score by 1
                                                 // _score.ChangeCalculatingPoint(transform.position.y);
            _runCount++;
            _playerControl._forwardDashActivated = false;
            _playerControl._time = 0f;
            _hitBottom = true;
            _bounceCount++;
            _hitHeight = false;
            _hitrequestedHeight = false;
            _hitHeight = false;
            _playersExactHeight = 0;
            _originalVMultiplier = _vMultiplier;
            _player.velocity = Vector3.up * _vMultiplier;
            _moveCharacterDown = false;
            _startingHeight += _increaseHeightBy + _bugBugHeight;
            _maxHeightValue = _startingHeight;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SpiderWebSticky")
        {
            var webChildren = _web.GetComponentsInChildren<AreaEffector2D>();

            foreach (var child in webChildren)
            {
                child.forceMagnitude = 0f;
            }

            _playerCanMove = false;
            _player.velocity = new Vector3(0, 0, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "SpiderWebSticky")
        {
            AfterWebExit();
        }
    }

    public void AfterWebExit()
    {
        _decrementGravity = true;
        _vMultiplier = 3f;
    }
}
