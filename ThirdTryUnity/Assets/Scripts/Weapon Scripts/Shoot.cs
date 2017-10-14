using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    [Tooltip("The 'bullet' that will shoot out of firepoint. The bullet Asset MUST have the MoveAndDestroyWeapon script attached to it.")]
    public GameObject _bullet;
    [Tooltip("How many 'bullets' can fire per second")]
    public float _fireRate = 3;

    private float _nextTimeToFire = 0f;
    private Transform _firePointTransform;
    private PlayerControl _playerControl;

    
    void Start () {       

        _firePointTransform = transform;
        _playerControl = FindObjectOfType<PlayerControl>();
    }
	

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;
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
