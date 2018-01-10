using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDeath : MonoBehaviour
{
    public SpriteRenderer _renderer;
    public GameObject _enemyDroplingToSpawn;

    private bool _floatingScoreInstantiated = true;
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
    private FloatingScore _floatingScore;
    public bool _triggerDeath;
    private bool _deathBySuper;
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
        if (_triggerDeath)
        {
            _triggerDeath = false;
            Die();
            _deathBySuper = false;
        }
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

        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "SuperElectricOrb")
        {
            if (collision.gameObject.tag == "SuperElectricOrb")
                _deathBySuper = true;

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

            _deathBySuper = true;
        }
        else if (collision.gameObject.tag == "SuperActivation" || collision.gameObject.tag == "Super")
        {
            _deathBySuper = true;
            Die();
        }
        else if (collision.gameObject.tag == "IceSpike")
            Die();
    }

    public void Die()
    {
        if (gameObject.tag == "BounceBack")
            foreach (var collider in GetComponents<Collider2D>())
                collider.enabled = false;
        else
            _collider.enabled = false;

        if (_floatingScoreInstantiated)
        {
            _floatingScoreInstantiated = true;
            GameObject instance = Instantiate(Resources.Load("Floating Score Canvas"), gameObject.transform) as GameObject;
            _floatingScore = FindObjectOfType<FloatingScore>();
            _floatingScore.SetText(gameObject.name, gameObject.transform);

            //Only setting the floating scores transform.position rather than the rotation and scale by using the gameObjects.transform
            var _transformPosition = new GameObject().transform;
            _transformPosition.transform.position = gameObject.transform.position;
            instance.transform.SetParent(_transformPosition, false);
        }

        _floatingScoreInstantiated = false;
        StartCoroutine("TurnRed");
        if (gameObject.tag != "Spider")
            _enemyMovement._moveSpeed = 1f;
        StartCoroutine("DeathSequence");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "BounceBack" && collision.gameObject.tag == "Player")
            return;

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

        if (gameObject.tag == "BurstBug" && !_deathBySuper)
        {
            var burstBug = GetComponent<BurstBug>();
            burstBug._shouldReplicate = true;
            burstBug.SpawnBugs();
        }

        if (gameObject.tag == "RussianBug" && !_deathBySuper)
        {
            var russianBug = GetComponent<RussianBug>();
            //russianBug._shouldReplicate = true;
            russianBug.SpawnBugs();
        }

        _playerDeath._enemyDeathCounter += 1;

        Destroy(gameObject);
    }

    private void DetermineHp()
    {
        if (gameObject.tag == "BounceBack")
            _hp = 12;
        else if (transform.localScale.x >= 1.3f && gameObject.tag != "Spider")
            _hp = 4;
    }
}