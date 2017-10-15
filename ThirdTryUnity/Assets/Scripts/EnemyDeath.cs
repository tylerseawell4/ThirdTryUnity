using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private Collider2D[] _colliders;
    private int _hp;
    public float flashTime;
    Color origionalColor;
    public SpriteRenderer _renderer;
    private Animator _anim;
    private EnemyController _enemyMovement;
    private bool _isColliding;

    // Use this for initialization
    void Start()
    {
        _anim = GetComponent<Animator>();
        origionalColor = _renderer.color;
        _colliders = GetComponents<Collider2D>();
        _enemyMovement = GetComponent<EnemyController>();
        DetermineHp();
    }

    // Update is called once per frame
    void Update()
    {
        _isColliding = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isColliding)
            return;

        _isColliding = true;

        if (collision.gameObject.tag == "Bullet")
        {
            _hp--;
            FlashRed();
            if (_hp == 0)
            {
                foreach (var collider in _colliders)
                    collider.enabled = false;

                _enemyMovement._moveSpeed = 1f;
                gameObject.tag = "Nonlethal";             
                StartCoroutine("DeathSequence");
            }
        }
    }

    IEnumerator DeathSequence()
    {
        _anim.SetInteger("State", 1);
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }

    private void DetermineHp()
    {
        if (transform.localScale.x > 1f)
            _hp = 2;
        else if (transform.localScale.x <= 1f)
            _hp = 1;
    }

    void FlashRed()
    {
        _renderer.color = Color.red;
        Invoke("ResetColor", flashTime);
    }

    void ResetColor()
    {
        _renderer.color = origionalColor;
    }
}
