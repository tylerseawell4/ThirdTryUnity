﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttackTime : MonoBehaviour
{
    private float _lightningAcumTime;
    private Transform _enemy;

    private void Awake()
    {
        _lightningAcumTime = .35f;
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
            var distance = Vector3.Distance(transform.position, _enemy.position);
            var yScale = transform.localScale.y;
            yScale = distance / 4;
            transform.localScale = new Vector2(transform.localScale.x, yScale);
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
