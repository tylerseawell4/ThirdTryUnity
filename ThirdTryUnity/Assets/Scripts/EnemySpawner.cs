using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Rigidbody2D _player;
    public GameObject _prefabToSpawn;
    private bool _stopSpawning;
    private double _heightYGoingUp;
    private VelocityBounce2 _playerHeightCheck;
    private bool _spawnAbove;
    private double _heightYGoingDown;
    private bool _isStartingHeight;

    // Use this for initialization
    void Start()
    {
        _stopSpawning = false;
        _isStartingHeight = true;
        _heightYGoingUp = 0;
        _playerHeightCheck = _player.GetComponent<VelocityBounce2>();
        _spawnAbove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_playerHeightCheck._hitHeight)
        {
            if (_spawnAbove)
            {
                //10 is just the number i found to work
                _heightYGoingDown = Math.Round(_playerHeightCheck._playersExactHeight, 2) - 10f;
                _spawnAbove = false;
            }
        }
        else if (_playerHeightCheck._hitBottom)
        {
            if (!_spawnAbove)
            {
                _spawnAbove = true;
                _heightYGoingUp = 0;
                _isStartingHeight = false;
            }
        }

        var pos = Math.Round(_player.transform.position.y, 2);
        if (_spawnAbove)
        { //.25 is to get a threshold up and down from the height since when the player POS is rounded
            if ((pos < (_heightYGoingUp + .25)) && (pos > (_heightYGoingUp - .25)) || pos == _heightYGoingUp)
            {
                _heightYGoingUp += _playerHeightCheck._playersExactHeight + _playerHeightCheck._increaseHeightBy;
                var yPos = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
                Instantiate(_prefabToSpawn, new Vector3(_player.transform.position.x, yPos + 5f, _player.transform.position.z), Quaternion.identity);
            }
        }
        else if (!_spawnAbove)
        {
            //10 is just the number i found to work, and checking if this is the starting height so as not to spawn enemy below the first one when coming down.
            if ((pos < (_playerHeightCheck._startingHeightCopy - 10f + .25)) && (pos > (_playerHeightCheck._startingHeightCopy - 10f - .25)) || pos == _playerHeightCheck._startingHeightCopy - 10f)
              _isStartingHeight = true;
            //.25 is to get a threshold up and down from the height since when the player POS is rounded
            if ((pos < (_heightYGoingDown + .25)) && (pos > (_heightYGoingDown - .25)) || pos == _heightYGoingDown)
            {
                if (!_isStartingHeight)
                {
                    _heightYGoingDown -= _playerHeightCheck._increaseHeightBy;
                    var yPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
                    Instantiate(_prefabToSpawn, new Vector3(_player.transform.position.x, yPos - 5f, _player.transform.position.z), Quaternion.identity);
                }
            }
        }


    }
}
