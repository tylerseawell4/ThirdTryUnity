using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndDestroyWeapon : MonoBehaviour
{
    [Tooltip("How fast the weapon will shoot from player")]
    public float _bulletMoveSpeed = 10f;

    [Tooltip("How long until the weapon shot will be destroyed from the game view")]
    public float _destroyWeaponTime = 0.5f;

    [Tooltip("Will this 'bullet' keep a constant speed throughout the game play")]
    public bool _keepConstantSpeed = false;

    private Rigidbody2D _myRigidBody;
    private PlayerControl _playerControl;
    private Shoot _shoot;
    public SpriteRenderer _bullet;
    private float _initialBulletMoveSpeed;
    private bool _isFirst;
    private int _frameCount;

    private void Start()
    {
        _playerControl = FindObjectOfType<PlayerControl>();
        _shoot = FindObjectOfType<Shoot>();
        _initialBulletMoveSpeed = _bulletMoveSpeed;
        _myRigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (_playerControl._player != null)
        {
            if (_playerControl._player.velocity.y >0)
            {
                if (name != "BulletDown")
                {
                    _bullet.flipY = true;

                    if (_isFirst)
                    {
                        _frameCount++;
                        if (_frameCount >= 2)
                            _isFirst = false;

                        _myRigidBody.velocity = _playerControl._player.velocity;
                    }
                    else
                        _myRigidBody.velocity = new Vector2(0, _playerControl._player.velocity.y);

                    _myRigidBody.AddForce(transform.up * 500);
                    name = "BulletUp";
                }
            }
            else if (_playerControl._player.velocity.y <= 0)
            {
                if (name != "BulletUp")
                {
                    _bullet.flipY = false;

                    if (_isFirst)
                    {
                        _frameCount++;
                        if (_frameCount >= 2)
                            _isFirst = false;

                        _myRigidBody.velocity = _playerControl._player.velocity;
                    }
                    else
                        _myRigidBody.velocity = new Vector2(0, _playerControl._player.velocity.y);
                    _myRigidBody.AddForce(transform.up * -500);
                    name = "BulletDown";
                }
            }
        }
    }
    private void OnEnable()
    {
        //if (_playerVelocityScript._hitHeight)
        //    _bullet.flipY = true;
        _isFirst = true;
        _frameCount = 0;
        name = "Bullet";
        Invoke("Destroy", _destroyWeaponTime);
    }
    private void Destroy()
    {
        name = "Bullet";
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Nonlethal") return;

        if (collision.tag == "Player") return;

        Destroy();
    }
}
