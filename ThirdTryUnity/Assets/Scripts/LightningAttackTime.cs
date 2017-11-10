using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttackTime : MonoBehaviour
{
    private float _lightningAcumTime;
    private Transform _enemy;

    private void Awake()
    {
        _lightningAcumTime = .5f;
    }
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy != null)
        {
            transform.up = _enemy.position - transform.position;
           // transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        _lightningAcumTime -= 1f * Time.deltaTime;

        if (_lightningAcumTime <= 0)
            Destroy(gameObject);
    }
    public void SpawnLightning(Transform enemy)
    {
        _enemy = enemy;
        transform.up = enemy.position - transform.position;
        gameObject.GetComponent<SpriteRenderer>().flipY = true;
        //transform.rotation = Quaternion.Euler(0, 0, -90);
    }

}
