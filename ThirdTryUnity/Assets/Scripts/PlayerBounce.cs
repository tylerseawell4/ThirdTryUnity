using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour {

    public Rigidbody2D _thePlayer;
    public Transform _startMarker;
    public Transform _endMarker;
    public float _speed = 1.0f;
    private float _startTime;
    private float _journeyLength;
    private bool _hitHeight;
    public float _increaseGravityScale;
    public float _increaseUpwardPlayerSpeed;
    public float _initialGravityScale;
    public int _numberOfBouncesToIncreaseBy;
    private int _bounceCount = 1;
    public float _increaseHeightOfEndPoint;
    // Use this for initialization
    void Start()
    {
        _thePlayer.gravityScale = _initialGravityScale;
        _hitHeight = true;
        _startTime = Time.time;
        _journeyLength = Vector3.Distance(_startMarker.position, _endMarker.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_hitHeight)
        {
            float distCovered = (Time.time - _startTime) * _speed;
            float fracJourney = distCovered / _journeyLength;
            transform.position = Vector3.Lerp(new Vector3(_startMarker.position.x, _startMarker.position.y, transform.position.z), new Vector3(_endMarker.position.x, _endMarker.position.y, transform.position.z), fracJourney);
        }
    }
    private void Update()
    {
        _endMarker.position = new Vector3(transform.position.x, _endMarker.position.y);
        _startMarker.position = new Vector3(transform.position.x, _startMarker.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MakeEmBounce")
        {
            _bounceCount++;
            _hitHeight = false;
            _startTime = Time.time;
            _speed += _increaseUpwardPlayerSpeed;
            _journeyLength = Vector3.Distance(_startMarker.position, _endMarker.position);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EndPoint")
        {
            _hitHeight = true;
            if (_bounceCount <= _numberOfBouncesToIncreaseBy)
            {
                _thePlayer.gravityScale += _increaseGravityScale;
                _endMarker.position = new Vector3(_endMarker.position.x, _endMarker.position.y + _increaseHeightOfEndPoint);
            }
        }
    }
}
