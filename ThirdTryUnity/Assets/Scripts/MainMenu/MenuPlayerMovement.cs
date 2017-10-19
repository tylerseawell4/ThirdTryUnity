using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerMovement : MonoBehaviour {
    public Transform _leftCamera;
    public Transform _rightCamera;
    public Transform _bottomCamera;
    public Transform _topCamera;
    public float _speed = 5;

    private float _yMin;
    private float _xMin;
    private float _yMax;
    private float _xMax;

    // Use this for initialization
    void Start () {
        _yMin = _bottomCamera.transform.position.y;
        _yMax = _topCamera.transform.position.y;
        _xMin = _leftCamera.transform.position.x;
        _xMax = _rightCamera.transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
               
    }
}
