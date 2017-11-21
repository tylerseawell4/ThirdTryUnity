using System.Collections;
using UnityEngine;
public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D _player;
    public float _activeMoveSpeed;
    public Transform _topPlayerPoint;
    public Transform _bottomPlayerPoint;
    public Score _score;
    private bool _facingRight;
    public float _startingPlayerTopPtDiff;
    public float _startingPlayerTopPtDiff2;
    public float _startingPlayerBottomPtDiff;
    public float _startingPlayerBottomPtDiff2;
    private float _currentPlayerPosDiff;
    private bool _shouldSlowCameraWhenGoingUp;
    public bool _forwardDashActivated;
    private TapManager _tapManager;
    private bool _rightDashActivated;
    private bool _leftDashActivated;
    private float posX;
    private bool _addforce;
    public float _time;
    private Vector2 _ogVel;
    private CameraOption3 _camera;
    private VelocityBounce2 _velBounce;
    private DashClickManager _dashManager;
    // Use this for initialization
    void Start()
    {
        _dashManager = FindObjectOfType<DashClickManager>();
        Screen.orientation = ScreenOrientation.Portrait;
        _activeMoveSpeed = 6f;
        _facingRight = true;

        _time = 0f;


        _startingPlayerTopPtDiff = _topPlayerPoint.position.y - transform.position.y;
        _startingPlayerTopPtDiff2 = _topPlayerPoint.position.y - transform.position.y;

        _startingPlayerBottomPtDiff = _bottomPlayerPoint.position.y - transform.position.y;
        _startingPlayerBottomPtDiff2 = _bottomPlayerPoint.position.y - transform.position.y;

        _tapManager = FindObjectOfType<TapManager>();
        //_swipeManager = FindObjectOfType<SwipeManager>();
        _velBounce = FindObjectOfType<VelocityBounce2>();
        _camera = FindObjectOfType<CameraOption3>();
        _score = FindObjectOfType<Score>();
        _addforce = true;
    }

    private IEnumerator LeftDashRoutine()
    {
        _player.transform.position = Vector3.Lerp(_player.position, new Vector3(posX - 5f, _player.transform.position.y, _player.transform.position.z), .125f);
        yield return new WaitForSeconds(2f);

        _leftDashActivated = false;
    }

    private IEnumerator RightDashRoutine()
    {
        _player.transform.position = Vector3.Lerp(_player.position, new Vector3(posX + 5f, _player.transform.position.y, _player.transform.position.z), .125f);
        yield return new WaitForSeconds(2f);

        _rightDashActivated = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A) || _dashManager._isLeftClicked)
        {
            _dashManager._isLeftClicked = false;
            _leftDashActivated = true;
            _rightDashActivated = false;
            posX = _player.transform.position.x;
        }

        if (Input.GetKeyDown(KeyCode.D) || _dashManager._isRightClicked)
        {
            _dashManager._isRightClicked = false;
            _rightDashActivated = true;
            _leftDashActivated = false;
            posX = _player.transform.position.x;
        }

        if (transform.position.x > Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x)
        {
            _rightDashActivated = false;
            _leftDashActivated = false;
            _player.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, _player.position.y, 0f);
        }
        else if (transform.position.x < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x)
        {
            _rightDashActivated = false;
            _leftDashActivated = false;
            _player.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x, _player.position.y, 0f);
        }
        else if (_rightDashActivated)
        {
            _player.transform.position = Vector3.Lerp(_player.position, new Vector3(posX + 5f, _player.transform.position.y, _player.transform.position.z), .125f);
            if (_player.transform.position.x >= posX + 4.5f)
            {
                _rightDashActivated = false;
            }
        }
        else if (_leftDashActivated)
        {
            _rightDashActivated = false;
            _player.transform.position = Vector3.Lerp(_player.position, new Vector3(posX - 5f, _player.transform.position.y, _player.transform.position.z), .125f);
            if (_player.transform.position.x <= posX - 4.5f)
            {
                _leftDashActivated = false;
            }
        }

        if (Input.acceleration.x > .035f && !_leftDashActivated)
            _player.velocity = new Vector3(30f * Input.acceleration.x, _player.velocity.y, 0f);
        else if (Input.acceleration.x < -.035f && !_rightDashActivated)
            _player.velocity = new Vector3(30f * Input.acceleration.x, _player.velocity.y, 0f);
        else
            _player.velocity = new Vector3(0f, _player.velocity.y, 0f);


        if (!_forwardDashActivated)
        {
            if ((_player.velocity.y > 0) && _dashManager._isUpDownClicked || (_player.velocity.y < 0 && _dashManager._isUpDownClicked))
            {
                //flip back
                _dashManager._isUpDownClicked = false;
                //checking if velocity is higher than 0 to see if we are going up (dont need to worry about transition when doing updash), 
                //and checking to see if the player position is less than the exact height of the player when he reaches the stop 
                //minus a value X units down in order to create a deadzone where no dashing can occur so the camera can transition
                if (_player.velocity.y > 0 || _player.position.y <= _velBounce._playersExactHeight - _camera._diffTransStartPosEndPos)
                {
                    _dashManager._isUpDownClicked = false;
                    _forwardDashActivated = true;
                    _shouldSlowCameraWhenGoingUp = true;
                    _currentPlayerPosDiff = 0f;
                    _addforce = true;

                    if (_forwardDashActivated && _player.velocity.y < 0)
                        _shouldSlowCameraWhenGoingUp = false;
                }
                else
                    _dashManager._isUpDownClicked = false;
            }
            else
                _dashManager._isUpDownClicked = false;
        }
        else
            _dashManager._isUpDownClicked = false;

        if (_forwardDashActivated && _player.velocity.y > 0)
        {
            if (_shouldSlowCameraWhenGoingUp)
            {
                if (_addforce)
                {
                    _addforce = false;
                    _ogVel = _player.velocity;
                }
                _player.velocity = new Vector2(_player.velocity.x, _player.velocity.y * 1.4f);

                _time += 1f * Time.deltaTime;

                _startingPlayerTopPtDiff2 -= Time.deltaTime * 8f;
                _topPlayerPoint.position = new Vector3(transform.position.x, transform.position.y + _startingPlayerTopPtDiff2, transform.position.z);
                _currentPlayerPosDiff = _topPlayerPoint.position.y - transform.position.y;

                if (_time > 1f)
                {
                    _player.velocity = new Vector2(_player.velocity.x, _player.velocity.y / 1.25f);
                    _startingPlayerTopPtDiff2 += .275f;
                    _topPlayerPoint.position = new Vector3(transform.position.x, transform.position.y + _startingPlayerTopPtDiff2, transform.position.z);
                    _currentPlayerPosDiff = _topPlayerPoint.position.y - transform.position.y;

                    if (_currentPlayerPosDiff >= _startingPlayerTopPtDiff)
                        _shouldSlowCameraWhenGoingUp = false;
                }
            }
            else
            {
                _time = 0f;
                _forwardDashActivated = false;
                _player.velocity = new Vector2(_player.velocity.x, _ogVel.y);
            }
        }
        else if (_forwardDashActivated && _player.velocity.y <= 0)
        {
            if (!_shouldSlowCameraWhenGoingUp)
            {
                if (_addforce)
                {
                    _addforce = false;
                    _ogVel = _player.velocity;
                }
                _player.velocity = new Vector2(_player.velocity.x, _player.velocity.y * 1.4f);

                _time += 1f * Time.deltaTime;

                _startingPlayerBottomPtDiff2 += Time.deltaTime * 8f;
                _bottomPlayerPoint.position = new Vector3(transform.position.x, transform.position.y + _startingPlayerBottomPtDiff2, transform.position.z);
                _currentPlayerPosDiff = _bottomPlayerPoint.position.y - transform.position.y;

                if (_time > 1f)
                {
                    _player.velocity = new Vector2(_player.velocity.x, _player.velocity.y / 1.25f);
                    _startingPlayerBottomPtDiff2 -= .275f;
                    _bottomPlayerPoint.position = new Vector3(transform.position.x, transform.position.y + _startingPlayerBottomPtDiff2, transform.position.z);
                    _currentPlayerPosDiff = _bottomPlayerPoint.position.y - transform.position.y;

                    if (_currentPlayerPosDiff <= _startingPlayerBottomPtDiff)
                        _shouldSlowCameraWhenGoingUp = true;
                }
            }
            else
            {
                _time = 0f;
                _forwardDashActivated = false;
                _player.velocity = new Vector2(_player.velocity.x, _ogVel.y);
            }
        }
        else
            _tapManager._doubleTap = false;

#if UNITY_ANDROID
        //creating neutral zone for character movements

#endif

        //transform.position = Vector3.Lerp(transform.position, new Vector3(0f, 10f, 0f), Time.deltaTime * _activeMoveSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        //#if UNITY_EDITOR
        //        //MoveLeftRight();
        //#endif

        //#if UNITY_ANDROID
        //        //creating neutral zone for flip
        //        if (_player.velocity.x < -.025f && _facingRight)
        //        {
        //            _facingRight = false;
        //            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //        }
        //        else if (_player.velocity.x > .025f && !_facingRight)
        //        {
        //            _facingRight = true;
        //            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //        }
        //#endif
    }
    private void MoveLeftRight()
    {

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
        if (_tapManager._holdActivated)
            if (collision.gameObject.tag.Equals("Ground"))
            {

            }
    }
}
