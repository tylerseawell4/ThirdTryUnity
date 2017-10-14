using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndDestroyWeapon : MonoBehaviour {
    [Tooltip("How fast the weapon will shoot from player")]
    public float _bulletMoveSpeed = 10f;

    [Tooltip("How long until the weapon shot will be destroyed from the game view")]
    public float _destroyWeaponTime = 0.5f;

    [Tooltip("Will this 'bullet' keep a constant speed throughout the game play")]
    public bool _keepConstantSpeed = false;

    private Rigidbody2D _myRigidBody;
    private PlayerControl _playerControl;
    private VelocityBounce2 _playerVelocityScript;
    private Shoot _shoot;
    private float _initialBulletMoveSpeed;
    private void Start()
    {
        _playerControl = FindObjectOfType<PlayerControl>();
        _playerVelocityScript = FindObjectOfType<VelocityBounce2>();
        _shoot = FindObjectOfType<Shoot>();
        _initialBulletMoveSpeed = _bulletMoveSpeed;
        _myRigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (_playerControl._player != null)
        {
            if (!_playerVelocityScript._hitHeight)
            {
                if(name != "BulletDown")
                {
                    _myRigidBody.velocity = _playerControl._player.velocity;
                    _myRigidBody.AddForce(transform.up * 500);
                    name = "BulletUp";
                }
            }
            else
            {
                if(name != "BulletUp")
                {
                    _myRigidBody.velocity = _playerControl._player.velocity;
                    _myRigidBody.AddForce(transform.up * -500);
                    name = "BulletDown";
                }
            }
        }
    }
    private void OnEnable()
    {
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
}
