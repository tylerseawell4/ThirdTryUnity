using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    [Tooltip("The 'bullet' that will shoot out of firepoint. The bullet Asset MUST have the MoveAndDestroyWeapon script attached to it.")]
    public Transform _bullet;
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
            Effect();
            _timeToSpawnEffect = Time.time + 1 / _effectSpawnTime;
        }
    }

    private void Effect()
    {
        Instantiate(_bullet, _firePointTransform.position, _firePointTransform.rotation);        
    }
}
