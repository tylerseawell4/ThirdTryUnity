using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float _moveSpeed;
    private float _moveXPos;
    private float _currYPos;
    private bool _movingRight;
    private SpriteRenderer _sprite;
    private float _changeYDirectionTime;
    private float _acumTime = 0;
    private bool _goUp;
    private PlayerControl _playerControl;

    // Use this for initialization
    void Start()
    {
        _playerControl = FindObjectOfType<PlayerControl>();
        _sprite = GetComponent<SpriteRenderer>();
        if (gameObject.name.Contains("Wasp"))
        {
            if (transform.position.x == Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 1.5f)
                _sprite.flipY = true;

            if (_playerControl._player.velocity.y < 0)
            {
                _sprite.flipX = true;
                _moveSpeed = -2.5f;
            }
            else
                _moveSpeed = 2.5f;
        }
        else
        {
            _changeYDirectionTime = Random.Range(1, 3);
            if (Random.Range(0, 2) == 1)
            {
                _moveXPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
                _movingRight = true;
                _sprite.flipX = true;
                _goUp = true;
            }
            else
            {
                _moveXPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
                _movingRight = false;
                _sprite.flipX = false;
                
            }
            if (_playerControl._player.velocity.y > 0)
                _goUp = true;
            else
                _goUp = false;

            _moveSpeed = Random.Range(4, 9);

            _currYPos = transform.position.y;
        }
    }

    void Update()
    {
        var screenHeightTop = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        var screenHeightBottom = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
        var check1 = gameObject.transform.position.y + 35;
        var check2 = gameObject.transform.position.y - 35;
        if (check1 < screenHeightTop || check2 > screenHeightBottom)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (!gameObject.name.Contains("Wasp"))
        {
            _acumTime += 1 * Time.deltaTime;
            if (_acumTime >= _changeYDirectionTime && _goUp)
            {
                _acumTime = 0f;
                _currYPos = transform.position.y + 5f;
            }
            else if (_acumTime >= _changeYDirectionTime && !_goUp)
            {
                _acumTime = 0f;
                _currYPos = transform.position.y - 5f;
            }


            if (!_movingRight && transform.position.x <= _moveXPos + 1)
            {
                _moveXPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
                _movingRight = true;
                _sprite.flipX = true;

            }
            if (_movingRight && transform.position.x >= _moveXPos - 1)
            {
                _moveXPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
                _movingRight = false;
                _sprite.flipX = false;
            }
            else
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_moveXPos, _currYPos, transform.position.z), _moveSpeed * Time.deltaTime);
        }
        else
            //need .left since it is rotated to move UP
            transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime);
    }
}
