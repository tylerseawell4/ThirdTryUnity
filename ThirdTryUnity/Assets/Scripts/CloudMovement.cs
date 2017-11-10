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
    private Renderer _renderer;
    private void Start()
    {
        _playerVelocity = FindObjectOfType<VelocityBounce2>();
        _renderer = GetComponent<Renderer>();
        RandomizeCloudProperties();
        _cloudYPosition = transform.position.y;
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _speed);
    }

    private void OnBecameInvisible()
    {
        if (_player.activeSelf)
        {
            if (_playerVelocity._player.velocity.y > 0)
            {
                int randomPosition = Random.Range(0, 3);
                var randomXPosition = Random.Range(_leftOutterBounds.position.x - 3, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
                transform.position = new Vector3(randomXPosition, _topOutterBounds.position.y + randomPosition, transform.position.z);
                RandomizeCloudProperties();
            }
            else if (_playerVelocity._player.velocity.y <= 0)
            {
                int randomPosition = Random.Range(0, 3);
                var randomXPosition = Random.Range(_leftOutterBounds.position.x - 3, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
                var randomYPosition = (_bottomOutterBounds.position.y + randomPosition);
                if (randomYPosition <= 0)
                {
                    randomYPosition = Random.Range(0, _topOutterBounds.position.y);
                    transform.position = new Vector3(_leftOutterBounds.position.x - 3, randomYPosition, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(randomXPosition, randomYPosition, transform.position.z);
                }
                RandomizeCloudProperties();
            }
        }
        else
        {
            var randomYPosition = Random.Range(_bottomOutterBounds.position.y, _topOutterBounds.position.y);
            transform.position = new Vector3(_leftOutterBounds.position.x - _renderer.bounds.size.x, randomYPosition, transform.position.z);
            RandomizeCloudProperties();
        }
    }

    private void RandomizeCloudProperties()
    {
        _speed = Random.Range(0.9f, 2f);
        float scaleSize = 0.0f;
        scaleSize = Random.Range(0.3f, 1.0f);
        transform.localScale = new Vector3(scaleSize, scaleSize, 1);
    }
}

