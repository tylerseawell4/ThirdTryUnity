using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public SpriteRenderer _renderer;
    public GameObject _enemyDroplingToSpawn;

    private int _hp;
    public float flashTime;
    private Color _origionalColor;
    private Animator _anim;
    private EnemyController _enemyMovement;
    private bool _isColliding;
    private PlayerHealth _playerHealth;
    private Collider2D _collider;
    private PlayerDeath _playerDeath;
    private SuperMoveManager _superMoveManager;
    private SuperKetchup _superKetchup;

    private void Awake()
    {
        _superKetchup = FindObjectOfType<SuperKetchup>();
        _superMoveManager = FindObjectOfType<SuperMoveManager>();
        _playerDeath = FindObjectOfType<PlayerDeath>();
        _collider = GetComponent<Collider2D>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _anim = GetComponent<Animator>();
        _origionalColor = _renderer.color;
        _enemyMovement = GetComponent<EnemyController>();
        _origionalColor = _renderer.color;
        _hp = 2;
        DetermineHp();
    }

    // Update is called once per frame
    void Update()
    {
        _isColliding = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Super" && _superKetchup._enemiesThatCanBeShocked.Contains(gameObject))
            _superKetchup._enemiesThatCanBeShocked.Remove(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isColliding)
            return;

        _isColliding = true;

        var sprite = collision.gameObject.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            var inWormColor = new Color(.25f, .25f, .25f, .6f);
            if (sprite.color == inWormColor)
                return;
        }

        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player")
        {
            var damage = 1;
            var collisionWithMoveAndDestroyScript = collision.gameObject.GetComponent<MoveAndDestroyWeapon>();
            if (collisionWithMoveAndDestroyScript != null)
                damage = collisionWithMoveAndDestroyScript._damage;

            StartCoroutine("TurnRed");
            _hp -= damage;

            if (collision.gameObject.tag == "Player")
                _hp = 0;

            if (_hp <= 0)
                Die();
        }
        else if (collision.gameObject.tag == "IceSpike")
            Die();

        if (collision.gameObject.tag == "Super" && _superMoveManager._ketchupActivated)
        {
            if (!_superKetchup._enemiesThatCanBeShocked.Contains(gameObject))
                _superKetchup._enemiesThatCanBeShocked.Add(gameObject);
        }
        else if (collision.gameObject.tag == "SuperActivation" || collision.gameObject.tag == "Super")
            Die();
        else if (collision.gameObject.tag == "IceSpike")
            Die();
    }

    public void Die()
    {
        _collider.enabled = false;
        StartCoroutine("TurnRed");
        _enemyMovement._moveSpeed = 1f;
        gameObject.tag = "Nonlethal";
        StartCoroutine("DeathSequence");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isColliding)
            return;

        _isColliding = true;

        var sprite = collision.gameObject.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            var inWormColor = new Color(.25f, .25f, .25f, .6f);
            if (sprite.color == inWormColor)
                return;
        }

        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player")
        {
            var damage = 1;
            var collisionWithMoveAndDestroyScript = collision.gameObject.GetComponent<MoveAndDestroyWeapon>();
            if (collisionWithMoveAndDestroyScript != null)
                damage = collisionWithMoveAndDestroyScript._damage;

            StartCoroutine("TurnRed");
            _hp -= damage;

            if (collision.gameObject.tag == "Player")
                _hp = 0;

            if (_hp <= 0)
                Die();
        }
        else if (collision.gameObject.tag == "IceSpike")
            Die();
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

        _playerDeath._enemyDeathCounter += 1;
        Destroy(gameObject);
    }

    private void DetermineHp()
    {
        if (transform.localScale.x >= 1.3f)
            _hp = 4;
    }
}