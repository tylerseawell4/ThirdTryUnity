using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSuperChild : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private SuperMoveManager _superMoveManager;
    private SuperMode _superMode;
    // Use this for initialization
    void Start()
    {
        var player = GameObject.Find("Player");
        if (player != null)
        {
            _playerHealth = player.GetComponent<PlayerHealth>();
            _superMoveManager = player.GetComponent<SuperMoveManager>();
            _superMode = GameObject.Find("Player").GetComponent<SuperMode>();
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
            if (collision.tag == "Player")
            {
                _playerHealth.IncreasePlayerHealth();
                Destroy(gameObject);
            }

        if (tag == "EnemyDropling" && collision.tag == "Player")
        {
            if (!_superMode._superActivated)
            {
                var value = Math.Abs((transform.localScale.x / 100) * 2);
                _superMoveManager.IncreaseSuperBar(value);
            }
        }

    }
}
