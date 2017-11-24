using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustardShoot : MonoBehaviour
{
    [Tooltip("How many 'bullets' can fire per second")]
    public float _fireRate;
    private Transform _firePointTransform;
    public Transform _firePtUpPos;
    public Transform _firePtDownPos;
    private bool _isInitialFire;
    private TapManager _tapManager;
    private Rigidbody2D _myRigidBody;
    public GameObject _mustardBlast;

    void Start()
    {
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
        GameObject obj = Instantiate(_mustardBlast);
        if (obj == null) return;

        obj.transform.position = new Vector3(_firePointTransform.position.x, _firePointTransform.position.y, _mustardBlast.transform.position.z);

        obj.transform.rotation = _firePointTransform.rotation;
        obj.SetActive(true);
    }
}
