using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    [Tooltip("What Layers the weapon will hit (not in use yet)")]
    public LayerMask _whatLayerObjectsToHit;

    [Tooltip("The weapon that will shoot from player")]
    public Transform _weapon;

    [Tooltip("How long before the player can shoot another weapon")]
    public float _effectSpawnTime;
    
    private float _timeToSpawnEffect = 0;
    private Transform _firePoint;
	
	void Start ()
    {
        _firePoint = transform.Find("FirePoint");
        if (_firePoint == null)
        {
            Debug.LogError("No Firepoint!");
        }
	}
	
	void Update ()
    {
		 if(Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }       
	}

    private void Shoot()
    {    
        if(Time.time >= _timeToSpawnEffect) 
        {
            Effect();          
            _timeToSpawnEffect = Time.time + 1 / _effectSpawnTime; 
        }      
    }

    private void Effect()
    {
        Instantiate(_weapon, _firePoint.position, _firePoint.rotation);        
    }
}
