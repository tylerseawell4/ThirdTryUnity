using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public SpriteRenderer _renderer;
    private int _healthCount;
    private Color _originalColor;

    // Use this for initialization
    private void Awake()
    {
        _healthCount = 1;
        _originalColor = _renderer.color;
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        _healthCount--;

        if(_healthCount == 0)
        {
            GetComponent<PlayerDeath>().Die();
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
