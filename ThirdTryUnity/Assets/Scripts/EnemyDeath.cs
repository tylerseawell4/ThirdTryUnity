using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private int _hp;
    public float flashTime;
    Color origionalColor;
    public SpriteRenderer renderer;
    // Use this for initialization
    void Start()
    {
        origionalColor = renderer.color;
        DetermineHp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            _hp--;
            FlashRed();
            if (_hp == 0)
                Destroy(gameObject);
        }
    }

    private void DetermineHp()
    {
        if (transform.localScale.x >= 1f)
            _hp = 2;
        else if (transform.localScale.x < 1f)
            _hp = 1;
    }

    void FlashRed()
    {
        renderer.color = Color.red;
        Invoke("ResetColor", flashTime);
    }

    void ResetColor()
    {
        renderer.color = origionalColor;
    }
}
