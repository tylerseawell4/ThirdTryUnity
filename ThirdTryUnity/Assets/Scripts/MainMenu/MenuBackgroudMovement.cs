using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroudMovement : MonoBehaviour {
    
    public float _speed = 1.5f;
    public Transform _leftOutterBounds;
    public Transform _rightOutterBounds;

    private float _cloudYPosition;

    private void Start()
    {
        _cloudYPosition = transform.position.y;       
    }

    void Update () {
        transform.Translate(Vector3.right * Time.deltaTime * _speed);        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "RightCamera")
        {
            transform.position = new Vector3(_leftOutterBounds.transform.position.x, _cloudYPosition, transform.position.z);
        } 
    }
}
