using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Rigidbody2D _player;
    public GameObject[] _prefabsToSpawns;
    private double _heightYGoingUp;
    private VelocityBounce2 _playerHeightCheck;
    private bool _spawnAbove;
    private double _heightYGoingDown;
    private float[] leftrightXPos;

    // Use this for initialization
    void Start()
    {
        _heightYGoingUp = 0;
        _playerHeightCheck = _player.GetComponent<VelocityBounce2>();
        _spawnAbove = true;
        leftrightXPos = new float[2];
        leftrightXPos[0] = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 1f;
        leftrightXPos[1] = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 1f;

        //All enemies need to be in the "Enemy" layer to avoid collisions with the enviornment (clouds)
        foreach (var item in _prefabsToSpawns)
        {
            item.layer = LayerMask.NameToLayer("Enemy");
        }
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
            }
        }

        var pos = Math.Round(_player.transform.position.y, 2);
        if (_spawnAbove)
        { //.25 is to get a threshold up and down from the height since when the player POS is rounded
            if ((pos < (_heightYGoingUp + .25)) && (pos > (_heightYGoingUp - .25)) || pos == _heightYGoingUp)
            {
                _heightYGoingUp += _playerHeightCheck._playersExactHeight + _playerHeightCheck._increaseHeightBy;
                var yPos = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
                float scale = UnityEngine.Random.Range(0.6f, 1.5f);
                int index = UnityEngine.Random.Range(0, _prefabsToSpawns.Length);
                var xPos = 0f;
                if (_prefabsToSpawns[index].name != "Wasp")
                {
                    _prefabsToSpawns[index].transform.localScale = new Vector2(scale, scale);
                    
                }
                else
                {
                    var randomSide = UnityEngine.Random.Range(0, 2);
                    xPos = leftrightXPos[randomSide];
                }
                Instantiate(_prefabsToSpawns[index], new Vector3(xPos, yPos + 5f, _player.transform.position.z), _prefabsToSpawns[index].transform.rotation);
            }
            else if ((pos < (_heightYGoingUp / 2 + .25)) && (pos > (_heightYGoingUp / 2 - .25)) || pos == _heightYGoingUp / 2)
            {
                _heightYGoingUp += _playerHeightCheck._playersExactHeight + _playerHeightCheck._increaseHeightBy;
                var yPos = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
                float scale = UnityEngine.Random.Range(0.6f, 1.5f);
                int index = UnityEngine.Random.Range(0, _prefabsToSpawns.Length);
                var xPos = 0f;
                if (_prefabsToSpawns[index].name != "Wasp")
                {
                    _prefabsToSpawns[index].transform.localScale = new Vector2(scale, scale);
                    
                }
                else
                {
                    var randomSide = UnityEngine.Random.Range(0, 2);
                    xPos = leftrightXPos[randomSide];
                }
                Instantiate(_prefabsToSpawns[index], new Vector3(xPos, yPos + 5f, _player.transform.position.z), _prefabsToSpawns[index].transform.rotation);
            }
        }
        else if (!_spawnAbove)
        {
            if ((pos < (_heightYGoingDown + .25)) && (pos > (_heightYGoingDown - .25)) || pos == _heightYGoingDown)
            {
                if (_player.position.y >= 8f)
                {
                    _heightYGoingDown -= _playerHeightCheck._increaseHeightBy;
                    var yPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;

                    float scale = UnityEngine.Random.Range(0.6f, 1.5f);
                    int index = UnityEngine.Random.Range(0, _prefabsToSpawns.Length);
                    var xPos = 0f;
                    if (_prefabsToSpawns[index].name != "Wasp")
                    {
                        _prefabsToSpawns[index].transform.localScale = new Vector2(scale, scale);
                        
                    }
                    else
                    {
                        var randomSide = UnityEngine.Random.Range(0, 2);
                        xPos = leftrightXPos[randomSide];
                    }
                    Instantiate(_prefabsToSpawns[index], new Vector3(xPos, yPos - 1f, _player.transform.position.z), _prefabsToSpawns[index].transform.rotation);

                    scale = UnityEngine.Random.Range(0.6f, 1.5f);
                    index = UnityEngine.Random.Range(0, _prefabsToSpawns.Length);
                    xPos = 0f;
                    if (_prefabsToSpawns[index].name != "Wasp")
                    {
                        _prefabsToSpawns[index].transform.localScale = new Vector2(scale, scale);

                    }
                    else
                    {
                        var randomSide = UnityEngine.Random.Range(0, 2);
                        xPos = leftrightXPos[randomSide];
                    }
                    Instantiate(_prefabsToSpawns[index], new Vector3(xPos, yPos - 12f, _player.transform.position.z), _prefabsToSpawns[index].transform.rotation);
                }
            }
        }


    }
}
