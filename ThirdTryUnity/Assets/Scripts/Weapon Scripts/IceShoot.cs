using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShoot : MonoBehaviour {
    [Tooltip("How many 'bullets' can fire per second")]
    public float _fireRate = 0.25f;
    private Transform _firePointTransform;
    public Transform _firePtUpPos;
    public Transform _firePtDownPos;
    private bool _isInitialFire;
    private float _fireRateCountdown;
    private TapManager _tapManager;
    private Rigidbody2D _myRigidBody;
    public GameObject _iceBlast;

    // Use this for initialization
    void Start () {
        _tapManager = FindObjectOfType<TapManager>();
        _myRigidBody = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        if (_tapManager._singleTap)
        {
            //flip back to false
            _tapManager._singleTap = false;

            if (_myRigidBody.velocity.y <= 0)
                _firePointTransform = _firePtDownPos;
            else if (_myRigidBody.velocity.y > 0)
                _firePointTransform = _firePtUpPos;

            Fire();
        }
        else
            _tapManager._singleTap = false;
    }
    private void Fire()
    {
        GameObject obj = Instantiate(_iceBlast);
        if (obj == null) return;

        obj.transform.position = new Vector3(_firePointTransform.position.x, _firePointTransform.position.y, _iceBlast.transform.position.z);

        obj.transform.rotation = _firePointTransform.rotation;
        obj.SetActive(true);
    }
}
