using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public Rigidbody2D _myRidgidBody;
    public float _activeMoveSpeed;
    // Use this for initialization
    void Start () {
        _activeMoveSpeed = 5f;
    }
	
	// Update is called once per frame
	void Update () {
        MoveLeftRight();
	}
    private void MoveLeftRight()
    {
        if (Input.GetAxisRaw("Horizontal") > 0f || Input.acceleration.x > .01f)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _myRidgidBody.velocity = new Vector3(_activeMoveSpeed, _myRidgidBody.velocity.y, 0f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f || Input.acceleration.x < -.01f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _myRidgidBody.velocity = new Vector3(-_activeMoveSpeed, _myRidgidBody.velocity.y, 0f);
        }
        else
            _myRidgidBody.velocity = new Vector3(0f, _myRidgidBody.velocity.y, 0f);
    }
}
