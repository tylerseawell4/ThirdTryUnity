using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayoShield : MonoBehaviour
{
    public GameObject _mayoShield;
    private TapManager _tapManager;
    public float _timeActive;
    private PlayerControl _playercontrol;
    public Transform _fireUpPos;
    public Transform _fireDownPos;
    public Rigidbody2D _mayoRigidBody;
    private Vector3 _firePos;
    // Use this for initialization
    void Awake()
    {
        _playercontrol = FindObjectOfType<PlayerControl>();

        _firePos = Vector3.zero;

        if (_playercontrol._player.velocity.y < 0)
            _firePos = _fireDownPos.position;
        else if (_playercontrol._player.velocity.y > 0)
            _firePos = _fireUpPos.position;

        _tapManager = FindObjectOfType<TapManager>();
    }

    private void FixedUpdate()
    {
        if (_playercontrol._player.velocity.y < 0)
            _firePos = _fireDownPos.position;
        else if (_playercontrol._player.velocity.y > 0)
            _firePos = _fireUpPos.position;


        //need to instantiate instead of always having it in hierarchy
        if (_mayoShield.activeInHierarchy)
            _mayoRigidBody.transform.position = new Vector3(_playercontrol._player.position.x, _firePos.y, _mayoRigidBody.transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        if (_tapManager._doubleTap && _timeActive <= 0)
        {
            _timeActive = 5f;
            _tapManager._doubleTap = false;


            _mayoShield.SetActive(true);
        }

        if (_timeActive > 0f)
        {
            _timeActive -= Time.deltaTime * 1f;
        }
        else if (_timeActive <= 0f)
        {
            _timeActive = 0f;

            if (_mayoShield.activeInHierarchy)
                _mayoShield.SetActive(false);
        }
    }
}
