using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    private VelocityBounce2 _playerBounce;
    public bool _canSpawnOS;
    private int _runToSpawn;
    public GameObject _OvershieldToSpawn;
    public bool _shouldResetRunNumber;
    // Use this for initialization
    void Start()
    {
        _playerBounce = FindObjectOfType<VelocityBounce2>();
        _canSpawnOS = true;
        _shouldResetRunNumber = false;
        //multiples of 3 because each 3 bounces the speed increases. just want to spawn 1 per 3 bounces/speed
        _runToSpawn = Random.Range(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldResetRunNumber)
        {
            _runToSpawn = Random.Range(1, 4);
            _shouldResetRunNumber = false;
        }

        if (_canSpawnOS)
        {
            if (_runToSpawn == _playerBounce.GetRunCount())
            {
                Instantiate(_OvershieldToSpawn, new Vector3(0, Random.Range(15f, _playerBounce._startingHeight - 15f), _OvershieldToSpawn.transform.position.z), Quaternion.identity);
                _canSpawnOS = false;
            }
        }
    }
}
