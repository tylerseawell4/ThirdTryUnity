using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLighnting : MonoBehaviour
{

    public GameObject _lightningPrefab;
    public Transform _targetPosition;
    public GameObject _contactPoint;
    public GameObject _spawnPoint;
    private bool _shouldGrow;
    private Vector3 _ogSpawnpoint;

    // Use this for initialization
    void Start()
    {
        //transform.up = _targetPosition.position - transform.position;
        //GetComponent<SpriteRenderer>().flipY = true;
        //_shouldGrow = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (_targetPosition != null)
        //{
        //    transform.up = _targetPosition.position - transform.position;
        //    transform.position = Vector3.Lerp(transform.position, _targetPosition.position, .5f * Time.deltaTime);
        //    var y = transform.localScale.y;
        //    y += .015f;
        //    transform.localScale = new Vector2(_lightningPrefab.transform.localScale.x, y);
        //}
    }
}
