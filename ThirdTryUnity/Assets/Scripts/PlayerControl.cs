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
    public float _time;
    private Vector2 _ogVel;
    private CameraOption3 _camera;
    private VelocityBounce2 _velBounce;
    public GameObject _superPrefab;
    public bool _superActivated;
    private float _superAcumTime;
    public Animator _superAnim;
    private bool _superInstantiated;
    public GameObject _super;

    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        _activeMoveSpeed = 6f;
        _facingRight = true;

        _time = 0f;
        _superAcumTime = 0f;

        _startingPlayerTopPtDiff = _topPlayerPoint.position.y - transform.position.y;
        _startingPlayerTopPtDiff2 = _topPlayerPoint.position.y - transform.position.y;

        _startingPlayerBottomPtDiff = _bottomPlayerPoint.position.y - transform.position.y;
        _startingPlayerBottomPtDiff2 = _bottomPlayerPoint.position.y - transform.position.y;

        _tapManager = FindObjectOfType<TapManager>();
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

    IEnumerator SuperFadeOut()
    {
        _superAnim.SetInteger("State", 1);
        _superInstantiated = false;
        _superActivated = false;
        yield return new WaitForSeconds(.5f);
        Destroy(_super);
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
        else if (Input.acceleration.x > .035f)
            _player.velocity = new Vector3(30f * Input.acceleration.x, _player.velocity.y, 0f);
        else if (Input.acceleration.x < -.035f)
            _player.velocity = new Vector3(30f * Input.acceleration.x, _player.velocity.y, 0f);
        else
        {
            _player.velocity = new Vector3(0f, _player.velocity.y, 0f);
        }

        if (_tapManager._holdActivated)
        {
            _superActivated = true;
        }

        if (_superActivated)
        {
            if (!_superInstantiated)
            {
                _superInstantiated = true;
                if (_super == null)
                    _super = new GameObject();
               _super= Instantiate(_superPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _super.transform.position.z), Quaternion.identity);
                _super.transform.parent = transform;
            }

            _superAcumTime += 1 * Time.deltaTime;
            if (_superAcumTime >= 5)
            {
                Color color1 = _super.GetComponent<SpriteRenderer>().material.color;
                Color color2 = _super.transform.GetChild(0).GetComponent<SpriteRenderer>().material.color;
                color1.a -= .025f;
                color2.a -= .025f;

                _super.GetComponent<SpriteRenderer>().material.color = color1;
                _super.transform.GetChild(0).GetComponent<SpriteRenderer>().material.color = color2;
                if(color1.a <= 0f)
                {
                    _tapManager._holdActivated = false;
                    _superInstantiated = false;
                    _superActivated = false;
                    _superAcumTime = 0f;
                    Destroy(_super);
                }
            }

        }

        if (!_forwardDashActivated)
        {
            if (_tapManager._doubleTap)
            {
                //checking if velocity is higher than 0 to see if we are going up (dont need to worry about transition when doing updash), 
                //and checking to see if the player position is less than the exact height of the player when he reaches the stop 
                //minus a value X units down in order to create a deadzone where no dashing can occur so the camera can transition
                if (_player.velocity.y > 0 || _player.position.y <= _velBounce._playersExactHeight - _camera._diffTransStartPosEndPos)
                {
                    //flip back
                    _tapManager._doubleTap = false;
                    _forwardDashActivated = true;
                    _shouldSlowCameraWhenGoingUp = true;
                    _currentPlayerPosDiff = 0f;
                    _addforce = true;

                    if (_forwardDashActivated && _player.velocity.y < 0)
                        _shouldSlowCameraWhenGoingUp = false;
                }
                else
                    _tapManager._doubleTap = false;
            }
            else
                _tapManager._doubleTap = false;
        }
        else
            _tapManager._doubleTap = false;

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
        if (collision.gameObject.tag.Equals("Ground"))
        {
            _forwardDashActivated = false;
            _time = 0f;

            _bottomPlayerPoint.localPosition = new Vector3(transform.position.x, -9.85f, transform.position.z);
            _startingPlayerBottomPtDiff = _bottomPlayerPoint.position.y - transform.position.y;
            _startingPlayerBottomPtDiff2 = _bottomPlayerPoint.position.y - transform.position.y;
            _score.BounceCount(); // Calling this every time the player collides with the ground to increase bounce count score by 1
                                  // _score.ChangeCalculatingPoint(transform.position.y);
        }
    }
}
