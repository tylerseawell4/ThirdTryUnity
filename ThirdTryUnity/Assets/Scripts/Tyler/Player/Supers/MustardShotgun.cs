using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustardShotgun : MonoBehaviour
{
    private TapManager _tapManager;
    private Rigidbody2D _myRigidBody;
    public GameObject _shotgunBlast;
    private PlayerControl _playerControl;
    // Use this for initialization
    void Start()
    {
        _playerControl = FindObjectOfType<PlayerControl>();
        _tapManager = FindObjectOfType<TapManager>();
        _myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_tapManager._doubleTap)
        {
            Fire();
            //flip back to false
            _tapManager._doubleTap = false;
        }
        else
            _tapManager._doubleTap = false;
    }
    private void Fire()
    {
        if (_playerControl._player.velocity.y >= 0)
        {
            Instantiate(_shotgunBlast, new Vector3(transform.position.x + .5f, transform.position.y - .5f, transform.position.z), Quaternion.Euler(0, 0, -135f)).SetActive(true);
            Instantiate(_shotgunBlast, new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, -90)).SetActive(true);
            Instantiate(_shotgunBlast, new Vector3(transform.position.x + .5f, transform.position.y + .5f, transform.position.z), Quaternion.Euler(0, 0, -45)).SetActive(true);
            Instantiate(_shotgunBlast, new Vector3(transform.position.x, transform.position.y + .6f, transform.position.z), Quaternion.Euler(0, 0, 0f)).SetActive(true);
            Instantiate(_shotgunBlast, new Vector3(transform.position.x - .5f, transform.position.y + .5f, transform.position.z), Quaternion.Euler(0, 0, 45)).SetActive(true);
            Instantiate(_shotgunBlast, new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 90)).SetActive(true);
            Instantiate(_shotgunBlast, new Vector3(transform.position.x - .5f, transform.position.y - .5f, transform.position.z), Quaternion.Euler(0, 0, 135)).SetActive(true);
            Instantiate(_shotgunBlast, new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z), Quaternion.Euler(0, 0, 180)).SetActive(true);
        }
        else
        {
            Instantiate(_shotgunBlast, new Vector3(transform.position.x - .5f, transform.position.y + .5f, transform.position.z), Quaternion.Euler(0, 0, -135f)).SetActive(true);
            Instantiate(_shotgunBlast, new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, -90)).SetActive(true);
            Instantiate(_shotgunBlast, new Vector3(transform.position.x - .5f, transform.position.y - .5f, transform.position.z), Quaternion.Euler(0, 0, -45)).SetActive(true);
            Instantiate(_shotgunBlast, new Vector3(transform.position.x, transform.position.y - .6f, transform.position.z), Quaternion.Euler(0, 0, 0f)).SetActive(true);
            Instantiate(_shotgunBlast, new Vector3(transform.position.x + .5f, transform.position.y - .5f, transform.position.z), Quaternion.Euler(0, 0, 45)).SetActive(true);
            Instantiate(_shotgunBlast, new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 90)).SetActive(true);
            Instantiate(_shotgunBlast, new Vector3(transform.position.x + .5f, transform.position.y + .5f, transform.position.z), Quaternion.Euler(0, 0, 135)).SetActive(true);
            Instantiate(_shotgunBlast, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Quaternion.Euler(0, 0, 180)).SetActive(true);

        }
    }
}
