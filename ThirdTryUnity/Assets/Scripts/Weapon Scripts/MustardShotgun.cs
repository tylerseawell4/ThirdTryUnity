using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustardShotgun : MonoBehaviour
{
    private TapManager _tapManager;
    private Rigidbody2D _myRigidBody;
    public GameObject _shotgunBlast;
    // Use this for initialization
    void Start()
    {
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
        //calling the generic get pooled object class to obtain game objects instead of instantiating them here
        Instantiate(_shotgunBlast, transform.position, Quaternion.Euler(0, 0, -135f)).SetActive(true);
        Instantiate(_shotgunBlast, transform.position, Quaternion.Euler(0, 0, -90)).SetActive(true);
        Instantiate(_shotgunBlast, transform.position, Quaternion.Euler(0, 0, -45)).SetActive(true);
        Instantiate(_shotgunBlast, transform.position, Quaternion.Euler(0, 0, 0f)).SetActive(true);
        Instantiate(_shotgunBlast, transform.position, Quaternion.Euler(0, 0, 45)).SetActive(true);
        Instantiate(_shotgunBlast, transform.position, Quaternion.Euler(0, 0, 90)).SetActive(true);
        Instantiate(_shotgunBlast, transform.position, Quaternion.Euler(0, 0, 135)).SetActive(true);
        Instantiate(_shotgunBlast, transform.position, Quaternion.Euler(0, 0, 180)).SetActive(true);
    }
}
