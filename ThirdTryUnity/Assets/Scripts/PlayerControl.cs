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
    private bool _shouldUnanchorCamera;
    public bool _forwardDashActivated;
    private TapManager _tapManager;
    private bool _rightDashActivated;
    private bool _leftDashActivated;
    private float posX;
    public float _time;
    private Vector2 _ogVel;
    private CameraOption3 _camera;
    private Camera4 _camera4;
    private VelocityBounce2 _velBounce;
    private DashClickManager _dashManager;
    private SpriteRenderer _sprite;
    private Color _originalColor;
    private Color _inWormColor;
    public SuperEnums _superState;
    public GameObject[] _webArrows;
    private int _arrowHitCount;
    public GameObject _web;
    private float _inputFactor;
    private bool _shouldPressButton;
    public GameObject _star;
    private bool _makeSmaller;
    private SuperMoveManager _superMoveManager;
    private float _dashAcumTime;
    private float _dashSpeed;

    // Use this for initialization
    void Start()
    {
        _dashSpeed = 7.5f;
        _superMoveManager = FindObjectOfType<SuperMoveManager>();
        _inputFactor = 1.5f;
        if (GetComponent<SuperKetchup>().enabled)
            _superState = SuperEnums.Ketchup;
        else if (GetComponent<SuperMustard>().enabled)
            _superState = SuperEnums.Mustard;
        else if (GetComponent<SuperIce>().enabled)
            _superState = SuperEnums.Mayo;
        //else if (GetComponent<>().enabled)
        //    _superState = SuperEnums.SnP;


        _inWormColor = new Color(.25f, .25f, .25f, .6f);
        _sprite = GetComponent<SpriteRenderer>();
        _originalColor = _sprite.material.color;
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
        _camera4 = FindObjectOfType<Camera4>();
        _score = FindObjectOfType<Score>();
    }

    //private IEnumerator LeftDashRoutine()
    //{
    //    _player.transform.position = Vector3.Lerp(_player.position, new Vector3(posX - 5f, _player.transform.position.y, _player.transform.position.z), .125f);
    //    yield return new WaitForSeconds(2f);

    //    _leftDashActivated = false;
    //}

    //private IEnumerator RightDashRoutine()
    //{
    //    _player.transform.position = Vector3.Lerp(_player.position, new Vector3(posX + 5f, _player.transform.position.y, _player.transform.position.z), .125f);
    //    yield return new WaitForSeconds(2f);

    //    _rightDashActivated = false;
    //}

    private void Update()
    {
        //all the spider web mini game stuff if you get stuck
        if (_shouldPressButton)
        {
            var scale = _star.transform.localScale;
            if (scale.x < .6 && !_makeSmaller)
            {
                scale = new Vector3(scale.x += .01f, scale.y += .01f);
            }
            else
            {
                _makeSmaller = true;
            }

            if (scale.x > .3f && _makeSmaller)
            {
                scale = new Vector3(scale.x -= .01f, scale.y -= .01f);
            }
            else
            {
                _makeSmaller = false;
            }
            _star.transform.localScale = scale;
            if (_star.GetComponent<DetectedByTouch>()._wasTouched)
            {
                _makeSmaller = false;
                _velBounce._playerCanMove = true;
                _velBounce.AfterWebExit();
                Destroy(_web);
                _shouldPressButton = false;
                var spider = FindObjectOfType<SpiderMovement>();
                if (spider != null)
                    spider._changeMovement = true;
            }
        }

        //checking to see if leftdash should be canceled (player can move only false when stuck in web), if tilted phone far enough to right
        if (Input.acceleration.x > .1f && _velBounce._playerCanMove)
            _leftDashActivated = false;

        //checking to see if right dash should be canceled, if tilted phone far enough to left
        if (Input.acceleration.x < -.1f && _velBounce._playerCanMove)
            _rightDashActivated = false;

        //a check to see if the dash manager has registered a tap on the left dash button, and sets the flags to perform the left dash.
        if (_velBounce._playerCanMove && (Input.GetKeyDown(KeyCode.A) || _dashManager._isLeftClicked))
        {
            _dashAcumTime = 0;
            _dashManager._isLeftClicked = false;
            _leftDashActivated = true;
            _rightDashActivated = false;
            posX = _player.transform.position.x;
        }

        //a check to see if the dash manager has registered a tap on the right dash button, and sets the flags to perform the right dash.
        if (_velBounce._playerCanMove && (Input.GetKeyDown(KeyCode.D) || _dashManager._isRightClicked))
        {
            _dashAcumTime = 0;
            _dashManager._isRightClicked = false;
            _rightDashActivated = true;
            _leftDashActivated = false;
            posX = _player.transform.position.x;
        }

        //checking to see if player is beyond the right edge of the screen, and teleports the player to the left side of the screen
        if (transform.position.x > Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x)
        {
            _player.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, _player.position.y, 0f);
        }
        //checking to see if player is beyond the left edge of the screen, and teleports the player to the right side of the screen
        else if (transform.position.x < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x)
        {
            _player.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x, _player.position.y, 0f);
        }
        //the check and code to perform the right dash on the player
        else if (_rightDashActivated)
        {
            _leftDashActivated = false;
            var right = _player.transform.position.x + .325f;
            _player.transform.position = new Vector3(right, _player.transform.position.y, _player.transform.position.z);

            _dashAcumTime += 1 * Time.deltaTime;
            if (_dashAcumTime > .275f)
                _rightDashActivated = false;
        }
        //the check and code to perform the left dash on the player
        else if (_leftDashActivated)
        {
            _rightDashActivated = false;
            var left = _player.transform.position.x - .325f;
            _player.transform.position = new Vector3(left, _player.transform.position.y, _player.transform.position.z);

            _dashAcumTime += 1 * Time.deltaTime;
            if (_dashAcumTime > .275f)
                _leftDashActivated = false;
        }

        //DEADZONE between .035 and -.035 for player control
        //moves the player right
        if (Input.acceleration.x > .035f && _velBounce._playerCanMove)
            _player.velocity = new Vector3(30f * Input.acceleration.x, _player.velocity.y, 0f);
        //moves the player left
        else if (Input.acceleration.x < -.035f && _velBounce._playerCanMove)
            _player.velocity = new Vector3(30f * Input.acceleration.x, _player.velocity.y, 0f);
        //keeps the horizontal velocity the same if in the deadzone
        else if (_velBounce._playerCanMove)
            _player.velocity = new Vector3(0f, _player.velocity.y, 0f);
        //mini game portion for the spider web
        else if (Input.acceleration.x < 0f && !_velBounce._playerCanMove && !_shouldPressButton || Input.GetKey(KeyCode.LeftArrow))
        {
            if (_webArrows[0].activeInHierarchy)
            {
                var scale = _webArrows[1].transform.localScale;
                var scaleFactor = Mathf.Abs(Input.acceleration.x * _inputFactor);
                scale = new Vector3(scaleFactor, scaleFactor);
                _webArrows[1].transform.localScale = scale;

                if (_webArrows[1].transform.localScale.x >= _webArrows[0].transform.localScale.x || Input.GetKey(KeyCode.LeftArrow))
                {
                    _arrowHitCount++;
                    var color = _web.GetComponent<SpriteRenderer>().color;
                    color.a -= .33f;
                    _web.GetComponent<SpriteRenderer>().color = color;
                    if (_arrowHitCount == 2)
                    {
                        _star.SetActive(true);
                        _webArrows[0].SetActive(false);
                        _webArrows[1].SetActive(false);
                        _shouldPressButton = true;
                    }
                    else
                    {
                        _webArrows[0].SetActive(false);
                        _webArrows[1].SetActive(false);

                        _webArrows[2].SetActive(true);

                        var newscale = new Vector3(0, 0);
                        _webArrows[3].transform.localScale = newscale;
                        _webArrows[3].SetActive(true);
                    }
                }
            }
        }
        //mini game portion for the spider web
        else if (Input.acceleration.x > 0f && !_velBounce._playerCanMove && !_shouldPressButton || Input.GetKey(KeyCode.RightArrow))
        {
            if (_webArrows[2].activeInHierarchy)
            {
                var scale = _webArrows[3].transform.localScale;
                var scaleFactor = Mathf.Abs(Input.acceleration.x * _inputFactor);
                scale = new Vector3(scaleFactor, scaleFactor);
                _webArrows[3].transform.localScale = scale;


                if (_webArrows[3].transform.localScale.x >= _webArrows[2].transform.localScale.x || Input.GetKey(KeyCode.RightArrow))
                {
                    _arrowHitCount++;
                    var color = _web.GetComponent<SpriteRenderer>().color;
                    color.a -= .33f;
                    _web.GetComponent<SpriteRenderer>().color = color;

                    if (_arrowHitCount == 2)
                    {
                        _star.SetActive(true);
                        _webArrows[2].SetActive(false);
                        _webArrows[3].SetActive(false);
                        _shouldPressButton = true;
                    }
                    else
                    {
                        _webArrows[2].SetActive(false);
                        _webArrows[3].SetActive(false);

                        _webArrows[0].SetActive(true);

                        var newscale = new Vector3(0, 0);
                        _webArrows[1].transform.localScale = newscale;
                        _webArrows[1].SetActive(true);
                    }
                }

            }
        }
        else
            _player.velocity = new Vector3(0f, 0, 0f);

        //check and code to run if the forward dash is not active
        if (!_forwardDashActivated)
        {
            //check to see if the dashclick manager has registered a tap on the up dash button
            if (_player.velocity.y > 0 && _dashManager._isUpDownClicked || (_player.velocity.y < 0 && _dashManager._isUpDownClicked))
            {
                //flip back
                _dashManager._isUpDownClicked = false;

                if (_camera4._canDash)
                {
                    //_dashManager._isUpDownClicked = false;
                    _forwardDashActivated = true;
                    _shouldUnanchorCamera = true;
                    _currentPlayerPosDiff = 0f;
                }
                else
                    _dashManager._isUpDownClicked = false;
            }
            else
                _dashManager._isUpDownClicked = false;
        }
        else
            _dashManager._isUpDownClicked = false;

        //check and code to run for forward dash
        if (_forwardDashActivated)
        {
            //setting the speed and camera movement when doing the dash
            if (_shouldUnanchorCamera)
            {
                if (_player.velocity.y > 0)
                {
                    ChangeSpeedDash(.1f, .01f);
                    MoveCameraDash(-.2f, -.125f);
                }
                else
                {
                    ChangeSpeedDash(-.1f, -.01f);
                    MoveCameraDash(.2f, .125f);
                }

                _time += 1f * Time.deltaTime;

                if (_time > 1f)
                {
                    _shouldUnanchorCamera = false;
                }
            }
            //resets the camera back to the original position
            else
            {
                _time = 0f;

                if (_player.velocity.y > 0)
                {
                    ResetCameraDashUp(.175f, .12f);
                    CheckToDisableDash(8.5f);
                    ResetPlayerSpeed(7.5f);
                }
                else
                {
                    ResetCameraDashDown(-.175f, -.12f);
                    CheckToDisableDash(-8.5f);
                    ResetPlayerSpeed(-7.5f);
                }
            }
        }
        else
            _dashManager._isUpDownClicked = false;

#if UNITY_ANDROID
        //creating neutral zone for character movements

#endif

        //transform.position = Vector3.Lerp(transform.position, new Vector3(0f, 10f, 0f), Time.deltaTime * _activeMoveSpeed);
    }

    private void ChangeSpeedDash(float speedToIncreaseBy, float speedToIncreaseByWhenSlowing)
    {
        if (_velBounce._decrementGravity)
        {
            _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + speedToIncreaseByWhenSlowing);
        }
        else
        {
            _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + speedToIncreaseBy);
        }
    }

    private void ResetPlayerSpeed(float speed)
    {
        if (_player.velocity.y != speed)
            _player.velocity = new Vector2(_player.velocity.x, speed);
    }

    private void MoveCameraDash(float valToMove, float valToMoveWhenSlowing)
    {
        if (_velBounce._decrementGravity)
        {
            _camera4._camerasFocusPoint.transform.localPosition = new Vector3(0, _camera4._camerasFocusPoint.transform.localPosition.y + valToMoveWhenSlowing, _camera4._camerasFocusPoint.transform.localPosition.z);
        }
        else
        {
            _camera4._camerasFocusPoint.transform.localPosition = new Vector3(0, _camera4._camerasFocusPoint.transform.localPosition.y + valToMove, _camera4._camerasFocusPoint.transform.localPosition.z);
        }
    }

    private void ResetCameraDashUp(float valToAdd, float valToAddWhenSlowing)
    {
        ResetCamera(valToAdd, valToAddWhenSlowing);

        if (_camera4._camerasFocusPoint.localPosition.y > 8.5f)
        {
            _camera4._camerasFocusPoint.localPosition = new Vector3(0, 8.5f, transform.position.z);
        }
    }


    private void CheckToDisableDash(float speedToCheck)
    {
        if (_camera4._camerasFocusPoint.localPosition.y == speedToCheck)
        {
            _forwardDashActivated = false;
            _shouldUnanchorCamera = true;
        }
    }
    private void ResetCameraDashDown(float valToAdd, float valToAddWhenSlowing)
    {
        ResetCamera(valToAdd, valToAddWhenSlowing);

        if (_camera4._camerasFocusPoint.localPosition.y < -8.5f)
        {
            _camera4._camerasFocusPoint.localPosition = new Vector3(0, -8.5f, transform.position.z);
        }
    }

    private void ResetCamera(float valToAdd, float valToAddWhenSlowing)
    {
        if (_velBounce._decrementGravity)
        {
            _camera4._camerasFocusPoint.transform.localPosition = new Vector3(0, _camera4._camerasFocusPoint.transform.localPosition.y + valToAddWhenSlowing, _camera4._camerasFocusPoint.transform.localPosition.z);
        }
        else
        {
            _camera4._camerasFocusPoint.transform.localPosition = new Vector3(0, _camera4._camerasFocusPoint.transform.localPosition.y + valToAdd, _camera4._camerasFocusPoint.transform.localPosition.z);
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
       // if (_dashManager._isUpDownClicked)
         //   _dashManager._isUpDownClicked = false;
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
        if (collision.gameObject.tag == "WormMouth")
        {
            _sprite.color = _inWormColor;
        }

        if (collision.gameObject.tag == "WormTail" && _sprite.color != _originalColor)
        {
            _sprite.color = _originalColor;
        }

        if (collision.tag == "SpiderWebSticky" && !_superMoveManager._isSuperActivated)
        {
            _tapManager.gameObject.SetActive(false);
            _webArrows[0].SetActive(true);
            _webArrows[1].SetActive(true);
            _arrowHitCount = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "SpiderWebSticky" && !_superMoveManager._isSuperActivated)
        {
            _tapManager.gameObject.SetActive(true);
            _webArrows[0].SetActive(false);
            _webArrows[1].SetActive(false);
            _webArrows[2].SetActive(false);
            _webArrows[3].SetActive(false);
            _arrowHitCount = 0;
            _velBounce._playerCanMove = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Bullet"))
            collision.collider.isTrigger = true;
    }
}
