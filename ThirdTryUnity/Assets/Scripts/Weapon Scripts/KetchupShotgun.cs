using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetchupShotgun : MonoBehaviour
{
    private Transform _firePointTransform;
    private PlayerControl _playerControl;
    public Transform _firePtUpPos;
    public Transform _firePtDownPos;
    private TapManager _tapManager;
    private Rigidbody2D _myRigidBody;
    private GameObject _shotgunBlast;
    public GameObject _shotgunBlastUp;
    public GameObject _shotgunBlastDown;
    // Use this for initialization
    void Start()
    {
        _playerControl = FindObjectOfType<PlayerControl>();
        _tapManager = FindObjectOfType<TapManager>();
        _myRigidBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_tapManager._doubleTap)
        {
            if (_myRigidBody.velocity.y <= 0)
            {
                _shotgunBlast = _shotgunBlastUp;
                _firePointTransform = _firePtDownPos;
            }
            else if (_myRigidBody.velocity.y > 0)
            {
                _shotgunBlast = _shotgunBlastDown;
                _firePointTransform = _firePtUpPos;
            }

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
        GameObject obj = Instantiate(_shotgunBlast);
        if (obj == null) return;

        //if (Input.acceleration.x > .025f)
        //    obj.transform.position = new Vector3(_firePointTransform.position.x + (.25f + Mathf.Abs(Input.acceleration.x)), _firePointTransform.position.y, _firePointTransform.position.z);
        //else if (Input.acceleration.x < .025f)
        //    obj.transform.position = new Vector3(_firePointTransform.position.x - (.25f + Mathf.Abs(Input.acceleration.x)), _firePointTransform.position.y, _firePointTransform.position.z);
        //else
        obj.transform.position = new Vector3(_firePointTransform.position.x, _firePointTransform.position.y, _shotgunBlast.transform.position.z);

        obj.transform.rotation = _firePointTransform.rotation;
        obj.SetActive(true);
    }
}
