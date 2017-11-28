using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayoShoot : MonoBehaviour
{
    public float _fireRateCountdown = 8f;
    public Transform _firePtUpPos;
    public Transform _firePtDownPos;
    private TapManager _tapManager;
    private Rigidbody2D _myRigidBody;
    public GameObject _mayoBeam;
    private bool _isMayoBeamActive;
    private GameObject _obj;
    public bool _shouldPull;

    void Start()
    {
        _tapManager = FindObjectOfType<TapManager>();
        _myRigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //
        if (_obj != null && _fireRateCountdown > 0)
        {
            if (_tapManager._doubleTap)
            {
                _tapManager._doubleTap = false;
                _shouldPull = !_shouldPull;
            }

            _fireRateCountdown -= 1 * Time.deltaTime;

            Color color1 = _obj.GetComponent<SpriteRenderer>().material.color;
            color1.a -= .002f;
            _obj.GetComponent<SpriteRenderer>().material.color = color1;

            _obj.GetComponent<Rigidbody2D>().velocity = _myRigidBody.velocity;
            if (_myRigidBody.velocity.y >= 0)
                _obj.transform.rotation = Quaternion.Euler(0, 0, 180f);
            else
                _obj.transform.rotation = Quaternion.Euler(0, 0, 0f);

            _obj.transform.position = transform.position;
        }
        else
        {
            DestroyBeam();
        }

        if (_tapManager._singleTap)
        {
            //flip back to false
            _tapManager._singleTap = false;

            if (_isMayoBeamActive)
            {
                DestroyBeam();
                return;
            }

            Fire();
        }
        else
            _tapManager._singleTap = false;
    }
    private void Fire()
    {
        _isMayoBeamActive = true;
        _obj = Instantiate(_mayoBeam);
        if (_obj == null) return;

        _obj.transform.position = transform.position;

        if (_myRigidBody.velocity.y >= 0)
            _obj.transform.rotation = Quaternion.Euler(0, 0, 180f);
        else
            _obj.transform.rotation = Quaternion.Euler(0, 0, 0f);

        _obj.SetActive(true);
    }

    public void DestroyBeam()
    {
        _shouldPull = false;
        _fireRateCountdown = 8f;
        _isMayoBeamActive = false;
        Destroy(_obj);
    }
}
