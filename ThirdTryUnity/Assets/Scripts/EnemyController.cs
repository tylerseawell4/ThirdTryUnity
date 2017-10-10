using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float _moveSpeed;
    private float _moveXPos;
    private bool _movingRight;
    private SpriteRenderer _sprite;
    // Use this for initialization
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        if (Random.Range(0, 2) == 1)
        {
            _moveXPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
            _movingRight = true;
            _sprite.flipX = true;
        }
        else
        {
            _moveXPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
            _movingRight = false;
            _sprite.flipX = false;
        }
        _moveSpeed = Random.Range(3, 9);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(_moveXPos, transform.position.y), _moveSpeed * Time.deltaTime);

        if (!_movingRight && transform.position.x <= _moveXPos)
        {
            _moveXPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
            _movingRight = true;
            _sprite.flipX = true;
            
        }
        if (_movingRight && transform.position.x >= _moveXPos)
        {
            _moveXPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
            _movingRight = false;
            _sprite.flipX = false;
        }

    }
}
