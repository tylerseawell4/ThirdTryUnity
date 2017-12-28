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
    private bool _shouldRotate;
    private float _rotationAmount;
    private bool _moveWithBeam;
    private Rigidbody2D _rigidbody;
    private MayoShoot _mayoShoot;
    private float _knockBackTime;
    private Transform _target;
    private Vector3 _originalTransformUp;
    private EnemyDeath _enemyDeath;
    private bool _canDieFromForce;
    public bool _shouldChangeYPos;
    public bool _shouldDieFromBeingOffSceen;

    // Use this for initialization
    void Start()
    {
        _canDieFromForce = false;
        _knockBackTime = 1f;
        _enemyDeath = GetComponent<EnemyDeath>();
        _originalTransformUp = transform.up;
        _mayoShoot = FindObjectOfType<MayoShoot>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerControl = FindObjectOfType<PlayerControl>();
        _target = _playerControl.transform;
        _sprite = GetComponent<SpriteRenderer>();
        if (gameObject.name.Contains("Wasp"))
        {
            if (transform.position.x <= Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x && transform.position.x >= Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 3)
                _sprite.flipY = true;

            if (_playerControl != null && _playerControl._player.velocity.y < 0)
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
            if (_playerControl != null && _playerControl._player.velocity.y > 0)
                _goUp = true;
            else
                _goUp = false;

            if(gameObject.tag != "BurstBug")
            _moveSpeed = Random.Range(4, 9);
            else
                _moveSpeed = 2;

            _currYPos = transform.position.y;
        }
    }

    void Update()
    {
        if (_canDieFromForce)
        {
            _knockBackTime -= Time.deltaTime * 1f;
            if (_knockBackTime <= 0)
            {
                _knockBackTime = 1f;
                _canDieFromForce = false;
                _rigidbody.velocity = Vector2.zero;
                _currYPos = transform.position.y;
            }
        }
        var screenHeightTop = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        var screenHeightBottom = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
        if (_shouldDieFromBeingOffSceen)
        {
            var check1 = gameObject.transform.position.y + 35;
            var check2 = gameObject.transform.position.y - 35;
            if (check1 < screenHeightTop || check2 > screenHeightBottom)
                Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (!_moveWithBeam)
        {
            if (!gameObject.name.Contains("Wasp"))
            {
                _acumTime += 1 * Time.deltaTime;
                if (_acumTime >= _changeYDirectionTime && _goUp && _shouldChangeYPos)
                {
                    _acumTime = 0f;
                    _currYPos = transform.position.y + 5f;
                }
                else if (_acumTime >= _changeYDirectionTime && !_goUp && _shouldChangeYPos)
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
        else
        {
            if (!_mayoShoot._shouldPull)
            {
                if (_rigidbody != null && _playerControl._player.velocity.y > 0)
                    _rigidbody.velocity = new Vector2(_playerControl._player.velocity.x, _playerControl._player.velocity.y + 4f);
                else if (_rigidbody != null && _playerControl._player.velocity.y <= 0)
                    _rigidbody.velocity = new Vector2(_playerControl._player.velocity.x, _playerControl._player.velocity.y + -4f);
            }
            else
            {
                if (_rigidbody != null && _playerControl._player.velocity.y > 0)
                    _rigidbody.velocity = new Vector2(_playerControl._player.velocity.x, _playerControl._player.velocity.y - 8f);
                else if (_rigidbody != null && _playerControl._player.velocity.y <= 0)
                    _rigidbody.velocity = new Vector2(_playerControl._player.velocity.x, _playerControl._player.velocity.y + 8f);
            }
        }

        if (_shouldRotate && !gameObject.name.Contains("Wasp"))
        {
            _rotationAmount += 5f;
            transform.rotation = Quaternion.Euler(0, 0, _rotationAmount);
        }

        if (!_shouldRotate && _rotationAmount != 0)
        {
            _rotationAmount -= 5f;
            transform.rotation = Quaternion.Euler(0, 0, _rotationAmount);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WormMouth") return;

        if (collision.gameObject.tag == "Beam")
        {
            _canDieFromForce = true;
            _moveWithBeam = true;
            _shouldRotate = true;

            if (_rigidbody != null && _playerControl._player.velocity.y > 0)
                _rigidbody.velocity = new Vector2(_playerControl._player.velocity.x, _playerControl._player.velocity.y + 3f);
            else if (_rigidbody != null && _playerControl._player.velocity.y <= 0)
                _rigidbody.velocity = new Vector2(_playerControl._player.velocity.x, _playerControl._player.velocity.y + -3f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Beam")
        {
            _canDieFromForce = true;
            _knockBackTime = 1f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Beam")
        {
            if (_rigidbody != null)
                _rigidbody.velocity = Vector2.zero;
            _shouldRotate = false;
            _moveWithBeam = false;
            _canDieFromForce = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12 && _canDieFromForce || collision.gameObject.tag == "Ground" && _canDieFromForce)
        {
            if (collision.gameObject.layer == 12)
                collision.gameObject.GetComponent<EnemyDeath>().Die();

            _enemyDeath.Die();
        }
        if (collision.gameObject.tag == "Ground")
        {
            _goUp = true;
        }
        if (collision.gameObject.tag == "Mayoshield")
        {
            //var ctPt = collision.contacts[0].point;
            if (_playerControl._player.velocity.y > 0)
            {
                _rigidbody.AddForce(Vector2.up * .075f);
                _canDieFromForce = true;
            }
            else
            {
                _rigidbody.AddForce(Vector2.down * .075f);
                _canDieFromForce = true;
            }
        }
        if (!gameObject.name.Contains("Wasp") && collision.gameObject.layer == 12)
        {
            if (_movingRight)
            {
                _movingRight = false;
                if (_sprite != null)
                    _sprite.flipX = false;
                _moveXPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
            }
            else if (!_movingRight)
            {
                _movingRight = true;
                if (_sprite != null)
                    _sprite.flipX = true;
                _moveXPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
            }
        }
    }

}
