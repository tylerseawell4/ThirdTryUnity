using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Tooltip("The 'bullet' that will shoot out of firepoint. The bullet Asset MUST have the MoveAndDestroyWeapon script attached to it.")]
    public GameObject _bullet;
    [Tooltip("How many 'bullets' can fire per second")]
    public float _fireRate;
    private float _fireRateCountdown;
    private Transform _firePointTransform;
    private PlayerControl _playerControl;
    public Transform _firePtUpPos;
    public Transform _firePtDownPos;
    private bool _isInitialFire;
    private TapManager _tapManager;
    private Rigidbody2D _myRigidBody;

    void Start()
    {
        _playerControl = FindObjectOfType<PlayerControl>();
        _isInitialFire = true;
        _fireRateCountdown = 0;
        _tapManager = FindObjectOfType<TapManager>();
        _myRigidBody = GetComponent<Rigidbody2D>();
       
    }

    void FixedUpdate()
    {
        _fireRateCountdown += 1 * Time.deltaTime;

        if ((_tapManager._singleTap && _fireRateCountdown >= _fireRate) || (_tapManager._singleTap && _isInitialFire))
        {
            if (_myRigidBody.velocity.y <= 0)
                _firePointTransform = _firePtDownPos;
            else if (_myRigidBody.velocity.y > 0)
                _firePointTransform = _firePtUpPos;

            Fire();

            _fireRateCountdown = 0;

            _isInitialFire = false;

            //flip back to false
            _tapManager._singleTap = false;
        }
        else
            _tapManager._singleTap = false;
    }
    private void Fire()
    {
        //calling the generic get pooled object class to obtain game objects instead of instantiating them here
        GameObject obj = ObjectPooling._current.GetPooledObject();
        if (obj == null) return;

        //if (Input.acceleration.x > .025f)
        //    obj.transform.position = new Vector3(_firePointTransform.position.x + (.25f + Mathf.Abs(Input.acceleration.x)), _firePointTransform.position.y, _firePointTransform.position.z);
        //else if (Input.acceleration.x < .025f)
        //    obj.transform.position = new Vector3(_firePointTransform.position.x - (.25f + Mathf.Abs(Input.acceleration.x)), _firePointTransform.position.y, _firePointTransform.position.z);
        //else
        obj.transform.position = _firePointTransform.position;

        obj.transform.rotation = _firePointTransform.rotation;
        obj.SetActive(true);
    }
}
