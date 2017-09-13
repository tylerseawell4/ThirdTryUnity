using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D _myRidgidBody;
    public float _activeMoveSpeed;
    public Transform _topPlayerPoint;
    public Transform _bottomPlayerPoint;
    private bool _facingRight;
    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        _activeMoveSpeed = 5f;
        _facingRight = true;
    }
    private void FixedUpdate()
    {
#if UNITY_ANDROID
        //creating neutral zone for character movements
        if (Input.acceleration.x > .025f)
            _myRidgidBody.velocity = new Vector3(10f * Input.acceleration.x, _myRidgidBody.velocity.y, 0f);
        else if (Input.acceleration.x < -.025f)
            _myRidgidBody.velocity = new Vector3(10f * Input.acceleration.x, _myRidgidBody.velocity.y, 0f);
#endif

        //transform.position = Vector3.Lerp(transform.position, new Vector3(0f, 10f, 0f), Time.deltaTime * _activeMoveSpeed);
    }
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        MoveLeftRight();
#endif

#if UNITY_ANDROID
        //creating neutral zone for flip
        if (_myRidgidBody.velocity.x < -.025f && _facingRight)
        {
            _facingRight = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (_myRidgidBody.velocity.x > .025f && !_facingRight)
        {
            _facingRight = true;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
#endif
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
   
private void OnDrawGizmosSelected()
    {
        // Draws a line from the player transform to the targets position in the scene view only
        if (_topPlayerPoint != null)
        {            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _topPlayerPoint.position);
        }
        if (_bottomPlayerPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _bottomPlayerPoint.position);
        }
    }
}
