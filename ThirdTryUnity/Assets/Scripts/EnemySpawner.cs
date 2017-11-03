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
    public GameObject _wasp;
    private bool _shouldReAddWasp;

    // Use this for initialization
    void Start()
    {
        _heightYGoingUp = 0;
        _playerHeightCheck = _player.GetComponent<VelocityBounce2>();
        _spawnAbove = true;
        leftrightXPos = new float[2];
        leftrightXPos[0] = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 1.5f;
        leftrightXPos[1] = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 1.5f;

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

                int indexToReplaceWasp = 0;
                for (int i = 0; i < 2; i++)
                {
                    int index = 0;
                    var yPos = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
                    float scale = UnityEngine.Random.Range(0.6f, 1.5f);
                    index = UnityEngine.Random.Range(0, _prefabsToSpawns.Length);
                    var randomVal = 0f;
                    //12 is heightest at the moment that can be spawned
                    if (i == 0)
                    {
                        randomVal = UnityEngine.Random.Range(1, 4);
                    }
                    if (i == 1)
                    {
                        randomVal = UnityEngine.Random.Range(6, 9);
                    }
                    var xPos = 0f;
                    if (_prefabsToSpawns[index].name != "Wasp")
                    {
                        _prefabsToSpawns[index].transform.localScale = new Vector2(scale, scale);
                        Instantiate(_prefabsToSpawns[index], new Vector3(xPos, yPos + randomVal, _prefabsToSpawns[index].transform.position.z), _prefabsToSpawns[index].transform.rotation);
                    }
                    else
                    {
                        //adding an extra 2 so it doesnt spawn in the view of the camera since the sprite is so big
                        if (randomVal - 2f <= 10)
                            randomVal += 2f;
                        Instantiate(_prefabsToSpawns[index], new Vector3(leftrightXPos[0], yPos + randomVal, _prefabsToSpawns[index].transform.position.z), _prefabsToSpawns[index].transform.rotation);
                        Instantiate(_prefabsToSpawns[0], new Vector3(0, yPos + randomVal + 3, _prefabsToSpawns[index].transform.position.z), _prefabsToSpawns[0].transform.rotation);
                        Instantiate(_prefabsToSpawns[index], new Vector3(leftrightXPos[1], yPos + randomVal, _prefabsToSpawns[index].transform.position.z), _prefabsToSpawns[index].transform.rotation);

                        //removing wasp so it doesnt double or triple spawn them
                        indexToReplaceWasp = index;
                        _prefabsToSpawns[index] = _prefabsToSpawns[0];
                        _shouldReAddWasp = true;
                    }
                }
                //re-adding wasp
                if (_shouldReAddWasp)
                {
                    _prefabsToSpawns[indexToReplaceWasp] = _wasp;
                    _shouldReAddWasp = false;
                }
            }
            else if ((pos < (_heightYGoingUp / 2 + .25)) && (pos > (_heightYGoingUp / 2 - .25)) || pos == _heightYGoingUp / 2)
            {
                _heightYGoingUp += _playerHeightCheck._playersExactHeight + _playerHeightCheck._increaseHeightBy;

                int indexToReplaceWasp = 0;
                for (int i = 0; i < 2; i++)
                {
                    int index = 0;
                    var yPos = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
                    float scale = UnityEngine.Random.Range(0.6f, 1.5f);
                    index = UnityEngine.Random.Range(0, _prefabsToSpawns.Length);
                    var randomVal = 0f;
                    //12 is heightest at the moment that can be spawned
                    if (i == 0)
                    {
                        randomVal = UnityEngine.Random.Range(1, 4);
                    }
                    if (i == 1)
                    {
                        randomVal = UnityEngine.Random.Range(6, 9);
                    }
                    var xPos = 0f;
                    if (_prefabsToSpawns[index].name != "Wasp")
                    {
                        _prefabsToSpawns[index].transform.localScale = new Vector2(scale, scale);
                        Instantiate(_prefabsToSpawns[index], new Vector3(xPos, yPos + randomVal, _prefabsToSpawns[index].transform.position.z), _prefabsToSpawns[index].transform.rotation);
                    }
                    else
                    {
                        if (randomVal - 2f <= 10)
                            randomVal += 2f;
                        Instantiate(_prefabsToSpawns[index], new Vector3(leftrightXPos[0], yPos + randomVal, _prefabsToSpawns[index].transform.position.z), _prefabsToSpawns[index].transform.rotation);
                        Instantiate(_prefabsToSpawns[0], new Vector3(0, yPos + randomVal + 3, _prefabsToSpawns[index].transform.position.z), _prefabsToSpawns[0].transform.rotation);
                        Instantiate(_prefabsToSpawns[index], new Vector3(leftrightXPos[1], yPos + randomVal, _prefabsToSpawns[index].transform.position.z), _prefabsToSpawns[index].transform.rotation);

                        //removing wasp so it doesnt double or triple spawn them
                        indexToReplaceWasp = index;
                        _prefabsToSpawns[index] = _prefabsToSpawns[0];
                        _shouldReAddWasp = true;
                    }
                }
                //re-adding wasp
                if (_shouldReAddWasp)
                {
                    _prefabsToSpawns[indexToReplaceWasp] = _wasp;
                    _shouldReAddWasp = false;
                }
            }
        }
        else if (!_spawnAbove)
        {
            if ((pos < (_heightYGoingDown + .25)) && (pos > (_heightYGoingDown - .25)) || pos == _heightYGoingDown)
            {
                if (_player.position.y >= 8f)
                {
                    _heightYGoingDown -= _playerHeightCheck._increaseHeightBy;

                    int indexToReplaceWasp = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        int index = 0;
                        var yPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
                        var randomVal = 0f;
                        //12 is the lowest can be at the moment
                        if (i == 0)
                        {
                            randomVal = UnityEngine.Random.Range(1, 4);
                        }
                        if (i == 1)
                        {
                            randomVal = UnityEngine.Random.Range(6, 9);
                        }
                        if (i == 2)
                        {
                            randomVal = UnityEngine.Random.Range(10, 13);
                        }
                        float scale = UnityEngine.Random.Range(0.6f, 1.5f);
                        index = UnityEngine.Random.Range(0, _prefabsToSpawns.Length);
                        var xPos = 0f;
                        if (_prefabsToSpawns[index].name != "Wasp")
                        {
                            _prefabsToSpawns[index].transform.localScale = new Vector2(scale, scale);
                            Instantiate(_prefabsToSpawns[index], new Vector3(xPos, yPos - randomVal, _prefabsToSpawns[index].transform.position.z), _prefabsToSpawns[index].transform.rotation);
                        }
                        else
                        {
                            if (randomVal - 2f <= 10)
                                randomVal += 2f;
                            Instantiate(_prefabsToSpawns[index], new Vector3(leftrightXPos[0], yPos - randomVal, _prefabsToSpawns[index].transform.position.z), _prefabsToSpawns[index].transform.rotation);
                            Instantiate(_prefabsToSpawns[0], new Vector3(0, yPos - randomVal -3, _prefabsToSpawns[index].transform.position.z), _prefabsToSpawns[0].transform.rotation);
                            Instantiate(_prefabsToSpawns[index], new Vector3(leftrightXPos[1], yPos - randomVal, _prefabsToSpawns[index].transform.position.z), _prefabsToSpawns[index].transform.rotation);

                            //removing wasp so it doesnt double or triple spawn them
                            indexToReplaceWasp = index;
                            _prefabsToSpawns[index] = _prefabsToSpawns[0];
                            _shouldReAddWasp = true;
                        }
                    }
                    //re-adding wasp
                    if (_shouldReAddWasp)
                    {
                        _prefabsToSpawns[indexToReplaceWasp] = _wasp;
                        _shouldReAddWasp = false;
                    }
                }
            }
        }
    }
}
