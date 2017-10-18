using System.Collections;
using UnityEngine;
public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D _player;
    public float _activeMoveSpeed;
    public Transform _topPlayerPoint;
    public Transform _bottomPlayerPoint;
    private bool _facingRight;
    public float _startingPlayerTopPtDiff;
    public float _startingPlayerTopPtDiff2;
    private float _startingPlayerBottomPtDiff;
    private float _startingPlayerBottomPtDiff2;
    private float _currentPlayerPosDiff;
    private bool _shouldSlowCameraWhenGoingUp;
    public bool _forwardDashActivated;
    private TapManager _tapManager;
    private bool _rightDashActivated;
    private bool _leftDashActivated;
    private float posX;
    private bool _addforce;
    private VelocityBounce2 _playerBounce;
    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        _activeMoveSpeed = 6f;
        _facingRight = true;

        _startingPlayerTopPtDiff = _topPlayerPoint.position.y - transform.position.y;
        _startingPlayerTopPtDiff2 = _topPlayerPoint.position.y - transform.position.y;

        _startingPlayerBottomPtDiff = _bottomPlayerPoint.position.y - transform.position.y;
        _startingPlayerBottomPtDiff2 = _bottomPlayerPoint.position.y - transform.position.y;

        _tapManager = FindObjectOfType<TapManager>();
        _addforce = true;

        _playerBounce = FindObjectOfType<VelocityBounce2>();
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            _leftDashActivated = true;
            _rightDashActivated = false;
            posX = _player.transform.position.x;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
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
        else if (Input.GetKey(KeyCode.RightArrow) || Input.acceleration.x > .025f)
        {
            _leftDashActivated = false;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _player.velocity = new Vector3(_activeMoveSpeed, _player.velocity.y, 0f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.acceleration.x < -.025f)
        {
            _rightDashActivated = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _player.velocity = new Vector3(-_activeMoveSpeed, _player.velocity.y, 0f);
        }
        else
        {
            _player.velocity = new Vector3(0f, _player.velocity.y, 0f);
        }

        if (!_forwardDashActivated)
        {
            // if (_tapManager._doubleTap)
            if (_tapManager._doubleTap)
            {
                //flip back
                _tapManager._doubleTap = false;
                _forwardDashActivated = true;
                _shouldSlowCameraWhenGoingUp = true;
                _currentPlayerPosDiff = 0f;
                _addforce = true;

                if (_forwardDashActivated && _playerBounce._hitHeight)
                    _shouldSlowCameraWhenGoingUp = false;
            }
            else
                _tapManager._doubleTap = false;
        }
        else
            _tapManager._doubleTap = false;

        if (_forwardDashActivated && !_playerBounce._hitHeight)
        {
            if (_shouldSlowCameraWhenGoingUp)
            {
                if (_topPlayerPoint.position.y >= _startingPlayerTopPtDiff2)
                {
                    _startingPlayerTopPtDiff2 -= Time.deltaTime * 8f;
                    _topPlayerPoint.position = new Vector3(transform.position.x, transform.position.y + _startingPlayerTopPtDiff2, transform.position.z);
                    if (_addforce)
                    {
                        _player.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
                        _addforce = false;
                    }
                }
                if (_topPlayerPoint.position.y < transform.position.y)
                {
                    _shouldSlowCameraWhenGoingUp = false;
                }
            }
            else
            {
                if (_currentPlayerPosDiff <= _startingPlayerTopPtDiff)
                {
                    _currentPlayerPosDiff += Time.deltaTime * 6f;
                    _topPlayerPoint.position = new Vector3(transform.position.x, transform.position.y + _currentPlayerPosDiff, transform.position.z);
                }
                if (_currentPlayerPosDiff >= _startingPlayerTopPtDiff)
                {
                    _forwardDashActivated = false;

                    _topPlayerPoint.position = new Vector3(transform.position.x, _startingPlayerTopPtDiff + transform.position.y, transform.position.z);
                    _startingPlayerTopPtDiff = _topPlayerPoint.position.y - transform.position.y;
                    _startingPlayerTopPtDiff2 = _topPlayerPoint.position.y - transform.position.y;

                }
            }
        }
        else if (_forwardDashActivated && _playerBounce._hitHeight)
        {
            if (!_shouldSlowCameraWhenGoingUp)
            {
                if (_bottomPlayerPoint.position.y >= _startingPlayerBottomPtDiff2)
                {
                    _startingPlayerBottomPtDiff2 += Time.deltaTime * 14f;
                    _bottomPlayerPoint.position = new Vector3(transform.position.x, transform.position.y + _startingPlayerBottomPtDiff2, transform.position.z);
                    if (_addforce)
                    {
                        _player.AddForce(Vector2.down * 4f, ForceMode2D.Impulse);
                        _addforce = false;
                    }
                }
                if (_bottomPlayerPoint.position.y > transform.position.y + 2f)
                {
                    _shouldSlowCameraWhenGoingUp = true;
                }
            }
            else
            {
                if (_currentPlayerPosDiff >= _startingPlayerBottomPtDiff)
                {
                    _currentPlayerPosDiff -= Time.deltaTime * 18f;
                    _bottomPlayerPoint.position = new Vector3(transform.position.x, transform.position.y + _currentPlayerPosDiff, transform.position.z);
                }
                if (_currentPlayerPosDiff <= _startingPlayerBottomPtDiff)
                {
                    _forwardDashActivated = false;

                    _bottomPlayerPoint.position = new Vector3(transform.position.x, _startingPlayerBottomPtDiff + transform.position.y, transform.position.z);
                    _startingPlayerBottomPtDiff = _bottomPlayerPoint.position.y - transform.position.y;
                    _startingPlayerBottomPtDiff2 = _bottomPlayerPoint.position.y - transform.position.y;
                }
            }
        }
        else
            _tapManager._doubleTap = false;

#if UNITY_ANDROID
        //creating neutral zone for character movements
        if (Input.acceleration.x > .025f)
            _player.velocity = new Vector3(25f * Input.acceleration.x, _player.velocity.y, 0f);
        else if (Input.acceleration.x < -.025f)
            _player.velocity = new Vector3(25f * Input.acceleration.x, _player.velocity.y, 0f);
#endif

        //transform.position = Vector3.Lerp(transform.position, new Vector3(0f, 10f, 0f), Time.deltaTime * _activeMoveSpeed);
    }
    // Update is called once per frame
    void Update()
    {




#if UNITY_EDITOR
        //MoveLeftRight();
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
        if (collision.gameObject.tag.Equals("Ground"))
        {
            _bottomPlayerPoint.position = new Vector3(transform.position.x, _startingPlayerBottomPtDiff + transform.position.y, transform.position.z);
            _startingPlayerBottomPtDiff = _bottomPlayerPoint.position.y - transform.position.y;
            _startingPlayerBottomPtDiff2 = _bottomPlayerPoint.position.y - transform.position.y;
        }
    }
}
