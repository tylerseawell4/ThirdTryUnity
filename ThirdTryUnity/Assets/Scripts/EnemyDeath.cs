using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private Collider2D[] _colliders;
    private int _hp;
    public float flashTime;
    private Color _origionalColor;
    public SpriteRenderer _renderer;
    private Animator _anim;
    private EnemyController _enemyMovement;
    private bool _isColliding;
    private PlayerHealth _playerHealth;
    public GameObject _enemyDroplingToSpawn;
    private void Awake()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _origionalColor = _renderer.color;
        _hp = 1;
        DetermineHp();
    }
    // Use this for initialization
    void Start()
    {
        _anim = GetComponent<Animator>();
        _origionalColor = _renderer.color;
        _colliders = GetComponents<Collider2D>();
        _enemyMovement = GetComponent<EnemyController>();
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

        if (collision.gameObject.tag == "Super" || collision.gameObject.tag == "SuperActivation")
        {
            if (collision.gameObject.tag == "Super")
            {
                FindObjectOfType<SuperMode>().SpawnLightning(transform);
            }
            foreach (var collider in _colliders)
                collider.enabled = false;

            _enemyMovement._moveSpeed = 1f;
            gameObject.tag = "Nonlethal";
            StartCoroutine("DeathSequence");
        }

        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player")
        {
            StartCoroutine("TurnRed");
            _hp--;
            if (_hp <= 0)
            {
                foreach (var collider in _colliders)
                    collider.enabled = false;

                _enemyMovement._moveSpeed = 1f;
                gameObject.tag = "Nonlethal";
                StartCoroutine("DeathSequence");
            }
        }
    }

    IEnumerator TurnRed()
    {
        _renderer.color = Color.red;
        yield return new WaitForSeconds(.25f);
        _renderer.color = _origionalColor;
    }

    IEnumerator DeathSequence()
    {
        _anim.SetInteger("State", 1);
        yield return new WaitForSeconds(.5f);

        if (gameObject.transform.localScale.x > 2f)
            _enemyDroplingToSpawn.transform.localScale = new Vector2(2.5f, 2.5f);
        else
            _enemyDroplingToSpawn.transform.localScale = new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.x);
        Instantiate(_enemyDroplingToSpawn, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _enemyDroplingToSpawn.gameObject.transform.position.z), Quaternion.identity);

        Destroy(gameObject);
    }

    private void DetermineHp()
    {
        if (transform.localScale.x >= 1.3f)
            _hp = 2;
    }
}
