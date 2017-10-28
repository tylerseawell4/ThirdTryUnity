using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{

    public float _speed = 1.5f;
    public Transform _leftOutterBounds;
    public Transform _rightOutterBounds;
    public Transform _topOutterBounds;
    public Transform _bottomOutterBounds;
    public GameObject _player;

    private Rigidbody2D _playerRigidBody;
    private VelocityBounce2 _playerVelocity;
    private float _cloudYPosition;

    private void Start()
    {
        _playerVelocity = FindObjectOfType<VelocityBounce2>();
        _cloudYPosition = transform.position.y;
        _speed = Random.Range(0.5f, 3f);

    }

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _speed);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "RightCamera")
        {
            transform.position = new Vector3(_leftOutterBounds.transform.position.x - 2, transform.position.y, transform.position.z);
            _speed = Random.Range(0.5f, 2f);
            float scaleSize = 0.0f;
            scaleSize = Random.Range(0.3f, 0.9f);
            transform.localScale = new Vector3(scaleSize, scaleSize, 1);
        }
    }

    //**Note** In Unity this OnBecameInvisible() will not work if the scene view window is open - close that to view proper cloud movement..
    private void OnBecameInvisible()
    {
        if (_player.activeSelf)
        {
            if (_playerVelocity._player.velocity.y <= 0)
            {
                int randomPosition = Random.Range(0, 3);
                var randomXPosition = Random.Range(-2, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
                transform.position = new Vector3(randomXPosition, _bottomOutterBounds.position.y + randomPosition, transform.position.z);
            }
            else if (_playerVelocity._player.velocity.y > 0)
            {
                int randomPosition = Random.Range(0, 3);
                var randomXPosition = Random.Range(-2, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
                transform.position = new Vector3(randomXPosition, _topOutterBounds.position.y + randomPosition, transform.position.z);
            }

        }

    }
}

