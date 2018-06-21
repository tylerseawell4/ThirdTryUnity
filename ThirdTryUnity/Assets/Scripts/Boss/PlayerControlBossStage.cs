using System.Collections;
using UnityEngine;
public class PlayerControlBossStage : MonoBehaviour
{
    public Rigidbody2D _player;
    private bool _rightDashActivated;
    private bool _leftDashActivated;
    private float posX;
    //private DashClickManager _dashManager;

    // Use this for initialization
    void Start()
    {
     //   _dashManager = FindObjectOfType<DashClickManager>();
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A)) //|| _dashManager._isLeftClicked)
        {
           // _dashManager._isLeftClicked = false;
            _leftDashActivated = true;
            _rightDashActivated = false;
            posX = _player.transform.position.x;
        }

        if (Input.GetKeyDown(KeyCode.D)) //|| _dashManager._isRightClicked)
        {
          //  _dashManager._isRightClicked = false;
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

    }
}
