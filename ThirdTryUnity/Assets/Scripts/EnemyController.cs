using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float _moveSpeed;
    private float _moveXPos;
    private float _currYPos;
    private bool _movingRight;
    private SpriteRenderer _sprite;
    private float _changeYDirectionTime;
    private float _acumTime = 0;
    private bool _goUp;
    // Use this for initialization
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _changeYDirectionTime = Random.Range(1, 5);
        if (Random.Range(0, 2) == 1)
        {
            _moveXPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
            _movingRight = true;
            _sprite.flipX = true;
            _goUp = true;
        }
        else
        {
            _moveXPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
            _movingRight = false;
            _sprite.flipX = false;
            _goUp = false;
        }
        _moveSpeed = Random.Range(4, 9);

        _currYPos = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _acumTime += 1 * Time.deltaTime;
        if (_acumTime >= _changeYDirectionTime && _goUp)
        {
            _acumTime = 0f;
            _currYPos = transform.position.y + 5f;
            _goUp = false;
        }
        else if (_acumTime >= _changeYDirectionTime && !_goUp)
        {
            _acumTime = 0f;
            _currYPos = transform.position.y - 5f;
            _goUp = true;
        }
    

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
        else
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(_moveXPos, _currYPos), _moveSpeed * Time.deltaTime);

    }
}
