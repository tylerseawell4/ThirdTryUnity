using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSuperChild : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private SuperMoveManager _superMoveManager;
    private SuperKetchup _SuperKetchup;
    // Use this for initialization
    void Start()
    {
        var player = GameObject.Find("Player");
        if (player != null)
        {
            _playerHealth = player.GetComponent<PlayerHealth>();
            _superMoveManager = player.GetComponent<SuperMoveManager>();
            _SuperKetchup = GameObject.Find("Player").GetComponent<SuperKetchup>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Super" || collision.tag == "SuperActivation")
            return;

        if (tag == "Overshield")
        {
            if (collision.tag == "Player")
            {
                var inWormColor = new Color(.25f, .25f, .25f, .6f);
                if (_playerHealth.GetComponent<SpriteRenderer>().color != inWormColor)
                {
                    _playerHealth.IncreasePlayerHealth();
                    Destroy(gameObject);
                }
            }
        }

        if (tag == "EnemyDropling" && collision.tag == "Player")
        {
            var inWormColor = new Color(.25f, .25f, .25f, .6f);
            if (!_SuperKetchup._superActivated && _playerHealth.GetComponent<SpriteRenderer>().color != inWormColor)
            {
                var value = Math.Abs((transform.localScale.x / 100) * 2);
                _superMoveManager.IncreaseSuperBar(value);
            }
        }

    }
}
