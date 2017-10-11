using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndDestroyWeapon : MonoBehaviour {
    [Tooltip("How fast the weapon will shoot from player")]
    public float _bulletMoveSpeed;

    [Tooltip("How long until the weapon shot will be destroyed from the game view")]
    public float _destroyWeaponTime;

    [Tooltip("Will this 'bullet' keep a constant speed throughout the game play")]
    public bool _keepConstantSpeed = false;

    private PlayerControl _playerControl;
    private Shoot _shoot;
    private float _initialBulletMoveSpeed;
    private void Start()
    {
        _playerControl = FindObjectOfType<PlayerControl>();
        _shoot = FindObjectOfType<Shoot>();
        _initialBulletMoveSpeed = _bulletMoveSpeed;
    }
    void Update()
    {
        if (_shoot._playerMovingUp)
        {
            _bulletMoveSpeed += 2; //TODO: need to modify the bullet speed when the player is moving upwards..
        }
        else
        {
            _bulletMoveSpeed = _initialBulletMoveSpeed;
        }
        transform.Translate(Vector3.up * Time.deltaTime * _bulletMoveSpeed);
       // Destroy(gameObject, _destroyWeaponTime);
    }
    private void OnEnable()
    {
        Invoke("Destroy", _destroyWeaponTime);
    }
    private void Destroy()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}
