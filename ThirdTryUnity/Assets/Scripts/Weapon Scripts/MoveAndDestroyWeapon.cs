using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndDestroyWeapon : MonoBehaviour
{
    //[Tooltip("How fast the weapon will shoot from player")]
    //public float _bulletMoveSpeed = 10f;

    [Tooltip("How long until the weapon shot will be destroyed from the game view")]
    public float _destroyWeaponTime = 0.5f;

    [Tooltip("Will this 'bullet' keep a constant speed throughout the game play")]
    public bool _keepConstantSpeed = false;

    private Rigidbody2D _myRigidBody;
    private PlayerControl _playerControl;
    private KetchupShoot _shoot;
    public SpriteRenderer _bullet;
    private Color _originalColor;
    private bool _isFirstSpawnedIn;
    private int _frameCount;
    public float _addForceValue;
    public bool _shouldOverPenatrate;
    public bool _shouldFade;
    public int _damage;

    private float _leftOutterBounds;
    private float _rightOutterBounds;
    private float _topOutterBounds;
    private float _bottomOutterBounds;

    private Camera _camera;
    public bool _shouldFollow;
    private Color _inWormColor;

    private void Awake()
    {
        _playerControl = FindObjectOfType<PlayerControl>();
        _inWormColor = new Color(.25f, .25f, .25f, .6f);
        if (_playerControl.GetComponent<SpriteRenderer>().color == _inWormColor)
        {
            _bullet.color = _inWormColor;
        }
    }
    private void Start()
    {
        _shoot = FindObjectOfType<KetchupShoot>();
        _myRigidBody = GetComponent<Rigidbody2D>();
        _camera = FindObjectOfType<Camera>();

        _topOutterBounds = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        _leftOutterBounds = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
        _rightOutterBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        _bottomOutterBounds = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
    }
    void Update()
    {
        _topOutterBounds = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        _leftOutterBounds = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
        _rightOutterBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        _bottomOutterBounds = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;

        if (gameObject.layer != 14 && transform.position.y > _topOutterBounds || transform.position.y < _bottomOutterBounds || transform.position.x > _rightOutterBounds || transform.position.x < _leftOutterBounds)
            Destroy();

        if (_shouldFade)
        {
            Color color1 = _bullet.GetComponent<SpriteRenderer>().material.color;
            color1.a -= .02f;
            _bullet.GetComponent<SpriteRenderer>().material.color = color1;
        }

        if (_bullet.GetComponent<SpriteRenderer>().material.color.a <= 0)
            Destroy();

        if (_playerControl._player != null)
        {
            if (_playerControl._player.velocity.y >= 0)
            {
                if (name != "BulletDown")
                {
                    _bullet.flipY = true;

                    if (_shouldFollow)
                    {
                        _myRigidBody.velocity = _playerControl._player.velocity;
                    }
                    else
                    {
                        if (_isFirstSpawnedIn)
                        {
                            _frameCount++;
                            if (_frameCount >= 2)
                                _isFirstSpawnedIn = false;

                            _myRigidBody.velocity = _playerControl._player.velocity;
                        }
                        else
                            _myRigidBody.velocity = new Vector2(0, _playerControl._player.velocity.y);
                    }

                    _myRigidBody.AddForce(transform.up * _addForceValue);
                    name = "BulletUp";
                }
            }
            else if (_playerControl._player.velocity.y <= 0)
            {
                if (name != "BulletUp")
                {
                    _bullet.flipY = false;

                    if (_isFirstSpawnedIn)
                    {
                        _frameCount++;
                        if (_frameCount >= 2)
                            _isFirstSpawnedIn = false;

                        _myRigidBody.velocity = _playerControl._player.velocity;
                    }
                    else
                        _myRigidBody.velocity = new Vector2(0, _playerControl._player.velocity.y);
                    _myRigidBody.AddForce(transform.up * -_addForceValue);
                    name = "BulletDown";
                }
            }
        }
    }
    private void OnEnable()
    {
        //if (_playerVelocityScript._hitHeight)
        //    _bullet.flipY = true;
        _isFirstSpawnedIn = true;
        _frameCount = 0;
        name = "Bullet";
        if (gameObject.layer == 14)
            Invoke("Destroy", _destroyWeaponTime);
    }
    private void Destroy()
    {
        if (transform.parent != null)
            Destroy(transform.parent.gameObject);
        else
            Destroy(gameObject);
        //name = "Bullet";
        //gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is EdgeCollider2D)
            Destroy();

        if (collision.gameObject.tag.Contains("Worm"))
        {
            _bullet.color = _inWormColor;
            return;
        }

        if (collision.tag == "Player" || collision.tag == "Background" || collision.tag == "Bullet") return;

        if (!_shouldOverPenatrate)
            Destroy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "WormMouth")
        //{
        //   var col= gameObject.GetComponent<Collider2D>();
        //    col.isTrigger = true;
        //    Destroy();
        //}
    }
}
