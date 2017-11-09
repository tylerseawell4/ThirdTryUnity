﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public SpriteRenderer _renderer;
    private int _healthCount;
    private Color _originalColor;
    private PlayerDeath _playerDeath;
    private PlayerControl _playerControl;
    public GameObject _super;

    // Use this for initialization
    private void Awake()
    {
        _healthCount = 1;
        _originalColor = _renderer.color;
        _playerDeath = GetComponent<PlayerDeath>();
        _playerControl = GetComponent<PlayerControl>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Super") return;

        if(collision.gameObject.tag == "Overshield")
        {
            IncreasePlayerHealth();
        }
        //12 is enemy layer
        if(collision.gameObject.layer == 12)
            DecreasePlayerHeath();
    }
    public void IncreasePlayerHealth()
    {
        _healthCount++;
        if (_healthCount == 2)
        {
            _renderer.color = Color.green;
        }
        else if (_healthCount == 3)
        {
            _renderer.color = Color.yellow;
        }
        else if (_healthCount == 4)
        {
            _renderer.color = Color.blue;
        }
    }
    private void DecreasePlayerHeath()
    {
        if (_playerControl._superActivated) return;

        _healthCount--;

        if(_healthCount == 0)
        {
            _playerDeath.Die();
            return;
        }
        if (_healthCount == 1)
        {
            _renderer.color = _originalColor;
        }
        else if (_healthCount == 2)
        {
            _renderer.color = Color.green;
        }
        else if (_healthCount == 3)
        {
            _renderer.color = Color.yellow;
        }
        else if (_healthCount == 4)
        {
            _renderer.color = Color.blue;
        }

    }
    public int GetPlayerHealth()
    {
        return _healthCount;
    }
}
