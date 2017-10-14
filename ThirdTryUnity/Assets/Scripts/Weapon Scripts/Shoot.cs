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

    private float _nextTimeToFire = 0f;
    private Transform _firePointTransform;
    private PlayerControl _playerControl;
    public Transform _firePtUpPos;
    public Transform _firePtDownPos;
    private VelocityBounce2 _playerVelocityScript;
    private bool _isInitialFire;

    void Start()
    {
        _playerControl = FindObjectOfType<PlayerControl>();
        _playerVelocityScript = FindObjectOfType<VelocityBounce2>();
        _isInitialFire = true;
        _fireRateCountdown = 0;
    }

    void FixedUpdate()
    {
        _fireRateCountdown += 1 * Time.deltaTime;

        if ((Input.GetKeyDown(KeyCode.Space) && _fireRateCountdown >= _fireRate) || (Input.GetKeyDown(KeyCode.Space) && _isInitialFire))
        {
            _isInitialFire = false;
            if (!_playerVelocityScript._hitHeight)
                _firePointTransform = _firePtUpPos;
            else
                _firePointTransform = _firePtDownPos;

            _fireRateCountdown = 0;

            Fire();
        }
    }
    private void Fire()
    {
        //calling the generic get pooled object class to obtain game objects instead of instantiating them here
        GameObject obj = ObjectPooling._current.GetPooledObject();
        if (obj == null) return;

        obj.transform.position = _firePointTransform.position;
        obj.transform.rotation = _firePointTransform.rotation;
        obj.SetActive(true);
    }
}
