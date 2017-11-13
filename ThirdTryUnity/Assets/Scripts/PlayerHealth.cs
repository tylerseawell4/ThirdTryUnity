using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public SpriteRenderer _renderer;
    private int _healthCount;
    private Color _originalColor;
    private PlayerDeath _playerDeath;
    private SuperMode _superMode;
    public GameObject _super;

    // Use this for initialization
    private void Awake()
    {
        _healthCount = 1;
        _originalColor = _renderer.color;
        _playerDeath = GetComponent<PlayerDeath>();
        _superMode = GetComponent<SuperMode>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Super" || gameObject.tag == "SuperActivation") return;

        //increase is called on the DetectSuperChild script

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
        if (_superMode._superActivated) return;

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
