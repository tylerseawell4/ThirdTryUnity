using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapCameraFollow : MonoBehaviour {

    public Transform _target;
    public float _smoothSpeed = .125f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
       // var dP = _target.position;
       // var sP = Vector3.Lerp(transform.position, dP, Time.deltaTime * _smoothSpeed);
       // transform.position = _target.position;
	}
}
