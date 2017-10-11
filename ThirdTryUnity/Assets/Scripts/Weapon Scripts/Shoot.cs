using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    [Tooltip("The 'bullet' that will shoot out of firepoint. The bullet Asset MUST have the MoveAndDestroyWeapon script attached to it.")]
    public GameObject _bullet;
    [Tooltip("How long before the player can shoot another bullet from this firepoint")]
    public float _effectSpawnTime;
    public bool _playerMovingUp;

    private float _timeToSpawnEffect;
    private Transform _firePointTransform;
    private PlayerControl _playerControl;

    
    // Use this for initialization
    void Start () {       

        _firePointTransform = transform;
        _playerControl = FindObjectOfType<PlayerControl>();
        _playerMovingUp = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
            if (_playerControl._player.velocity.y > 0)
            {
                _playerMovingUp = true;
            }
            else
            {
                _playerMovingUp = false;
            }

        }
    }
    private void ShootBullet()
    {
        if (Time.time >= _timeToSpawnEffect)
        {
            Fire();
            _timeToSpawnEffect = Time.time + 1 / _effectSpawnTime;
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
