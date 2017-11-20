using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{

    public float _cloudMovementSpeed = 1.5f;
    public float _cloudColorTransitionSpeed = 0.25f;
    public Color _endCloudColor = new Color(0.25f, 0.25f, 0.25f, 1);

    private float _leftOutterBounds;
    private float _rightOutterBounds;
    private float _topOutterBounds;
    private float _bottomOutterBounds;
    private float _cloudYPosition;

    private VelocityBounce2 _playerVelocity;
    private Camera _camera;
    private Renderer _renderer;
    private PlayerControl _playerControl;
    private BackgroundColorChange _backgroundColor;
    private Color _startColor;
    private SpriteRenderer _spriteRenderer;
    private string _cloudColorString;
    private string _endCloudColorString;
    private float _transitionDownYValue = 0;

    private void Start()
    {
        _playerVelocity = FindObjectOfType<VelocityBounce2>();
        _playerControl = FindObjectOfType<PlayerControl>();
        _backgroundColor = FindObjectOfType<BackgroundColorChange>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _renderer = GetComponent<Renderer>();
        _camera = FindObjectOfType<Camera>();
        RandomizeCloudProperties();
        _cloudYPosition = transform.position.y;
        _startColor = _spriteRenderer.color;
    
    }

    private void FixedUpdate()
    {
        _topOutterBounds = _camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        _leftOutterBounds = _camera.ScreenToWorldPoint(new Vector2(0, 0)).x;
        _rightOutterBounds = _camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        _bottomOutterBounds = _camera.ScreenToWorldPoint(new Vector2(0, 0)).y;
        transform.Translate(Vector3.right * Time.deltaTime * _cloudMovementSpeed);
    }

    void Update()
    {

        _cloudColorString = _spriteRenderer.color.ToString();
        _endCloudColorString = _endCloudColor.ToString();

        if (_cloudColorString == _endCloudColorString)
        {
            if (_transitionDownYValue == 0)
            {
                _transitionDownYValue = transform.position.y;
            }
        }

        if (_backgroundColor._background.color != Color.white && _backgroundColor._background.transform.position.y >= _backgroundColor._backgroundChangeYValue)
        {
            if (_playerControl._player != null)
            {
                if (_playerControl._player.gameObject.activeSelf)
                {
                    if (_playerControl._player.velocity.y > 0)
                    {
                        _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, _endCloudColor, Time.deltaTime * _cloudColorTransitionSpeed);
                    }
                }
            }
        }

        if(_backgroundColor._background.color != Color.white)
        {
            if (_playerControl._player != null) 
            {
                if (_playerControl._player.gameObject.activeSelf)
                {
                    if (_playerControl._player.velocity.y < 0)
                    {
                        if (_transitionDownYValue == 0)
                        {
                            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, _startColor, Time.deltaTime * _cloudColorTransitionSpeed);
                        }
                        else if (transform.position.y <= _transitionDownYValue)
                        {
                            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, _startColor, Time.deltaTime * _cloudColorTransitionSpeed);
                        }

                    }
                }
            }
        }
    }

    private void OnBecameInvisible()
    {
            if (_playerControl._player != null)
            {
                if (_playerControl._player.gameObject.activeSelf)
                {
                    if (_playerControl._player.velocity.y > 0)
                    {
                        int randomPosition = Random.Range(0, 3);
                        var randomXPosition = Random.Range(_leftOutterBounds - 3, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
                        transform.position = new Vector3(randomXPosition, _topOutterBounds + randomPosition + _renderer.bounds.size.y, transform.position.z);
                        RandomizeCloudProperties();
                    }
                    else if (_playerVelocity._player.velocity.y <= 0)
                    {
                        int randomPosition = Random.Range(0, 3);
                        var randomXPosition = Random.Range(_leftOutterBounds - 3, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
                        var randomYPosition = (_bottomOutterBounds - randomPosition - _renderer.bounds.size.y);
                        if (randomYPosition <= 0)
                        {
                            randomYPosition = Random.Range(0, _topOutterBounds + _renderer.bounds.size.y);
                            transform.position = new Vector3(_leftOutterBounds - 3, randomYPosition, transform.position.z);
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
                    var randomYPosition = Random.Range(_bottomOutterBounds - _renderer.bounds.size.y, _topOutterBounds + _renderer.bounds.size.y);
                    transform.position = new Vector3(_leftOutterBounds - _renderer.bounds.size.x, randomYPosition, transform.position.z);
                    RandomizeCloudProperties();
                }
        }
    }

    private void RandomizeCloudProperties()
    {
        _cloudMovementSpeed = Random.Range(0.9f, 2f);
        float scaleSize = 0.0f;
        scaleSize = Random.Range(0.3f, 1.0f);
        transform.localScale = new Vector3(scaleSize, scaleSize, 1);
    }
}

